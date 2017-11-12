//using System;
//using System.Threading.Tasks;
//using Aklion.Crm.Attributes;
//using Aklion.Crm.Enums;
//using Aklion.Infrastructure.Utils.DateTime;
//using Aklion.Infrastructure.Utils.Password;
//using Aklion.Infrastructure.Utils.Token;
//using Dapper;
//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;

//namespace Aklion.Crm.Controllers
//{
//    public class AccountController : BaseController
//    {
//        private readonly IMailService _mailService;
//        private readonly ISmsService _smsService;
//        private readonly IImageLoadService _imageLoadService;
//        private readonly IIdentityTokenRepository _identityTokenRepository;
//        private readonly IAdministratorRepository _administratorRepository;
//        private readonly IGamerActionRepository _gamerActionRepository;

//        public AccountController(
//            IIdentityRepository identityRepository,
//            IIdentityRoleRepository identityRoleRepository,
//            IAdministratorRepository administratorRepository,
//            IGamerRepository gamerRepository,
//            IServerRepository serverRepository,
//            IMailService mailService,
//            ISmsService smsService,
//            IImageLoadService imageLoadService,
//            IIdentityTokenRepository identityTokenRepository,
//            IGamerActionRepository gamerActionRepository,
//            IContextRepository contextRepository)
//            : base(
//                identityRepository,
//                identityRoleRepository,
//                administratorRepository,
//                gamerRepository,
//                serverRepository,
//                contextRepository)
//        {
//            _mailService = mailService;
//            _smsService = smsService;
//            _imageLoadService = imageLoadService;
//            _identityTokenRepository = identityTokenRepository;
//            _gamerActionRepository = gamerActionRepository;
//            _administratorRepository = administratorRepository;
//        }

//        [Auth]
//        [HttpGet]
//        public IActionResult Index()
//        {
//            return View(CurrentGamer);
//        }

//        [HttpGet]
//        [AllowAnonymous]
//        public IActionResult Login(string returnUrl = null)
//        {
//            ViewData["ReturnUrl"] = returnUrl;

//            if (ViewBag.IsAuthenticated)
//            {
//                return RedirectToLocal(returnUrl);
//            }

//            return View();
//        }

//        [HttpPost]
//        [AllowAnonymous]
//        [ValidateAntiForgeryToken]
//        public async Task<IActionResult> Login(LoginViewModel model, string returnUrl = null)
//        {
//            ViewData["ReturnUrl"] = returnUrl;

//            if (!ModelState.IsValid)
//                return View(model);

//            var identity = await IdentityRepository.SelectByLoginAsync(model.Login).ConfigureAwait(false);
//            if (identity == null)
//            {
//                ModelState.AddModelError(string.Empty, "Неправильный логин или пароль");
//                return View(model);
//            }

//            if (!PasswordHelper.Verify(model.Password, identity.PasswordHash))
//            {
//                ModelState.AddModelError(string.Empty, "Неправильный логин или пароль");
//                return View(model);
//            }

//            await SignInAsync(identity, model.RememberMe).ConfigureAwait(false);

//            if (CurrentGamer != null)
//            {
//                var gamerAction = new GamerAction
//                {
//                    GamerId = CurrentGamer.Id,
//                    ActionType = GamerActionType.LogIn
//                };

//                _gamerActionRepository.InsertAsync(gamerAction);
//            }

//            return RedirectToLocal(returnUrl);
//        }

//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        [Authorize(Roles = RoleExtension.All)]
//        public async Task<IActionResult> LogOff()
//        {
//            if (CurrentGamer != null)
//            {
//                var gamerAction = new GamerAction
//                {
//                    GamerId = CurrentGamer.Id,
//                    ActionType = GamerActionType.LogOff
//                };

//                _gamerActionRepository.InsertAsync(gamerAction);
//            }
//            await SignOutAsync().ConfigureAwait(false);

//            return RedirectToAction("Index", "Home");
//        }

//        [HttpGet]
//        [AllowAnonymous]
//        public IActionResult Register(string returnUrl = null)
//        {
//            ViewData["ReturnUrl"] = returnUrl;

