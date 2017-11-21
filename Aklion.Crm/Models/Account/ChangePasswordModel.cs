using System.ComponentModel.DataAnnotations;

namespace Aklion.Crm.Models.Account
{
    public class ChangePasswordModel
    {
        [Required(ErrorMessage = "Введите старый пароль")]
        [DataType(DataType.Password)]
        [Display(Name = "Текущий пароль")]
        [StringLength(256, ErrorMessage = "Пароль не должен превышать 256 символов")]
        public string OldPassword { get; set; }

        [Required(ErrorMessage = "Введите новый пароль")]
        [Display(Name = "Новый пароль")]
        [DataType(DataType.Password)]
        [StringLength(256, ErrorMessage = "Пароль не должен превышать 256 символов")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Подтвеждение пароля")]
        [Compare("Password", ErrorMessage = "Пароли не совпадают")]
        public string ConfirmPassword { get; set; }
    }
}