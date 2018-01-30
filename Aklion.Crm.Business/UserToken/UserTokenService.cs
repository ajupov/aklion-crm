using System;
using System.Threading.Tasks;
using Aklion.Crm.Dao.UserToken;
using Aklion.Crm.Domain.UserToken;
using Aklion.Crm.Enums;
using Aklion.Infrastructure.Random;

namespace Aklion.Crm.Business.UserToken
{
    public class UserTokenService : IUserTokenService
    {
        private readonly IUserTokenDao _userTokenDao;

        public UserTokenService(IUserTokenDao userTokenDao)
        {
            _userTokenDao = userTokenDao;
        }

        public async Task<string> CreateAsync(int userId, TokenType type)
        {
            var token = new UserTokenModel
            {
                UserId = userId,
                TokenType = type,
                Token = GenerateToken(type),
                ExpirationDate = GetExpirationDate(type),
                IsUsed = false,
                CreateDate = DateTime.Now
            };

            await _userTokenDao.CreateAsync(token).ConfigureAwait(false);

            return token.Token;
        }

        public async Task<bool> ConfirmAsync(int userId, TokenType type, string code)
        {
            var identityToken = await _userTokenDao.GetAsync(new UserTokenParameterModel
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

            await _userTokenDao.SetUsedAsync(identityToken.Id).ConfigureAwait(false);

            return true;
        }

        private static string GenerateToken(TokenType tokenType)
        {
            switch (tokenType)
            {
                case TokenType.EmailConfirmation:
                    return RandomGenerator.GenerateRandomCharacters(127);
                case TokenType.PhoneConfirmation:
                    return RandomGenerator.GenerateRandomInt(4);
                case TokenType.PasswordReset:
                    return RandomGenerator.GenerateRandomCharacters(127);
                default:
                    return null;
            }
        }

        private static DateTime GetExpirationDate(TokenType tokenType)
        {
            switch (tokenType)
            {
                case TokenType.EmailConfirmation:
                    return DateTime.Now.AddDays(1);
                case TokenType.PhoneConfirmation:
                    return DateTime.Now.AddMinutes(10);
                case TokenType.PasswordReset:
                    return DateTime.Now.AddDays(1);
                default:
                    return DateTime.Now;
            }
        }
    }
}