//            var model = new RegisterViewModel
//            {
//                Gender = Gender.Male,
//                BirthDateString = GetDefaultRegistrationBirthDateString()
//            };

//            return View(model);
//        }

//        [HttpPost]
//        [AllowAnonymous]
//        [ValidateAntiForgeryToken]
//        public async Task<IActionResult> Register(RegisterViewModel model, string returnUrl = null)
//        {
//            ViewData["ReturnUrl"] = returnUrl;

//            if (!ModelState.IsValid)
//                return View(model);

//            if (model.GameStartHour >= model.GameEndHour)
//            {
//                ModelState.AddModelError("GameStartHour", "Время игры указано неверно");
//                ModelState.AddModelError("GameEndHour", "Время игры указано неверно");
//                return View(model);
//            }

//            var isExistByLogin = await IdentityRepository.IsExistByLoginAsync(model.Login).ConfigureAwait(false);
//            if (isExistByLogin)
//            {
//                ModelState.AddModelError("Login", "Логин уже занят");
//                return View(model);
//            }

//            var isExistByNik = await GamerRepository.IsExistByNikAsync(model.Nik).ConfigureAwait(false);
//            if (isExistByNik)
//            {
//                ModelState.AddModelError("Nik", "Ник уже занят");
//                return View(model);
//            }

//            var isExistByEmail = await GamerRepository.IsExistByEmailAsync(model.Email).ConfigureAwait(false) ||
//                                 await _administratorRepository.IsExistByEmailAsync(model.Email).ConfigureAwait(false);
//            if (isExistByEmail)
//            {
//                ModelState.AddModelError("Email", "Email уже занят");
//                return View(model);
//            }

//            var isExistByPhone = await GamerRepository.IsExistByPhoneAsync(model.Phone).ConfigureAwait(false) ||
//                                 await _administratorRepository.IsExistByPhoneAsync(model.Phone).ConfigureAwait(false);
//            if (isExistByPhone)
//            {
//                ModelState.AddModelError("Phone", "Телефон уже занят");
//                return View(model);
//            }

//            var identity = new SqlMapper.Identity
//            {
//                Login = model.Login,
//                PasswordHash = PasswordHelper.GenerateAlphaNumbericString(model.Password)
//            };

//            await IdentityRepository.InsertAsync(identity).ConfigureAwait(false);

//            var gamer = new Gamer
//            {
//                IdentityId = identity.Id,
//                Nik = model.Nik,
//                Name = model.Name,
//                Email = model.Email,
//                Phone = model.Phone,
//                Gender = model.Gender,
//                BirthDate = model.BirthDateString.ToDate(),
//                CityId = model.CityId,
//                Chronicle = model.Chronicle,
//                Reith = model.Reith,
//                Style = model.Style,
//                GameRole = model.GameRole,
//                GameStartHour = model.GameStartHour,
//                GameEndHour = model.GameEndHour,
//                Rating = 0,
//                IsEmailConfirmed = false,
//                IsPhoneConfirmed = false,
//                IsDeleted = false,
//                HasVote = true
//            };

//            await GamerRepository.InsertAsync(gamer).ConfigureAwait(false);

//            var identityRole = new IdentityRole
//            {
//                IdentityId = identity.Id,
//                Role = Role.Gamer
//            };

//            await IdentityRoleRepository.InsertAsync(identityRole).ConfigureAwait(false);

//            var token = await CreateIdentityTokenForEmailConfirmation(identity.Id).ConfigureAwait(false);

//            var emailConfirmUrl = CreateEmailConfirmUrl(token);

//            await SendConfirmationEmailUrlAsync(gamer.Email, emailConfirmUrl).ConfigureAwait(false);

//            await SignInAsync(identity, true).ConfigureAwait(false);

//            var gamerAction = new GamerAction
//            {
//                GamerId = CurrentGamer.Id,
//                ActionType = GamerActionType.Register
//            };

//            _gamerActionRepository.InsertAsync(gamerAction);

