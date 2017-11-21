using System.ComponentModel.DataAnnotations;

namespace Aklion.Crm.Models.Account
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Введите логин")]
        [DataType(DataType.Text)]
        [Display(Name = "Логин")]
        [StringLength(256, ErrorMessage = "Логин не должен превышать 256 символов")]
        public string Login { get; set; }

        [Required(ErrorMessage = "Введите пароль")]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль")]
        [StringLength(256, ErrorMessage = "Пароль не должен превышать 256 символов")]
        public string Password { get; set; }

        [Display(Name = "Запомнить")]
        public bool RememberMe { get; set; }
    }
}