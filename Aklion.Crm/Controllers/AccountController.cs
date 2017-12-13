using System.Threading.Tasks;
using Aklion.Crm.Business.ImageLoad;
using Aklion.Crm.Business.Mail;
using Aklion.Crm.Business.Sms;
using Aklion.Crm.Business.UserToken;
using Aklion.Crm.Dao.User;
using Aklion.Crm.Dao.UserContext;
using Aklion.Crm.Enums;
using Aklion.Crm.Mappers.Account;
using Aklion.Crm.Models.Account;
using Aklion.Infrastructure.DateTime;
using Aklion.Infrastructure.FileFormat;
using Aklion.Infrastructure.Password;
using Aklion.Infrastructure.PhoneNumber;
using Aklion.Infrastructure.Utils.Logger;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Aklion.Crm.Controllers
{
    public class AccountController : BaseController
    {
        private readonly ILogger _logger;
        private readonly IMailService _mailService;
        private readonly ISmsService _smsService;
        private readonly IImageLoadService _imageLoadService;
        private readonly IUserTokenService _userTokenService;
        private readonly IUserDao _userDao;
        private readonly IUserContextDao _userContextDao;

        public AccountController(
            ILogger logger,
            IMailService mailService,
            ISmsService smsService,
            IImageLoadService imageLoadService,
            IUserTokenService userTokenService,
            IUserDao userDao,
            IUserContextDao userContextDao)
        {
            _logger = logger;
            _mailService = mailService;
            _smsService = smsService;
            _imageLoadService = imageLoadService;
            _userTokenService = userTokenService;
            _userDao = userDao;
            _userContextDao = userContextDao;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Index()
        {
            var user = await _userDao.Get(UserContext.UserId).ConfigureAwait(false);
            if (user == null)
            {
                _logger.LogWarning("AccountController.Index(). User not found.", UserContext.UserId);

                return View("Error");
            }

            return View(user);
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult LogIn(string returnUrl = null)
        {
            ViewBag.ReturnUrl = returnUrl;

            if (IsUserContextInitialized)
            {
                return RedirectToLocal(returnUrl);
            }

            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LogIn(LoginModel model, string returnUrl = null)
        {
            ViewBag.ReturnUrl = returnUrl;

            if (!ModelState.IsValid)
            {
                _logger.LogWarning("AccountController.LogIn(). ModelState is invalid.", 0, new
                {
                    model.Login,
                    model.RememberMe,
                    ReturnUrl = returnUrl
                });

                return View(model);
            }

            var user = await _userDao.GetByLogin(model.Login).ConfigureAwait(false);
            if (user == null)
            {
                ModelState.AddModelError(string.Empty, "Неправильный логин или пароль");

                _logger.LogWarning("AccountController.LogIn(). User not found.", 0, new
                {
                    model.Login,
                    model.RememberMe,
                    ReturnUrl = returnUrl
                });

                return View(model);
            }

            if (!PasswordHelper.Verify(model.Password, user.PasswordHash))
            {
                ModelState.AddModelError(string.Empty, "Неправильный логин или пароль");

                _logger.LogWarning("AccountController.LogIn(). Invalid login or password.", 0, new
                {
                    model.Login,
                    model.RememberMe,
                    ReturnUrl = returnUrl
                });

                return View(model);
            }

            await SignInAsync(user.Login, model.RememberMe).ConfigureAwait(false);

            var userContextDomain = await _userContextDao.Get(user.Login, 0).ConfigureAwait(false);
            if (userContextDomain == null)
            {
                _logger.LogWarning("AccountController.LogIn(). User context not found.", 0, new
                {
                    model.Login,
                    model.RememberMe,
                    ReturnUrl = returnUrl
                });
            }

            InitializeUserContext(userContextDomain);

            _logger.LogInfo("AccountController.LogIn(). Signed in.", user.Id, new
            {
                model.Login,
                model.RememberMe,
                ReturnUrl = returnUrl
            });

            return RedirectToLocal(returnUrl);
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LogOff()
        {
            var user = await _userDao.Get(UserContext.UserId).ConfigureAwait(false);
            if (user == null)
            {
                _logger.LogWarning("AccountController.LogOff(). User not found.", UserContext.UserId, new
                {
                    UserContext.UserId
                });

                return View("Error");
            }

            await SignOutAsync().ConfigureAwait(false);

            _logger.LogInfo("AccountController.LogOff(). Signed out.", 0, new
            {
                UserId = user.Id
            });

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Register(string returnUrl = null)
        {
            ViewBag.ReturnUrl = returnUrl;

            if (IsUserContextInitialized)
            {
                return RedirectToLocal(returnUrl);
            }

            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterModel model, string returnUrl = null)
        {
            ViewBag.ReturnUrl = returnUrl;

            if (!ModelState.IsValid)
            {
                _logger.LogWarning("AccountController.Register(). ModelState is invalid.", 0, new
                {
                    model.Login,
                    model.Email,
                    model.Phone,
                    model.Surname,
                    model.Name,
                    model.Patronymic,
                    model.Gender,
                    model.BirthDateString,
                    ReturnUrl = returnUrl
                });

                return View(model);
            }

            var isExistByLogin = await _userDao.IsExistByLogin(model.Login).ConfigureAwait(false);
            if (isExistByLogin)
            {
                ModelState.AddModelError("Login", "Логин уже занят");

                _logger.LogWarning("AccountController.Register(). Login is exist.", 0, new
                {
                    model.Login,
                    model.Email,
                    model.Phone,
                    model.Surname,
                    model.Name,
                    model.Patronymic,
                    model.Gender,
                    model.BirthDateString,
                    ReturnUrl = returnUrl
                });

                return View(model);
            }

            var isExistByEmail = await _userDao.IsExistByEmail(model.Email).ConfigureAwait(false);
            if (isExistByEmail)
            {
                ModelState.AddModelError("Email", "Email уже занят");

                _logger.LogWarning("AccountController.Register(). Email is exist.", 0, new
                {
                    model.Login,
                    model.Email,
                    model.Phone,
                    model.Surname,
                    model.Name,
                    model.Patronymic,
                    model.Gender,
                    model.BirthDateString,
                    ReturnUrl = returnUrl
                });

                return View(model);
            }

            var isExistByPhone = await _userDao.IsExistByPhone(model.Phone).ConfigureAwait(false);
            if (isExistByPhone)
            {
                ModelState.AddModelError("Phone", "Телефон уже занят");

                _logger.LogWarning("AccountController.Register(). Phone is exist.", 0, new
                {
                    model.Login,
                    model.Email,
                    model.Phone,
                    model.Surname,
                    model.Name,
                    model.Patronymic,
                    model.Gender,
                    model.BirthDateString,
                    ReturnUrl = returnUrl
                });

                return View(model);
            }

            var user = model.Map();

            user.Id = await _userDao.Create(user).ConfigureAwait(false);

            _logger.LogInfo("AccountController.Register(). Registered.", 0,
                new
                {
                    model.Login,
                    model.Email,
                    model.Phone,
                    model.Surname,
                    model.Name,
                    model.Patronymic,
                    model.Gender,
                    model.BirthDateString,
                    ReturnUrl = returnUrl
                });

            await EmailConfirmationProcess(user.Id).ConfigureAwait(false);

            _logger.LogInfo("AccountController.Register(). Email confirmation link sended.", 0, new
            {
                model.Login,
                model.Email,
                model.Phone,
                model.Surname,
                model.Name,
                model.Patronymic,
                model.Gender,
                model.BirthDateString,
                ReturnUrl = returnUrl
            });

            await SignInAsync(user.Login, true).ConfigureAwait(false);

            var userContextDomain = await _userContextDao.Get(user.Login, 0).ConfigureAwait(false);
            if (userContextDomain == null)
            {
                _logger.LogWarning("AccountController.Register(). User context not found.", 0, new
                {
                    model.Login,
                    model.Email,
                    model.Phone,
                    model.Surname,
                    model.Name,
                    model.Patronymic,
                    model.Gender,
                    model.BirthDateString,
                    ReturnUrl = returnUrl
                });
            }

            InitializeUserContext(userContextDomain);

            _logger.LogInfo("AccountController.Register(). Signed in.", UserContext.UserId, new
            {
                model.Login,
                model.Email,
                model.Phone,
                model.Surname,
                model.Name,
                model.Patronymic,
                model.Gender,
                model.BirthDateString,
                ReturnUrl = returnUrl
            });

            return RedirectToAction("Index",
                new {message = "На Вашу электронную почту отправлено письмо с подтверждением"});
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> SendConfirmEmail()
        {
            var user = await _userDao.Get(UserContext.UserId).ConfigureAwait(false);
            if (user == null)
            {
                _logger.LogWarning("AccountController.SendConfirmEmail(). User not found.", UserContext.UserId, new
                {
                    UserContext.UserId
                });

                return View("Error");
            }

            await EmailConfirmationProcess(UserContext.UserId).ConfigureAwait(false);

            _logger.LogInfo("AccountController.SendConfirmEmail(). Email confirmation link sended.",
                UserContext.UserId);

            return RedirectToAction("Index",
                new {message = "На Вашу электронную почту отправлено письмо с подтверждением"});
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> SendSmsCode()
        {
            var user = await _userDao.Get(UserContext.UserId).ConfigureAwait(false);
            if (user == null)
            {
                _logger.LogWarning("AccountController.SendSmsCode(). User not found.", UserContext.UserId, new
                {
                    UserContext.UserId
                });

                return View("Error");
            }

            await PhoneConfirmationProcess(UserContext.UserId).ConfigureAwait(false);

            _logger.LogInfo("AccountController.SendSmsCode(). Phone confirmation code sended.", UserContext.UserId);

            return RedirectToAction("VerifySmsCode",
                new {message = "На Ваш номер телефона отправлен SMS с кодом подтверждения"});
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> ConfirmEmail(int userId, string code)
        {
            if (userId == 0 || string.IsNullOrEmpty(code))
            {
                _logger.LogWarning("AccountController.ConfirmEmail(). Empty data.", 0, new
                {
                    UserId = userId,
                    Code = code
                });

                return View("Error");
            }

            var user = await _userDao.Get(userId).ConfigureAwait(false);
            if (user == null)
            {
                _logger.LogWarning("AccountController.ConfirmEmail(). User not found.", 0, new
                {
                    UserId = userId,
                    Code = code
                });

                return View("Error");
            }

            var isTokenConfirm = await _userTokenService.Confirm(userId, TokenType.EmailConfirmation, code)
                .ConfigureAwait(false);

            if (!isTokenConfirm)
            {
                _logger.LogWarning("AccountController.ConfirmEmail(). Token not confirmed.", 0, new
                {
                    UserId = userId,
                    Code = code
                });

                return View("Error");
            }

            _logger.LogInfo("AccountController.ConfirmEmail(). Token confirmed.", 0, new
            {
                UserId = userId,
                Code = code
            });

            user.IsEmailConfirmed = true;

            await _userDao.Update(user).ConfigureAwait(false);

            _logger.LogInfo("AccountController.ConfirmEmail(). User.IsEmailConfirmed = true.", 0, new
            {
                UserId = userId,
                Code = code
            });

            return RedirectToAction("Index", new {message = "Ваш Email подтвержден"});
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> ChangePassword()
        {
            var user = await _userDao.Get(UserContext.UserId).ConfigureAwait(false);
            if (user == null)
            {
                _logger.LogWarning("AccountController.ChangePassword(). User not found.", UserContext.UserId, new
                {
                    UserContext.UserId
                });

                return View("Error");
            }

            return View();
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangePassword(ChangePasswordModel model)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogWarning("AccountController.ChangePassword(). ModelState is invalid.", UserContext.UserId);

                return View(model);
            }

            var user = await _userDao.Get(UserContext.UserId).ConfigureAwait(false);
            if(user == null)
            {
                _logger.LogWarning("AccountController.ChangePassword(). User not found.", UserContext.UserId);

                return View(model);
            }

            var isOldPasswordVerified = PasswordHelper.Verify(model.OldPassword, user.PasswordHash);
            if (!isOldPasswordVerified)
            {
                ModelState.AddModelError("OldPassword", "Старый пароль введен неверно");

                _logger.LogWarning("AccountController.ChangePassword(). Old password is incorrect.", UserContext.UserId);

                return View(model);
            }

            user.PasswordHash = PasswordHelper.Generate(model.Password);

            await _userDao.Update(user).ConfigureAwait(false);

            _logger.LogInfo("AccountController.ChangePassword(). Password changed.", UserContext.UserId);

            await SignInAsync(user.Login, true).ConfigureAwait(false);

            _logger.LogInfo("AccountController.ChangePassword(). Signed in.", UserContext.UserId);

            return RedirectToAction("Index", new {message = "Ваш пароль успешно изменен"});
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> ChangeEmail()
        {
            var user = await _userDao.Get(UserContext.UserId).ConfigureAwait(false);
            if (user == null)
            {
                _logger.LogWarning("AccountController.ChangeEmail(). User not found.", UserContext.UserId, new
                {
                    UserContext.UserId
                });

                return View("Error");
            }

            return View();
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangeEmail(ChangeEmailModel model)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogWarning("AccountController.ChangeEmail(). ModelState is invalid.", UserContext.UserId, new
                {
                    model.Email
                });

                return View(model);
            }

            var user = await _userDao.Get(UserContext.UserId).ConfigureAwait(false);
            if (user == null)
            {
                _logger.LogWarning("AccountController.ChangeEmail(). User not found.", UserContext.UserId);

                return View(model);
            }

            if (user.Email == model.Email)
            {
                ModelState.AddModelError("Email", "Введен текущий Email");

                _logger.LogWarning("AccountController.ChangeEmail(). Current Email entered.", UserContext.UserId);

                return View(model);
            }

            var isExistByEmail = await _userDao.IsExistByEmail(model.Email).ConfigureAwait(false);
            if (isExistByEmail)
            {
                ModelState.AddModelError("Email", "Email уже занят");

                _logger.LogWarning("AccountController.ChangeEmail(). Email is exist.", UserContext.UserId, new
                {
                    model.Email
                });

                return View(model);
            }

            user.Email = model.Email;
            user.IsEmailConfirmed = false;

            await _userDao.Update(user).ConfigureAwait(false);

            _logger.LogInfo("AccountController.ChangeEmail(). Email changed.", UserContext.UserId, new
            {
                model.Email
            });

            await EmailConfirmationProcess(user.Id).ConfigureAwait(false);

            _logger.LogInfo("AccountController.ChangeEmail(). Email confirmation link sended.", UserContext.UserId, new
            {
                model.Email
            });

            return RedirectToAction("Index", new
            {
                message = "Ваш Email успешно изменен. На Вашу электронную почту отправлено письмо с подтверждением"
            });
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> ChangePhone()
        {
            var user = await _userDao.Get(UserContext.UserId).ConfigureAwait(false);
            if (user == null)
            {
                _logger.LogWarning("AccountController.ChangePhone(). User not found.", UserContext.UserId, new
                {
                    UserContext.UserId
                });

                return View("Error");
            }

            return View();
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangePhone(ChangePhoneModel model)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogWarning("AccountController.ChangePhone(). ModelState is invalid.", UserContext.UserId, new
                {
                    model.Phone
                });

                return View(model);
            }

            var user = await _userDao.Get(UserContext.UserId).ConfigureAwait(false);
            if (user == null)
            {
                _logger.LogWarning("AccountController.ChangePhone(). User not found.", UserContext.UserId);

                return View(model);
            }

            var isExistByPhone = await _userDao.IsExistByPhone(model.Phone).ConfigureAwait(false);
            if (isExistByPhone)
            {
                ModelState.AddModelError("Phone", "Номер телефона уже занят");

                _logger.LogWarning("AccountController.ChangePhone(). Phone is exist.", UserContext.UserId, new
                {
                    model.Phone
                });

                return View(model);
            }

            user.Phone = model.Phone.ExtractPhoneNumber();
            user.IsPhoneConfirmed = false;

            await _userDao.Update(user).ConfigureAwait(false);

            _logger.LogInfo("AccountController.ChangePhone(). Phone changed.", UserContext.UserId, new
            {
                model.Phone
            });

            await PhoneConfirmationProcess(user.Id).ConfigureAwait(false);

            _logger.LogInfo("AccountController.ChangePhone(). Phone confirmation code sended.", UserContext.UserId, new
            {
                model.Phone
            });

            return RedirectToAction("VerifySmsCode", new
            {
                message =
                "Ваш номер телефона успешно изменен. На Ваш номер телефона отправлен SMS с кодом подтверждения"
            });
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> VerifySmsCode(string message = null)
        {
            ViewBag.StatusMessage = message;

            var user = await _userDao.Get(UserContext.UserId).ConfigureAwait(false);
            if (user == null)
            {
                _logger.LogWarning("AccountController.VerifySmsCode(). User not found.", UserContext.UserId, new
                {
                    UserContext.UserId
                });

                return View("Error");
            }

            return View();
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> VerifySmsCode(VerifySmsCodeModel model)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogWarning("AccountController.VerifySmsCode(). ModelState is invalid.", UserContext.UserId, new
                {
                    model.Code
                });

                return View(model);
            }

            var user = await _userDao.Get(UserContext.UserId).ConfigureAwait(false);
            if (user == null)
            {
                _logger.LogWarning("AccountController.VerifySmsCode(). User not found.", 0, new
                {
                    UserContext.UserId,
                    model.Code
                });

                return View("Error");
            }

            var isTokenConfirm = await _userTokenService
                .Confirm(UserContext.UserId, TokenType.PhoneConfirmation, model.Code).ConfigureAwait(false);

            if (!isTokenConfirm)
            {
                _logger.LogWarning("AccountController.VerifySmsCode(). Token not confirmed.", 0, new
                {
                    UserContext.UserId,
                    model.Code
                });

                return View("Error");
            }

            _logger.LogInfo("AccountController.VerifySmsCode(). Token confirmed.", 0, new
            {
                UserContext.UserId,
                model.Code
            });

            user.IsPhoneConfirmed = true;

            await _userDao.Update(user).ConfigureAwait(false);

            _logger.LogInfo("AccountController.VerifySmsCode(). User.IsPhoneConfirmed = true.", UserContext.UserId, new
            {
                UserContext.UserId,
                model.Code
            });

            return RedirectToAction("Index", new {message = "Ваш номер телефона подтвержден"});
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> ChangePersonalInfo()
        {
            var user = await _userDao.Get(UserContext.UserId).ConfigureAwait(false);
            if (user == null)
            {
                _logger.LogWarning("AccountController.ChangePersonalInfo(). User not found.", 0, new
                {
                    UserContext.UserId
                });

                return View("Error");
            }

            var model = user.Map();

            return View(model);
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangePersonalInfo(ChangePersonalInfoModel model)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogWarning("AccountController.ChangePersonalInfo(). ModelState is invalid.", UserContext.UserId,
                    model);

                return View(model);
            }

            var user = await _userDao.Get(UserContext.UserId).ConfigureAwait(false);
            if (user == null)
            {
                _logger.LogWarning("AccountController.ChangePersonalInfo(). User not found.", 0, new
                {
                    UserContext.UserId
                });

                return View("Error");
            }

            model.Map(user);

            user.Surname = model.Surname;
            user.Name = model.Name;
            user.Patronymic = model.Patronymic;
            user.Gender = model.Gender;
            user.BirthDate = model.BirthDateString.ToDate();

            await _userDao.Update(user).ConfigureAwait(false);

            _logger.LogInfo("AccountController.ChangePersonalInfo(). Personal info changed.", UserContext.UserId,
                model);

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
                _logger.LogWarning("AccountController.ForgotPassword(). ModelState is invalid.", UserContext.UserId,
                    model);

                return View(model);
            }

            var user = await _userDao.GetByEmail(model.Email).ConfigureAwait(false);
            if (user == null)
            {
                ModelState.AddModelError("Email", "Пользователь с указанным Email не найден");

                _logger.LogWarning("AccountController.ForgotPassword(). User not found.", 0, model);

                return View(model);
            }

            await PasswordResetProcess(user.Id).ConfigureAwait(false);

            _logger.LogInfo("AccountController.ForgotPassword(). Password reset code sended.");

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
        public async Task<IActionResult> ResetPassword(int userId, string code)
        {
            if (userId == 0 || string.IsNullOrEmpty(code))
            {
                _logger.LogWarning("AccountController.ResetPassword(). Empty data.", 0, new
                {
                    UserId = userId,
                    Code = code
                });

                return View("Error");
            }

            var user = await _userDao.Get(userId).ConfigureAwait(false);
            if (user == null)
            {
                _logger.LogWarning("AccountController.ResetPassword(). User not found.", 0, new
                {
                    UserId = userId,
                    Code = code
                });

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
                _logger.LogWarning("AccountController.ResetPassword(). ModelState is invalid.", UserContext.UserId, new
                {
                    model.Email,
                    model.Code
                });

                return View(model);
            }

            var user = await _userDao.GetByEmail(model.Email).ConfigureAwait(false);
            if (user == null)
            {
                ModelState.AddModelError("Email", "Пользователь с указанным Email не найден");

                _logger.LogWarning("AccountController.ResetPassword(). User not found.", 0, model);

                return View(model);
            }

            var isTokenConfirm = await _userTokenService.Confirm(user.Id, TokenType.PasswordReset, model.Code)
                .ConfigureAwait(false);

            if (!isTokenConfirm)
            {
                _logger.LogWarning("AccountController.ResetPassword(). Token not confirmed.", 0, new
                {
                    UserId = user.Id,
                    model.Code
                });

                return View("Error");
            }

            _logger.LogInfo("AccountController.ResetPassword(). Token confirmed.", 0, new
            {
                UserId = user.Id,
                model.Code
            });

            user.PasswordHash = PasswordHelper.Generate(model.Password);

            await _userDao.Update(user).ConfigureAwait(false);

            _logger.LogInfo("AccountController.ResetPassword(). User.PasswordHash updated.", 0, new
            {
                UserId = user.Id,
                model.Code
            });

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
            if (!model.AvatarFile.Name.IsImage())
            {
                return View("Error");
            }

            var user = await _userDao.Get(UserContext.UserId).ConfigureAwait(false);
            if (user == null)
            {
                _logger.LogWarning("AccountController.LoadAvatar(). User not found.", UserContext.UserId);

                return View("Error");
            }

            user.AvatarUrl = await _imageLoadService.LoadAvatarImage(model.AvatarFile).ConfigureAwait(false);

            await _userDao.Update(user).ConfigureAwait(false);

            _logger.LogInfo("AccountController.ResetPassword(). User.AvatarUrl updated.", UserContext.UserId);

            return RedirectToAction("Index", "Account");
        }

        private async Task EmailConfirmationProcess(int userId)
        {
            var user = await _userDao.Get(userId).ConfigureAwait(false);

            var code = await _userTokenService.Create(userId, TokenType.EmailConfirmation).ConfigureAwait(false);

            var emailConfirmUrl = Url.Action("ConfirmEmail", "Account", new {userId, code}, HttpContext.Request.Scheme);

            await _mailService.SendFromAdmin(user.Email, "Подтверждение почты",
                    $"Пожалуйста, подтвердите свою почту, нажав на <a href='{emailConfirmUrl}'>ссылку</a>.")
                .ConfigureAwait(false);
        }

        private async Task PhoneConfirmationProcess(int userId)
        {
            var user = await _userDao.Get(userId).ConfigureAwait(false);

            var code = await _userTokenService.Create(userId, TokenType.PhoneConfirmation).ConfigureAwait(false);

            await _smsService.Send(user.Phone, code).ConfigureAwait(false);
        }

        private async Task PasswordResetProcess(int userId)
        {
            var user = await _userDao.Get(userId).ConfigureAwait(false);

            var code = await _userTokenService.Create(user.Id, TokenType.PasswordReset).ConfigureAwait(false);

            var passwordResetUrl = Url.Action("ResetPassword", "Account", new { userId = user.Id, code }, HttpContext.Request.Scheme);

            await _mailService.SendFromAdmin(user.Email, "Сброс пароля",
                    $"Для сброса пароля нажмите на <a href='{passwordResetUrl}'>ссылку</a>.")
                .ConfigureAwait(false);
        }
    }
}