//            return RedirectToAction("Index", "Home",
//                new {message = "На Вашу электронную почту отправлено письмо с подтверждением"});
//        }

//        [HttpGet]
//        [Authorize(Roles = RoleExtension.GamerName)]
//        public async Task<IActionResult> SendConfirmEmail()
//        {
//            var token = await CreateIdentityTokenForEmailConfirmation(CurrentIdentity.Id).ConfigureAwait(false);

//            var emailConfirmUrl = CreateEmailConfirmUrl(token);

//            await SendConfirmationEmailUrlAsync(CurrentGamer.Email, emailConfirmUrl).ConfigureAwait(false);

//            return RedirectToAction("Index",
//                new {message = "На Вашу электронную почту отправлено письмо с подтверждением"});
//        }

//        [HttpGet]
//        [Authorize(Roles = RoleExtension.GamerName)]
//        public async Task<IActionResult> SendSmsCode()
//        {
//            var token = await CreateIdentityTokenForPhoneConfirmation(CurrentIdentity.Id).ConfigureAwait(false);

//            var phoneConfirmCode = CreatePhoneConfirmCode(token);

//            await SendConfirmationSmsCode(CurrentGamer.Phone, phoneConfirmCode).ConfigureAwait(false);

//            return RedirectToAction("VerifySmsCode",
//                new {message = "На Ваш номер телефона отправлен SMS с кодом подтверждения"});
//        }

//        [HttpGet]
//        [AllowAnonymous]
//        public async Task<IActionResult> ConfirmEmail(int identityId, string code)
//        {
//            if (identityId == 0 || string.IsNullOrEmpty(code))
//                return View("Error");

//            var identity = await IdentityRepository.SelectAsync(identityId).ConfigureAwait(false);
//            if (identity == null)
//                return View("Error");

//            var identityToken = await _identityTokenRepository.SelectAsync(identityId, code,
//                TokenType.EmailConfirmation).ConfigureAwait(false);
//            if (identityToken == null)
//                return View("Error");

//            if (identityToken.ExpirationDate < DateTime.Now || identityToken.IsUsed)
//                return View("Error");

//            await _identityTokenRepository.SetUsedAsync(identityToken.Id).ConfigureAwait(false);

//            var gamer = await GamerRepository.SelectByIdentityIdAsync(identityId).ConfigureAwait(false);

//            gamer.IsEmailConfirmed = true;

//            await GamerRepository.UpdateAsync(gamer).ConfigureAwait(false);

//            var gamerAction = new GamerAction
//            {
//                GamerId = CurrentGamer.Id,
//                ActionType = GamerActionType.ConfirmEmail
//            };

//            _gamerActionRepository.InsertAsync(gamerAction);

//            return RedirectToAction("Index", "Home", new {message = "Ваш Email подтвержден"});
//        }

//        [HttpGet]
//        [Authorize(Roles = RoleExtension.GamerName)]
//        public IActionResult ChangePassword()
//        {
//            return View();
//        }

//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        [Authorize(Roles = RoleExtension.GamerName)]
//        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
//        {
//            if (!ModelState.IsValid)
//                return View(model);

//            var isOldPasswordVerified = PasswordHelper.Verify(model.OldPassword, CurrentIdentity.PasswordHash);
//            if (!isOldPasswordVerified)
//            {
//                ModelState.AddModelError("OldPassword", "Старый пароль введен неверно");
//                return View(model);
//            }

//            CurrentIdentity.PasswordHash = PasswordHelper.GenerateAlphaNumbericString(model.Password);
//            await IdentityRepository.UpdateAsync(CurrentIdentity).ConfigureAwait(false);

//            await SignInAsync().ConfigureAwait(false);

//            var gamerAction = new GamerAction
//            {
//                GamerId = CurrentGamer.Id,
//                ActionType = GamerActionType.ChangePassword
//            };

//            _gamerActionRepository.InsertAsync(gamerAction);

//            return RedirectToAction("Index", new {message = "Ваш пароль успешно изменен"});
//        }

