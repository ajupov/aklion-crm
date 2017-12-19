using System.Threading.Tasks;
using Aklion.Crm.Enums;

namespace Aklion.Crm.Business.UserToken
{
    public interface IUserTokenService
    {
        Task<string> CreateAsync(int userId, TokenType type);

        Task<bool> ConfirmAsync(int userId, TokenType type, string code);
    }
}