using System.Threading.Tasks;

namespace Aklion.Crm.Business.Sms
{
    public interface ISmsService
    {
        Task SendAsync(string phoneNumber, string message);
    }
}