//        [HttpGet]
//        [Authorize(Roles = RoleExtension.GamerName)]
//        public IActionResult ChangeEmail()
//        {
//            return View();
//        }

//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        [Authorize(Roles = RoleExtension.GamerName)]
//        public async Task<IActionResult> ChangeEmail(ChangeEmailViewModel model)
//        {
//            if (!ModelState.IsValid)
//                return View(model);

//            var isExistByEmail = await GamerRepository.IsExistByEmailAsync(model.Email).ConfigureAwait(false);
//            if (isExistByEmail)
//            {
//                ModelState.AddModelError("Email", "Email уже занят");
//                return View(model);
//            }

//            CurrentGamer.Email = model.Email;
//            CurrentGamer.IsEmailConfirmed = false;

//            await GamerRepository.UpdateAsync(CurrentGamer).ConfigureAwait(false);

//            var token = await CreateIdentityTokenForEmailConfirmation(CurrentIdentity.Id).ConfigureAwait(false);

//            var emailConfirmUrl = CreateEmailConfirmUrl(token);

//            await SendConfirmationEmailUrlAsync(CurrentGamer.Email, emailConfirmUrl).ConfigureAwait(false);

//            var gamerAction = new GamerAction
//            {
//                GamerId = CurrentGamer.Id,
//                ActionType = GamerActionType.ChangeEmail
//            };

//            _gamerActionRepository.InsertAsync(gamerAction);

//            return RedirectToAction("Index",
//                new
//                {
//                    message = "Ваш Email успешно изменен. На Вашу электронную почту отправлено письмо с подтверждением"
//                });
//        }

//        [HttpGet]
//        [Authorize(Roles = RoleExtension.GamerName)]
//        public IActionResult ChangePhone()
//        {
//            return View();
//        }

//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        [Authorize(Roles = RoleExtension.GamerName)]
//        public async Task<IActionResult> ChangePhone(ChangePhoneViewModel model)
//        {
//            if (!ModelState.IsValid)
//                return View(model);

//            var isExistByPhone = await GamerRepository.IsExistByPhoneAsync(model.Phone).ConfigureAwait(false);
//            if (isExistByPhone)
//            {
//                ModelState.AddModelError("Phone", "Номер телефона уже занят");
//                return View(model);
//            }

//            CurrentGamer.Phone = model.Phone;
//            CurrentGamer.IsPhoneConfirmed = false;

//            await GamerRepository.UpdateAsync(CurrentGamer).ConfigureAwait(false);

//            var token = await CreateIdentityTokenForPhoneConfirmation(CurrentIdentity.Id).ConfigureAwait(false);

//            var phoneConfirmCode = CreatePhoneConfirmCode(token);

//            await SendConfirmationSmsCode(CurrentGamer.Phone, phoneConfirmCode).ConfigureAwait(false);

//            var gamerAction = new GamerAction
//            {
//                GamerId = CurrentGamer.Id,
//                ActionType = GamerActionType.ChangePhone
//            };

//            _gamerActionRepository.InsertAsync(gamerAction);

//            return RedirectToAction("VerifySmsCode",
//                new
//                {
//                    message =
//                    "Ваш номер телефона успешно изменен. На Ваш номер телефона отправлен SMS с кодом подтверждения"
//                });
//        }

//        [HttpGet]
//        [Authorize(Roles = RoleExtension.GamerName)]
//        public IActionResult VerifySmsCode(string message = null)
//        {
//            ViewData["StatusMessage"] = message;

//            return View();
//        }

//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        [Authorize(Roles = RoleExtension.GamerName)]
//        public async Task<IActionResult> VerifySmsCode(VerifySmsCodeViewModel model)
//        {
//            if (!ModelState.IsValid)
//                return View(model);

//            var identityToken = await _identityTokenRepository.SelectAsync(
//                CurrentIdentity.Id, model.Code,
//                TokenType.PhoneConfirmation).ConfigureAwait(false);
//            if (identityToken == null)
//                return View("Error");

