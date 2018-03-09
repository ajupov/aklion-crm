using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Crm.Attributes;
using Crm.Business.Mail;
using Crm.Business.UserPermission;
using Crm.Business.UserToken;
using Crm.Dao.User;
using Crm.Dao.UserPermission;
using Crm.Enums;
using Crm.Mappers.User.User;
using Crm.Models;
using Crm.Models.User.User;
using Infrastructure.Password;
using Infrastructure.PhoneNumber;
using Infrastructure.Random;
using Microsoft.AspNetCore.Mvc;

namespace Crm.Controllers.Users.User
{
    [AjaxErrorHandle]
    [Route("Users")]
    public class UserUserController : BaseController
    {
        private readonly IMailService _mailService;
        private readonly IUserTokenService _userTokenService;
        private readonly IUserPermissionService _userPermissionService;
        private readonly IUserPermissionDao _userPermissionDao;
        private readonly IUserDao _userDao;

        public UserUserController(
            IMailService mailService,
            IUserTokenService userTokenService,
            IUserPermissionService userPermissionService,
            IUserPermissionDao userPermissionDao,
            IUserDao userDao)
        {
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
        public async Task<PagingModel<UserModel>> GetList(UserParameterModel model)
        {
            var result = await _userDao.GetPagedListAsync(model.MapNew(UserContext.StoreId)).ConfigureAwait(false);
            return result.MapNew(model.Page, model.Size);
        }

        [HttpGet]
        public Task<Dictionary<string, int>> GetAutocomplete(string pattern)
        {
            return _userDao.GetAutocompleteAsync(pattern.MapNew());
        }

        [HttpPost]
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

            await EmailConfirmationProcess(password, user.Id).ConfigureAwait(false);
        }

        [HttpPost]
        public async Task Delete(int id)
        {
            var isExist = await _userPermissionDao.IsExistAsync(id, UserContext.StoreId).ConfigureAwait(false);
            if (!isExist)
            {
                throw new Exception("Вы не можете удалить данного пользователя");
            }

            var result = await _userDao.GetAsync(id).ConfigureAwait(false);

            result.IsDeleted = true;
            await _userDao.UpdateAsync(result).ConfigureAwait(false);
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