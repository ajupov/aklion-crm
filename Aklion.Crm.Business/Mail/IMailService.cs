using System.Threading.Tasks;

namespace Aklion.Crm.Business.Mail
{
    public interface IMailService
    {
        Task Send(string from, string to, string subject, string message);

        Task SendFromAdmin(string to, string subject, string message);
    }
}