//            if (identityToken.ExpirationDate < DateTime.Now || identityToken.IsUsed)
//                return View("Error");

//            await _identityTokenRepository.SetUsedAsync(identityToken.Id).ConfigureAwait(false);

//            CurrentGamer.IsPhoneConfirmed = true;

//            await GamerRepository.UpdateAsync(CurrentGamer).ConfigureAwait(false);

//            var gamerAction = new GamerAction
//            {
//                GamerId = CurrentGamer.Id,
//                ActionType = GamerActionType.ConfirmPhone
//            };

//            _gamerActionRepository.InsertAsync(gamerAction);

//            return RedirectToAction("Index", new {message = "Ваш номер телефона подтвержден"});
//        }

//        [HttpGet]
//        [Authorize(Roles = RoleExtension.GamerName)]
//        public IActionResult ChangePersonalInfo()
//        {
//            var model = new ChangePersonalInfoViewModel
//            {
//                Nik = CurrentGamer.Nik,
//                Name = CurrentGamer.Name,
//                Gender = CurrentGamer.Gender,
//                BirthDateString = CurrentGamer.BirthDate?.ToDateString(),
//                CityId = CurrentGamer.CityId,
//                CityName = CurrentGamer.CityName,
//                Chronicle = CurrentGamer.Chronicle,
//                Reith = CurrentGamer.Reith,
//                Style = CurrentGamer.Style,
//                GameRole = CurrentGamer.GameRole,
//                GameStartHour = CurrentGamer.GameStartHour ?? 0,
//                GameEndHour = CurrentGamer.GameEndHour ?? 0
//            };

//            return View(model);
//        }

//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        [Authorize(Roles = RoleExtension.GamerName)]
//        public async Task<IActionResult> ChangePersonalInfo(ChangePersonalInfoViewModel model)
//        {
//            if (!ModelState.IsValid)
//                return View(model);

//            if (model.GameStartHour >= model.GameEndHour)
//            {
//                ModelState.AddModelError("GameStartHour", "Время игры указано неверно");
//                ModelState.AddModelError("GameEndHour", "Время игры указано неверно");
//                return View(model);
//            }

//            var isExistByNik = CurrentGamer.Nik != model.Nik &&
//                               await GamerRepository.IsExistByNikAsync(model.Nik).ConfigureAwait(false);
//            if (isExistByNik)
//            {
//                ModelState.AddModelError("Nik", "Ник уже занят");
//                return View(model);
//            }

//            CurrentGamer.Nik = model.Nik;
//            CurrentGamer.Name = model.Name;
//            CurrentGamer.Gender = model.Gender;
//            CurrentGamer.BirthDate = model.BirthDateString.ToDate();
//            CurrentGamer.CityId = model.CityId;
//            CurrentGamer.Chronicle = model.Chronicle;
//            CurrentGamer.Reith = model.Reith;
//            CurrentGamer.Style = model.Style;
//            CurrentGamer.GameRole = model.GameRole;
//            CurrentGamer.GameStartHour = model.GameStartHour;
//            CurrentGamer.GameEndHour = model.GameEndHour;

//            await GamerRepository.UpdateAsync(CurrentGamer).ConfigureAwait(false);

//            var gamerAction = new GamerAction
//            {
//                GamerId = CurrentGamer.Id,
//                ActionType = GamerActionType.ChangePersonalInfo
//            };

//            _gamerActionRepository.InsertAsync(gamerAction);

//            return RedirectToAction("Index", new {message = "Ваши изменения сохранены"});
//        }

//        [HttpGet]
//        [AllowAnonymous]
//        public IActionResult ForgotPassword()
//        {
//            return View();
//        }

//        [HttpPost]
//        [AllowAnonymous]
//        [ValidateAntiForgeryToken]
//        public async Task<IActionResult> ForgotPassword(ForgotPasswordViewModel model)
//        {
//            if (!ModelState.IsValid)
//                return View(model);

//            var gamer = await GamerRepository.SelectByEmailAsync(model.Email).ConfigureAwait(false);
//            if (gamer == null)
//            {
//                ModelState.AddModelError("Email", "Пользователь с указанным Email не найден");
//                return View(model);
//            }

