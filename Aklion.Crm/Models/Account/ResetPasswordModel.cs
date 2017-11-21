using System.ComponentModel.DataAnnotations;

namespace Aklion.Crm.Models.Account
{
    public class ResetPasswordModel
    {
        [Required(ErrorMessage = "Введите Email")]
        [Display(Name = "Email")]
        [DataType(DataType.EmailAddress)]
        [EmailAddress(ErrorMessage = "Некорректный Email")]
        [StringLength(256, ErrorMessage = "Email не должен превышать 256 символов")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Введите пароль")]
        [Display(Name = "Пароль")]
        [DataType(DataType.Password)]
        [StringLength(256, ErrorMessage = "Пароль не должен превышать 256 символов")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Введите подтверждение пароля")]
        [DataType(DataType.Password)]
        [Display(Name = "Подтвеждение пароля")]
        [Compare("Password", ErrorMessage = "Пароли не совпадают")]
        public string ConfirmPassword { get; set; }

        public string Code { get; set; }
    }
}