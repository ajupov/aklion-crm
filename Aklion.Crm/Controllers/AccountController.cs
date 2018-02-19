using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Aklion.Crm.Business.ImageLoad;
using Aklion.Crm.Business.Mail;
using Aklion.Crm.Business.Sms;
using Aklion.Crm.Business.UserPermission;
using Aklion.Crm.Business.UserToken;
using Aklion.Crm.Dao.Store;
using Aklion.Crm.Dao.User;
using Aklion.Crm.Dao.UserContext;
using Aklion.Crm.Domain.Store;
using Aklion.Crm.Enums;
using Aklion.Crm.Mappers.Account;
using Aklion.Crm.Mappers.UserContext;
using Aklion.Crm.Models.Account;
using Aklion.Crm.UserContext;
using Aklion.Infrastructure.FileFormat;
using Aklion.Infrastructure.Password;
using Aklion.Infrastructure.PhoneNumber;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Aklion.Crm.Controllers
{
    public class AccountController : BaseController
    {
        private readonly IMailService _mailService;
        private readonly ISmsService _smsService;
        private readonly IImageLoadService _imageLoadService;
        private readonly IUserTokenService _userTokenService;
        private readonly IUserPermissionService _userPermissionService;
        private readonly IUserDao _userDao;
        private readonly IUserContextDao _userContextDao;
        private readonly IStoreDao _storeDao;

        public AccountController(
            IMailService mailService,
            ISmsService smsService,
            IImageLoadService imageLoadService,
            IUserTokenService userTokenService,
            IUserPermissionService userPermissionService,
            IUserDao userDao,
            IUserContextDao userContextDao,
            IStoreDao storeDao)
        {
            _mailService = mailService;
            _smsService = smsService;
            _imageLoadService = imageLoadService;
            _userTokenService = userTokenService;
            _userPermissionService = userPermissionService;
            _userDao = userDao;
            _userContextDao = userContextDao;
            _storeDao = storeDao;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Index()
        {
            var user = await _userDao.GetAsync(UserContext.UserId).ConfigureAwait(false);
            return user == null ? View("Error") : View(user);
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult LogIn(string returnUrl = null)
        {
            ViewBag.ReturnUrl = returnUrl;

            return IsUserContextInitialized ? RedirectToLocal(returnUrl) : View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LogIn(LoginModel model, string returnUrl = null)
        {
            ViewBag.ReturnUrl = returnUrl;

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await _userDao.GetByLoginAsync(model.Login).ConfigureAwait(false);
            if (user == null)
            {
                ModelState.AddModelError(string.Empty, "Неправильный логин или пароль");

                return View(model);
            }

            if (user.IsDeleted)
            {
                ModelState.AddModelError(string.Empty, "Данная учетная запись удалена");

                return View(model);
            }

            if (!PasswordHelper.Verify(model.Password, user.PasswordHash))
            {
                ModelState.AddModelError(string.Empty, "Неправильный логин или пароль");

                return View(model);
            }

            await SignInAsync(user.Id, 0, model.RememberMe).ConfigureAwait(false);

            var userContextDomain = await _userContextDao.GetAsync(user.Id, 0).ConfigureAwait(false);

            InitializeUserContext(userContextDomain);

            return RedirectToLocal(returnUrl);
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LogOff()
        {
            await SignOutAsync().ConfigureAwait(false);

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Register(string returnUrl = null)
        {
            ViewBag.ReturnUrl = returnUrl;

            return IsUserContextInitialized ? RedirectToLocal(returnUrl) : View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterModel model, string returnUrl = null)
        {
            ViewBag.ReturnUrl = returnUrl;

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var isExistByLogin = await _userDao.IsExistByLoginAsync(model.Login).ConfigureAwait(false);
            if (isExistByLogin)
            {
                ModelState.AddModelError("Login", "Логин уже занят");

                return View(model);
            }

            var isExistByEmail = await _userDao.IsExistByEmailAsync(model.Email).ConfigureAwait(false);
            if (isExistByEmail)
            {
                ModelState.AddModelError("Email", "Email уже занят");

                return View(model);
            }

            var isExistByPhone = await _userDao.IsExistByPhoneAsync(model.Phone.ExtractPhoneNumber()).ConfigureAwait(false);
            if (isExistByPhone)
            {
                ModelState.AddModelError("Phone", "Телефон уже занят");

                return View(model);
            }

            var user = model.MapNew();

            user.PasswordHash = PasswordHelper.Generate(model.Password);
            user.Phone = user.Phone.ExtractPhoneNumber();
            user.Id = await _userDao.CreateAsync(user).ConfigureAwait(false);

            var store = new StoreModel
            {
                Name = model.StoreName,
                ApiSecret = null,
                IsLocked = false,
                IsDeleted = false,
                CreateDate = DateTime.Now,
                ModifyDate = null
            };

            store.Id = await _storeDao.CreateAsync(store).ConfigureAwait(false);

            await _userPermissionService.CreateForRegisteredUserAsync(user.Id, store.Id).ConfigureAwait(false);

            await EmailConfirmationProcess(user.Id).ConfigureAwait(false);

            await SignInAsync(user.Id, store.Id, true).ConfigureAwait(false);

            var userContextDomain = await _userContextDao.GetAsync(user.Id, 0).ConfigureAwait(false);

            InitializeUserContext(userContextDomain);

            return RedirectToAction("Index",
                new {message = "На Вашу электронную почту отправлено письмо с подтверждением"});
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> SendConfirmEmail()
        {
            await EmailConfirmationProcess(UserContext.UserId).ConfigureAwait(false);

            return RedirectToAction("Index",
                new {message = "На Вашу электронную почту отправлено письмо с подтверждением"});
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> SendSmsCode()
        {
            await PhoneConfirmationProcess(UserContext.UserId).ConfigureAwait(false);

            return RedirectToAction("VerifySmsCode",
                new {message = "На Ваш номер телефона отправлен SMS с кодом подтверждения"});
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> ConfirmEmail(int userId, string code)
        {
            if (userId == 0 || string.IsNullOrEmpty(code))
            {
                return View("Error");
            }

            var user = await _userDao.GetAsync(userId).ConfigureAwait(false);
            if (user == null)
            {
                return View("Error");
            }

            var isTokenConfirm = await _userTokenService.ConfirmAsync(userId, TokenType.EmailConfirmation, code)
                .ConfigureAwait(false);

            if (!isTokenConfirm)
            {
                return View("Error");
            }

            user.IsEmailConfirmed = true;

            await _userDao.UpdateAsync(user).ConfigureAwait(false);

            return RedirectToAction("Index", new {message = "Ваш Email подтвержден"});
        }

        [HttpGet]
        [Authorize]
        public IActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangePassword(ChangePasswordModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await _userDao.GetAsync(UserContext.UserId).ConfigureAwait(false);
            if (user == null)
            {
                return View(model);
            }

            var isOldPasswordVerified = PasswordHelper.Verify(model.OldPassword, user.PasswordHash);
            if (!isOldPasswordVerified)
            {
                ModelState.AddModelError("OldPassword", "Старый пароль введен неверно");

                return View(model);
            }

            user.PasswordHash = PasswordHelper.Generate(model.Password);

            await _userDao.UpdateAsync(user).ConfigureAwait(false);

            await SignInAsync(user.Id, 0, true).ConfigureAwait(false);

            return RedirectToAction("Index", new {message = "Ваш пароль успешно изменен"});
        }

        [HttpGet]
        [Authorize]
        public IActionResult ChangeEmail()
        {
            return View();
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangeEmail(ChangeEmailModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await _userDao.GetAsync(UserContext.UserId).ConfigureAwait(false);
            if (user == null)
            {
                return View(model);
            }

            if (user.Email == model.Email)
            {
                ModelState.AddModelError("Email", "Введен текущий Email");

                return View(model);
            }

            var isExistByEmail = await _userDao.IsExistByEmailAsync(model.Email).ConfigureAwait(false);
            if (isExistByEmail)
            {
                ModelState.AddModelError("Email", "Email уже занят");

                return View(model);
            }

            user.Email = model.Email;
            user.IsEmailConfirmed = false;

            await _userDao.UpdateAsync(user).ConfigureAwait(false);

            await EmailConfirmationProcess(user.Id).ConfigureAwait(false);

            return RedirectToAction("Index", new
            {
                message = "Ваш Email успешно изменен. На Вашу электронную почту отправлено письмо с подтверждением"
            });
        }

        [HttpGet]
        [Authorize]
        public IActionResult ChangePhone()
        {
            return View();
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangePhone(ChangePhoneModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await _userDao.GetAsync(UserContext.UserId).ConfigureAwait(false);
            if (user == null)
            {
                return View(model);
            }

            var isExistByPhone = await _userDao.IsExistByPhoneAsync(model.Phone).ConfigureAwait(false);
            if (isExistByPhone)
            {
                ModelState.AddModelError("Phone", "Номер телефона уже занят");

                return View(model);
            }

            user.Phone = model.Phone.ExtractPhoneNumber();
            user.IsPhoneConfirmed = false;

            await _userDao.UpdateAsync(user).ConfigureAwait(false);

            await PhoneConfirmationProcess(user.Id).ConfigureAwait(false);

            return RedirectToAction("VerifySmsCode", new
            {
                message = "Ваш номер телефона успешно изменен. На Ваш номер телефона отправлен SMS с кодом подтверждения"
            });
        }

        [HttpGet]
        [Authorize]
        public IActionResult VerifySmsCode(string message = null)
        {
            ViewBag.StatusMessage = message;

            return View();
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> VerifySmsCode(VerifySmsCodeModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await _userDao.GetAsync(UserContext.UserId).ConfigureAwait(false);
            if (user == null)
            {
                return View("Error");
            }

            var isTokenConfirm = await _userTokenService
                .ConfirmAsync(UserContext.UserId, TokenType.PhoneConfirmation, model.Code).ConfigureAwait(false);

            if (!isTokenConfirm)
            {
                return View("Error");
            }

            user.IsPhoneConfirmed = true;

            await _userDao.UpdateAsync(user).ConfigureAwait(false);

            return RedirectToAction("Index", new {message = "Ваш номер телефона подтвержден"});
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> ChangePersonalInfo()
        {
            var user = await _userDao.GetAsync(UserContext.UserId).ConfigureAwait(false);
            if (user == null)
            {
                return View("Error");
            }

            var model = user.MapNew();

            return View(model);
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangePersonalInfo(ChangePersonalInfoModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await _userDao.GetAsync(UserContext.UserId).ConfigureAwait(false);
            if (user == null)
            {
                return View("Error");
            }

            user.MapFrom(model);

            await _userDao.UpdateAsync(user).ConfigureAwait(false);

            return RedirectToAction("Index", new {message = "Ваши изменения сохранены"});
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await _userDao.GetByEmailAsync(model.Email).ConfigureAwait(false);
            if (user == null)
            {
                ModelState.AddModelError("Email", "Пользователь с указанным Email не найден");

                return View(model);
            }

            await PasswordResetProcess(user.Id).ConfigureAwait(false);

            return RedirectToAction("ForgotPasswordConfirmation",
                new {message = "На Вашу электронную почту отправлено письмо со ссылкой на форму сброса пароля"});
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult ForgotPasswordConfirmation()
        {
            return View();
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult ResetPassword(int userId, string code)
        {
            if (userId == 0 || string.IsNullOrEmpty(code))
            {
                return View("Error");
            }

            var model = new ResetPasswordModel
            {
                Code = code
            };

            return View(model);
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ResetPassword(ResetPasswordModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await _userDao.GetByEmailAsync(model.Email).ConfigureAwait(false);
            if (user == null)
            {
                ModelState.AddModelError("Email", "Пользователь с указанным Email не найден");

                return View(model);
            }

            var isTokenConfirm = await _userTokenService.ConfirmAsync(user.Id, TokenType.PasswordReset, model.Code)
                .ConfigureAwait(false);

            if (!isTokenConfirm)
            {
                return View("Error");
            }

            var oldUserClone = user.Clone();

            user.PasswordHash = PasswordHelper.Generate(model.Password);

            await _userDao.UpdateAsync(user).ConfigureAwait(false);

            return RedirectToAction("ResetPasswordConfirmation");
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult ResetPasswordConfirmation()
        {
            return View();
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LoadAvatar(LoadAvatarModel model)
        {
            if (!model.AvatarFile.FileName.IsImage())
            {
                return View("Error");
            }

            var user = await _userDao.GetAsync(UserContext.UserId).ConfigureAwait(false);

            user.AvatarUrl = await _imageLoadService.LoadAvatarImageAsync(model.AvatarFile).ConfigureAwait(false);

            await _userDao.UpdateAsync(user).ConfigureAwait(false);

            return RedirectToAction("Index", "Account");
        }

        [HttpGet]
        public async Task<List<UserAvialableStore>> GetStores()
        {
            var result = await _userContextDao.GetAvialableStoresAsync(UserContext.UserId).ConfigureAwait(false);
            return result.Map();
        }

        [HttpPost]
        public Task SetStore(int storeId)
        {
            return SignInAsync(UserContext.UserId, storeId, true);
        }

        private async Task EmailConfirmationProcess(int userId)
        {
            var user = await _userDao.GetAsync(userId).ConfigureAwait(false);

            var code = await _userTokenService.CreateAsync(userId, TokenType.EmailConfirmation).ConfigureAwait(false);

            var emailConfirmUrl = Url.Action("ConfirmEmail", "Account", new {userId, code}, HttpContext.Request.Scheme);

            await _mailService.SendFromAdminAsync(user.Email, "Подтверждение почты",
                    $"Пожалуйста, подтвердите свою почту, нажав на <a href='{emailConfirmUrl}'>ссылку</a>.")
                .ConfigureAwait(false);
        }

        private async Task PhoneConfirmationProcess(int userId)
        {
            var user = await _userDao.GetAsync(userId).ConfigureAwait(false);

            var code = await _userTokenService.CreateAsync(userId, TokenType.PhoneConfirmation).ConfigureAwait(false);

            await _smsService.SendAsync(user.Phone, code).ConfigureAwait(false);
        }

        private async Task PasswordResetProcess(int userId)
        {
            var user = await _userDao.GetAsync(userId).ConfigureAwait(false);

            var code = await _userTokenService.CreateAsync(user.Id, TokenType.PasswordReset).ConfigureAwait(false);

            var passwordResetUrl = Url.Action("ResetPassword", "Account", new {userId = user.Id, code},
                HttpContext.Request.Scheme);

            await _mailService.SendFromAdminAsync(user.Email, "Сброс пароля",
                    $"Для сброса пароля нажмите на <a href='{passwordResetUrl}'>ссылку</a>.")
                .ConfigureAwait(false);
        }
    }
}