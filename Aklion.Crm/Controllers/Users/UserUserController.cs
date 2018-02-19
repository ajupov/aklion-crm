using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Aklion.Crm.Attributes;
using Aklion.Crm.Business.AuditLog;
using Aklion.Crm.Business.Mail;
using Aklion.Crm.Business.UserPermission;
using Aklion.Crm.Business.UserToken;
using Aklion.Crm.Dao.User;
using Aklion.Crm.Dao.UserPermission;
using Aklion.Crm.Enums;
using Aklion.Crm.Mappers.User.User;
using Aklion.Crm.Models;
using Aklion.Crm.Models.User.User;
using Aklion.Infrastructure.Password;
using Aklion.Infrastructure.PhoneNumber;
using Aklion.Infrastructure.Random;

namespace Aklion.Crm.Controllers.UsersControllers
{
    [Route("Users")]
    public class UserUserController : BaseController
    {
        private readonly IAuditLogger _auditLogService;
        private readonly IMailService _mailService;
        private readonly IUserTokenService _userTokenService;
        private readonly IUserPermissionService _userPermissionService;
        private readonly IUserPermissionDao _userPermissionDao;
        private readonly IUserDao _userDao;

        public UserUserController(
            IAuditLogger auditLogService,
            IMailService mailService,
            IUserTokenService userTokenService,
            IUserPermissionService userPermissionService,
            IUserPermissionDao userPermissionDao,
            IUserDao userDao)
        {
            _auditLogService = auditLogService;
            _mailService = mailService;
            _userTokenService = userTokenService;
            _userPermissionService = userPermissionService;
            _userPermissionDao = userPermissionDao;
            _userDao = userDao;
        }

        [HttpGet]
        [Route("")]
        public IActionResult Index()
        {
            return View("~/Views/User/User/Index.cshtml");
        }

        [HttpGet]
        [Route("GetList")]
        public async Task<PagingModel<UserModel>> GetList(UserParameterModel model)
        {
            var result = await _userDao.GetPagedListAsync(model.MapNew(UserContext.StoreId)).ConfigureAwait(false);

            return result.MapNew(model.Page, model.Size);
        }

        [HttpGet]
        [Route("GetForAutocompleteByLoginPattern")]
        public Task<Dictionary<string, int>> GetForAutocompleteByLoginPattern(string pattern)
        {
            return _userDao.GetForAutocompleteAsync(pattern.MapNew());
        }

        [HttpPost]
        [Route("Create")]
        [AjaxErrorHandle]
        public async Task Create(UserModel model)
        {
            var isExistByLogin = await _userDao.IsExistByLoginAsync(model.Login).ConfigureAwait(false);
            if (isExistByLogin)
            {
                throw new Exception("Логин уже занят");
            }

            var isExistByEmail = await _userDao.IsExistByEmailAsync(model.Email).ConfigureAwait(false);
            if (isExistByEmail)
            {
                throw new Exception("Email уже занят");
            }

            var isExistByPhone = await _userDao.IsExistByPhoneAsync(model.Phone.ExtractPhoneNumber()).ConfigureAwait(false);
            if (isExistByPhone)
            {
                throw new Exception("Телефон уже занят");
            }

            var user = model.MapNew();

            var password = RandomGenerator.GenerateAlphaNumbericString(8);

            user.PasswordHash = PasswordHelper.Generate(password);
            user.Phone = user.Phone.ExtractPhoneNumber();

            user.Id = await _userDao.CreateAsync(user).ConfigureAwait(false);

            await _userPermissionService.CreateForAddedUserAsync(user.Id, UserContext.UserId).ConfigureAwait(false);

            _auditLogService.LogInserting(UserContext.UserId, UserContext.StoreId, user);

            await EmailConfirmationProcess(password, user.Id).ConfigureAwait(false);
        }

        [HttpPost]
        [Route("SwitchIsDeleted")]
        [AjaxErrorHandle]
        public async Task<bool> SwitchIsDeleted(int id)
        {
            var isExist = await _userPermissionDao.IsExistAsync(id, UserContext.StoreId).ConfigureAwait(false);
            if (!isExist)
            {
                throw new Exception("Вы не можете удалить данного пользователя");
            }

            var user = await _userDao.GetAsync(id).ConfigureAwait(false);

            var oldModelClone = user.Clone();

            user.IsDeleted = !user.IsDeleted;

            await _userDao.UpdateAsync(user).ConfigureAwait(false);

            _auditLogService.LogUpdating(UserContext.UserId, UserContext.StoreId, oldModelClone, user);

            return user.IsDeleted;
        }

        private async Task EmailConfirmationProcess(string password, int userId)
        {
            var user = await _userDao.GetAsync(userId).ConfigureAwait(false);

            var code = await _userTokenService.CreateAsync(userId, TokenType.EmailConfirmation).ConfigureAwait(false);

            var emailConfirmUrl = Url.Action("ConfirmEmail", "Account", new { userId, code }, HttpContext.Request.Scheme);

            await _mailService.SendFromAdminAsync(user.Email, "Подтверждение почты",
                    $"Ваш сгенерированный пароль: {password}. Вы можете сменить его в настройках своего аккаунта. Пожалуйста, подтвердите свою почту, нажав на <a href='{emailConfirmUrl}'>ссылку</a>.")
                .ConfigureAwait(false);
        }
    }
}