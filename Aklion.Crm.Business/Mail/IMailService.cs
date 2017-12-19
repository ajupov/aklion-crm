using System.Threading.Tasks;

namespace Aklion.Crm.Business.Mail
{
    public interface IMailService
    {
        Task SendAsync(string from, string to, string subject, string message);

        Task SendFromAdminAsync(string to, string subject, string message);
    }
}