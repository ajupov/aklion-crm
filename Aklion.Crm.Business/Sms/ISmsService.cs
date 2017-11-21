using System.Threading.Tasks;

namespace Aklion.Crm.Business.Sms
{
    public interface ISmsService
    {
        Task Send(string phoneNumber, string message);
    }
}