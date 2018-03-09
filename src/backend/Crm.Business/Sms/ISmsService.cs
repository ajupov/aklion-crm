using System.Threading.Tasks;

namespace Crm.Business.Sms
{
    public interface ISmsService
    {
        Task SendAsync(string phoneNumber, string message);
    }
}