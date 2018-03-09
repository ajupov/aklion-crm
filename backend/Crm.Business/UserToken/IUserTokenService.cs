using System.Threading.Tasks;
using Crm.Enums;

namespace Crm.Business.UserToken
{
    public interface IUserTokenService
    {
        Task<string> CreateAsync(int userId, TokenType type);

        Task<bool> ConfirmAsync(int userId, TokenType type, string code);
    }
}