namespace Crm.Business.Mail.Models
{
    public class MailServiceConfiguration
    {
        public string SmtpHost { get; set; }

        public int SmtpPort { get; set; }

        public string AccountName { get; set; }

        public string Password { get; set; }
    }
}