//            var token = await CreateIdentityTokenForPasswordReset(gamer.IdentityId).ConfigureAwait(false);

//            var passwordResetUrl = CreatePasswordResetUrl(token);

//            await SendPasswordResetUrlAsync(gamer.Email, passwordResetUrl).ConfigureAwait(false);

//            return RedirectToAction("ForgotPasswordConfirmation",
//                new {message = "На Вашу электронную почту отправлено письмо со ссылкой на форму сброса пароля"});
//        }

//        [HttpGet]
//        [AllowAnonymous]
//        public IActionResult ForgotPasswordConfirmation()
//        {
//            return View();
//        }

//        [HttpGet]
//        [AllowAnonymous]
//        public async Task<IActionResult> ResetPassword(int identityId, string code)
//        {
//            if (identityId == 0 || string.IsNullOrEmpty(code))
//                return View("Error");

//            var identity = await IdentityRepository.SelectAsync(identityId).ConfigureAwait(false);
//            if (identity == null)
//                return View("Error");

//            var model = new ResetPasswordViewModel
//            {
//                Code = code
//            };

//            return View(model);
//        }

//        [HttpPost]
//        [AllowAnonymous]
//        [ValidateAntiForgeryToken]
//        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel model)
//        {
//            if (!ModelState.IsValid)
//                return View(model);

//            var gamer = await GamerRepository.SelectByEmailAsync(model.Email).ConfigureAwait(false);
//            if (gamer == null)
//            {
//                ModelState.AddModelError("Email", "Пользователь с указанным Email не найден");
//                return View(model);
//            }

//            var identityToken = await _identityTokenRepository.SelectAsync(
//                gamer.IdentityId, model.Code,
//                TokenType.PasswordReset).ConfigureAwait(false);
//            if (identityToken == null)
//                return View("Error");

//            if (identityToken.ExpirationDate < DateTime.Now || identityToken.IsUsed)
//                return View("Error");

//            await _identityTokenRepository.SetUsedAsync(identityToken.Id).ConfigureAwait(false);

//            var identity = await IdentityRepository.SelectAsync(gamer.IdentityId).ConfigureAwait(false);

//            identity.PasswordHash = PasswordHelper.GenerateAlphaNumbericString(model.Password);

//            await IdentityRepository.UpdateAsync(identity).ConfigureAwait(false);

//            var gamerAction = new GamerAction
//            {
//                GamerId = CurrentGamer.Id,
//                ActionType = GamerActionType.ResetPassword
//            };

//            _gamerActionRepository.InsertAsync(gamerAction);

//            return RedirectToAction("ResetPasswordConfirmation");
//        }

//        [HttpGet]
//        [AllowAnonymous]
//        public IActionResult ResetPasswordConfirmation()
//        {
//            return View();
//        }

//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        [Authorize(Roles = RoleExtension.GamerName)]
//        public async Task<IActionResult> LoadAvatar(LoadAvatarViewModel model)
//        {
//            if (!model.AvatarFile.IsImage())
//                return View("Error");

//            CurrentGamer.AvatarUrl = await _imageLoadService.LoadAvatarAsync(model.AvatarFile).ConfigureAwait(false);

//            await GamerRepository.UpdateAsync(CurrentGamer).ConfigureAwait(false);

//            var gamerAction = new GamerAction
//            {
//                GamerId = CurrentGamer.Id,
//                ActionType = GamerActionType.LoadAvatar
//            };

//            _gamerActionRepository.InsertAsync(gamerAction);

//            return RedirectToAction("Index", "Account");
//        }

//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        [Authorize(Roles = RoleExtension.GamerName)]
//        public async Task<object> LoadBackground(LoadBackgroundViewModel model)
//        {
//            if (!model.BackgroundFile.IsImage())
//                return View("Error");

//            CurrentGamer.BackgroundUrl =
//                await _imageLoadService.LoadBackgroundAsync(model.BackgroundFile).ConfigureAwait(false);

