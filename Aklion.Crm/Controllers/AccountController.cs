﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Aklion.Crm.Business.AuditLog;
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
using Aklion.Infrastructure.Logger;
using Aklion.Infrastructure.Password;
using Aklion.Infrastructure.PhoneNumber;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Aklion.Crm.Controllers
{
    public class AccountController : BaseController
    {
        private readonly ILogger _logger;
        private readonly IAuditLogService _auditLogService;
        private readonly IMailService _mailService;
        private readonly ISmsService _smsService;
        private readonly IImageLoadService _imageLoadService;
        private readonly IUserTokenService _userTokenService;
        private readonly IUserPermissionService _userPermissionService;
        private readonly IUserDao _userDao;
        private readonly IUserContextDao _userContextDao;
        private readonly IStoreDao _storeDao;

        public AccountController(
            ILogger logger,
            IAuditLogService auditLogService,
            IMailService mailService,
            ISmsService smsService,
            IImageLoadService imageLoadService,
            IUserTokenService userTokenService,
            IUserPermissionService userPermissionService,
            IUserDao userDao,
            IUserContextDao userContextDao,
            IStoreDao storeDao)
        {
            _logger = logger;
            _auditLogService = auditLogService;
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

            var user = await _userDao.GetByLoginAsync(model.Login).ConfigureAwait(false);
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

            await SignInAsync(user.Id, 0, model.RememberMe).ConfigureAwait(false);

            var userContextDomain = await _userContextDao.GetAsync(user.Id, 0).ConfigureAwait(false);
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
            var userId = UserContext.UserId;

            await SignOutAsync().ConfigureAwait(false);

            _logger.LogInfo("AccountController.LogOff(). Signed out.", 0, new
            {
                UserId = userId
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
                Name = $"Магазин пользователя {user.Login}",
                ApiSecret = null,
                IsLocked = false,
                IsDeleted = false,
                CreateDate = DateTime.Now,
                ModifyDate = null
            };

            store.Id = await _storeDao.CreateAsync(store).ConfigureAwait(false);

            await _userPermissionService.CreateForRegisteredUserAsync(user.Id, store.Id).ConfigureAwait(false);

            _auditLogService.LogInserting(0, 0, user);

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

            _logger.LogInfo("AccountController.SendConfirmEmail(). Email confirmation link sended.",
                UserContext.UserId);

            return RedirectToAction("Index",
                new {message = "На Вашу электронную почту отправлено письмо с подтверждением"});
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> SendSmsCode()
        {
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

            var user = await _userDao.GetAsync(userId).ConfigureAwait(false);
            if (user == null)
            {
                _logger.LogWarning("AccountController.ConfirmEmail(). User not found.", 0, new
                {
                    UserId = userId,
                    Code = code
                });

                return View("Error");
            }

            var isTokenConfirm = await _userTokenService.ConfirmAsync(userId, TokenType.EmailConfirmation, code)
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

            var oldUserClone = user.Clone();

            user.IsEmailConfirmed = true;

            await _userDao.UpdateAsync(user).ConfigureAwait(false);

            _auditLogService.LogUpdating(UserContext.UserId, UserContext.StoreId, oldUserClone, user);

            _logger.LogInfo("AccountController.ConfirmEmail(). User.IsEmailConfirmed = true.", 0, new
            {
                UserId = userId,
                Code = code
            });

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
                _logger.LogWarning("AccountController.ChangePassword(). ModelState is invalid.", UserContext.UserId);

                return View(model);
            }

            var user = await _userDao.GetAsync(UserContext.UserId).ConfigureAwait(false);
            if (user == null)
            {
                _logger.LogWarning("AccountController.ChangePassword(). User not found.", UserContext.UserId);

                return View(model);
            }

            var isOldPasswordVerified = PasswordHelper.Verify(model.OldPassword, user.PasswordHash);
            if (!isOldPasswordVerified)
            {
                ModelState.AddModelError("OldPassword", "Старый пароль введен неверно");

                _logger.LogWarning("AccountController.ChangePassword(). Old password is incorrect.",
                    UserContext.UserId);

                return View(model);
            }

            var oldUserClone = user.Clone();

            user.PasswordHash = PasswordHelper.Generate(model.Password);

            await _userDao.UpdateAsync(user).ConfigureAwait(false);

            _auditLogService.LogUpdating(UserContext.UserId, UserContext.StoreId, oldUserClone, user);

            _logger.LogInfo("AccountController.ChangePassword(). Password changed.", UserContext.UserId);

            await SignInAsync(user.Id, 0, true).ConfigureAwait(false);

            _logger.LogInfo("AccountController.ChangePassword(). Signed in.", UserContext.UserId);

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
                _logger.LogWarning("AccountController.ChangeEmail(). ModelState is invalid.", UserContext.UserId, new
                {
                    model.Email
                });

                return View(model);
            }

            var user = await _userDao.GetAsync(UserContext.UserId).ConfigureAwait(false);
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

            var isExistByEmail = await _userDao.IsExistByEmailAsync(model.Email).ConfigureAwait(false);
            if (isExistByEmail)
            {
                ModelState.AddModelError("Email", "Email уже занят");

                _logger.LogWarning("AccountController.ChangeEmail(). Email is exist.", UserContext.UserId, new
                {
                    model.Email
                });

                return View(model);
            }

            var oldUserClone = user.Clone();

            user.Email = model.Email;
            user.IsEmailConfirmed = false;

            await _userDao.UpdateAsync(user).ConfigureAwait(false);

            _auditLogService.LogUpdating(UserContext.UserId, UserContext.StoreId, oldUserClone, user);

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
                _logger.LogWarning("AccountController.ChangePhone(). ModelState is invalid.", UserContext.UserId, new
                {
                    model.Phone
                });

                return View(model);
            }

            var user = await _userDao.GetAsync(UserContext.UserId).ConfigureAwait(false);
            if (user == null)
            {
                _logger.LogWarning("AccountController.ChangePhone(). User not found.", UserContext.UserId);

                return View(model);
            }

            var isExistByPhone = await _userDao.IsExistByPhoneAsync(model.Phone).ConfigureAwait(false);
            if (isExistByPhone)
            {
                ModelState.AddModelError("Phone", "Номер телефона уже занят");

                _logger.LogWarning("AccountController.ChangePhone(). Phone is exist.", UserContext.UserId, new
                {
                    model.Phone
                });

                return View(model);
            }

            var oldUserClone = user.Clone();

            user.Phone = model.Phone.ExtractPhoneNumber();
            user.IsPhoneConfirmed = false;

            await _userDao.UpdateAsync(user).ConfigureAwait(false);

            _auditLogService.LogUpdating(UserContext.UserId, UserContext.StoreId, oldUserClone, user);

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
                _logger.LogWarning("AccountController.VerifySmsCode(). ModelState is invalid.", UserContext.UserId, new
                {
                    model.Code
                });

                return View(model);
            }

            var user = await _userDao.GetAsync(UserContext.UserId).ConfigureAwait(false);
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
                .ConfirmAsync(UserContext.UserId, TokenType.PhoneConfirmation, model.Code).ConfigureAwait(false);

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

            var oldUserClone = user.Clone();

            user.IsPhoneConfirmed = true;

            await _userDao.UpdateAsync(user).ConfigureAwait(false);

            _auditLogService.LogUpdating(UserContext.UserId, UserContext.StoreId, oldUserClone, user);

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
            var user = await _userDao.GetAsync(UserContext.UserId).ConfigureAwait(false);
            if (user == null)
            {
                _logger.LogWarning("AccountController.ChangePersonalInfo(). User not found.", 0, new
                {
                    UserContext.UserId
                });

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
                _logger.LogWarning("AccountController.ChangePersonalInfo(). ModelState is invalid.", UserContext.UserId,
                    model);

                return View(model);
            }

            var user = await _userDao.GetAsync(UserContext.UserId).ConfigureAwait(false);
            if (user == null)
            {
                _logger.LogWarning("AccountController.ChangePersonalInfo(). User not found.", 0, new
                {
                    UserContext.UserId
                });

                return View("Error");
            }

            var oldUserClone = user.Clone();

            user.MapFrom(model);

            await _userDao.UpdateAsync(user).ConfigureAwait(false);

            _auditLogService.LogUpdating(UserContext.UserId, UserContext.StoreId, oldUserClone, user);

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

            var user = await _userDao.GetByEmailAsync(model.Email).ConfigureAwait(false);
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
        public IActionResult ResetPassword(int userId, string code)
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

            var user = await _userDao.GetByEmailAsync(model.Email).ConfigureAwait(false);
            if (user == null)
            {
                ModelState.AddModelError("Email", "Пользователь с указанным Email не найден");

                _logger.LogWarning("AccountController.ResetPassword(). User not found.", 0, model);

                return View(model);
            }

            var isTokenConfirm = await _userTokenService.ConfirmAsync(user.Id, TokenType.PasswordReset, model.Code)
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

            var oldUserClone = user.Clone();

            user.PasswordHash = PasswordHelper.Generate(model.Password);

            await _userDao.UpdateAsync(user).ConfigureAwait(false);

            _auditLogService.LogUpdating(UserContext.UserId, UserContext.StoreId, oldUserClone, user);

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
            if (!model.AvatarFile.FileName.IsImage())
            {
                return View("Error");
            }

            var user = await _userDao.GetAsync(UserContext.UserId).ConfigureAwait(false);
            if (user == null)
            {
                _logger.LogWarning("AccountController.LoadAvatar(). User not found.", UserContext.UserId);

                return View("Error");
            }

            user.AvatarUrl = await _imageLoadService.LoadAvatarImageAsync(model.AvatarFile).ConfigureAwait(false);

            await _userDao.UpdateAsync(user).ConfigureAwait(false);

            _logger.LogInfo("AccountController.ResetPassword(). User.AvatarUrl updated.", UserContext.UserId);

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