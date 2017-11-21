using System.Threading.Tasks;
using Aklion.Crm.Enums;

namespace Aklion.Crm.Business.UserToken
{
    public interface IUserTokenService
    {
        Task<string> Create(int userId, TokenType type);

        Task<bool> Confirm(int userId, TokenType type, string code);
    }
}