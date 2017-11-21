using System;
using System.Threading.Tasks;
using Aklion.Crm.Dao.UserToken;
using Aklion.Crm.Domain.UserToken;
using Aklion.Crm.Enums;
using Aklion.Infrastructure.Utils.Token;

namespace Aklion.Crm.Business.UserToken
{
    public class UserTokenService : IUserTokenService
    {
        private readonly IUserTokenDao _userTokenDao;

        public UserTokenService(IUserTokenDao userTokenDao)
        {
            _userTokenDao = userTokenDao;
        }

        public async Task<string> Create(int userId, TokenType type)
        {
            var token = new UserTokenModel
            {
                UserId = userId,
                TokenType = TokenType.EmailConfirmation,
                Token = TokenHelper.GenerateToken(type),
                ExpirationDate = TokenHelper.GetExpirationDate(type),
                IsUsed = false
            };

            await _userTokenDao.Create(token).ConfigureAwait(false);

            return token.Token;
        }

        public async Task<bool> Confirm(int userId, TokenType type, string code)
        {
            var identityToken = await _userTokenDao.Get(new UserTokenParameterModel
            {
                UserId = userId,
                TokenType = type,
                Token = code
            }).ConfigureAwait(false);

            if (identityToken == null)
            {
                return false;
            }

            if (identityToken.ExpirationDate < DateTime.Now || identityToken.IsUsed)
            {
                return false;
            }

            await _userTokenDao.SetUsed(identityToken.Id).ConfigureAwait(false);

            return true;
        }
    }
}