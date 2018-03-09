using System.ComponentModel.DataAnnotations;

namespace Crm.Models.Account
{
    public class ForgotPasswordModel
    {
        [Required(ErrorMessage = "Введите Email")]
        [Display(Name = "Email")]
        [DataType(DataType.EmailAddress)]
        [EmailAddress(ErrorMessage = "Некорректный Email")]
        [StringLength(256, ErrorMessage = "Пароль не должен превышать 256 символов")]
        public string Email { get; set; }
    }
}