//            await GamerRepository.UpdateAsync(CurrentGamer).ConfigureAwait(false);

//            var gamerAction = new GamerAction
//            {
//                GamerId = CurrentGamer.Id,
//                ActionType = GamerActionType.LoadBackground
//            };

//            _gamerActionRepository.InsertAsync(gamerAction);

//            return RedirectToAction("Index", "Account");
//        }

//        private IActionResult RedirectToLocal(string returnUrl)
//        {
//            if (Url.IsLocalUrl(returnUrl))
//                return Redirect(returnUrl);

//            return RedirectToAction("Index", "Home");
//        }

//        private async Task<IdentityToken> CreateIdentityTokenForEmailConfirmation(int identityId)
//        {
//            var token = new IdentityToken
//            {
//                IdentityId = identityId,
//                TokenType = TokenType.EmailConfirmation,
//                Token = TokenHelper.GenerateToken(TokenType.EmailConfirmation),
//                ExpirationDate = TokenHelper.GetExpirationDate(TokenType.EmailConfirmation),
//                IsUsed = false
//            };

//            await _identityTokenRepository.InsertAsync(token).ConfigureAwait(false);

//            return token;
//        }

//        private async Task<IdentityToken> CreateIdentityTokenForPasswordReset(int identityId)
//        {
//            var token = new IdentityToken
//            {
//                IdentityId = identityId,
//                TokenType = TokenType.PasswordReset,
//                Token = TokenHelper.GenerateToken(TokenType.PasswordReset),
//                ExpirationDate = TokenHelper.GetExpirationDate(TokenType.PasswordReset),
//                IsUsed = false
//            };

//            await _identityTokenRepository.InsertAsync(token).ConfigureAwait(false);

//            return token;
//        }

//        private async Task<IdentityToken> CreateIdentityTokenForPhoneConfirmation(int identityId)
//        {
//            var token = new IdentityToken
//            {
//                IdentityId = identityId,
//                TokenType = TokenType.PhoneConfirmation,
//                Token = TokenHelper.GenerateToken(TokenType.PhoneConfirmation),
//                ExpirationDate = TokenHelper.GetExpirationDate(TokenType.PhoneConfirmation),
//                IsUsed = false
//            };

//            await _identityTokenRepository.InsertAsync(token).ConfigureAwait(false);

//            return token;
//        }

//        private string CreateEmailConfirmUrl(IdentityToken token)
//        {
//            return Url.Action("ConfirmEmail", "Account",
//                new {identityId = token.IdentityId, code = token.Token}, HttpContext.Request.Scheme);
//        }

//        private string CreatePasswordResetUrl(IdentityToken token)
//        {
//            return Url.Action("ResetPassword", "Account",
//                new {identityId = token.IdentityId, code = token.Token}, HttpContext.Request.Scheme);
//        }

//        private static string CreatePhoneConfirmCode(IdentityToken token)
//        {
//            return token.Token;
//        }

//        private async Task SendConfirmationEmailUrlAsync(string email, string emailConfirmUrl)
//        {
//            await _mailService.SendAsync("Администрация игрового сайта", email, "Подтверждение почты",
//                    $"Пожалуйста, подтвердите свою почту, нажав на <a href='{emailConfirmUrl}'>ссылку</a>.")
//                .ConfigureAwait(false);
//        }

//        private async Task SendPasswordResetUrlAsync(string email, string passwordResetUrl)
//        {
//            await _mailService.SendAsync("Администрация игрового сайта", email, "Сброс пароля",
//                    $"Для сброса пароля нажмите на <a href='{passwordResetUrl}'>ссылку</a>.")
//                .ConfigureAwait(false);
//        }

//        private Task SendConfirmationSmsCode(string phoneNumber, string code)
//        {
//            return _smsService.SendAsync(phoneNumber, code);
//        }

//        public static string GetDefaultRegistrationBirthDateString()
//        {
//            return DateTime.Today.AddYears(-18).FirstDayOfYear().ToDateString();
//        }
//    }
//}