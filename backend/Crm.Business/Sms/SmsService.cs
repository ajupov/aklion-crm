using System;
using System.Threading.Tasks;
using Crm.Business.Sms.Models;
using Infrastructure.PhoneNumber;
using MainSMS;
using Microsoft.Extensions.Options;

namespace Crm.Business.Sms
{
    public class SmsService : ISmsService
    {
        private readonly SmsServiceConfiguration _configuration;

        public SmsService(IOptions<SmsServiceConfiguration> options)
        {
            _configuration = options.Value;
        }

        public async Task SendAsync(string phoneNumber, string message)
        {
            try
            {
                phoneNumber = phoneNumber.ExtractPhoneNumber();
                
                var client = new MainSmsClient(_configuration.ProjectName, _configuration.ApiKey);
                await client.SendAsync(phoneNumber.ToFullPhoneNumber(), message).ConfigureAwait(false);
            }
            catch (Exception)
            {
            }
        }
    }
}