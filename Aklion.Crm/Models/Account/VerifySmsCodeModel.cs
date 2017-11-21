using System.ComponentModel.DataAnnotations;

namespace Aklion.Crm.Models.Account
{
    public class VerifySmsCodeModel
    {
        [Required(ErrorMessage = "Введите код подтверждения из SMS")]
        [DataType(DataType.Text)]
        [Display(Name = "Код подтверждения из SMS")]
        [RegularExpression(@"^[0-9]{4}$", ErrorMessage = "Код подтверждения должен содержать четыре цифры")]
        public string Code { get; set; }
    }
}