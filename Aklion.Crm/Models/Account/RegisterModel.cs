using System.ComponentModel.DataAnnotations;
using Aklion.Crm.Enums;

namespace Aklion.Crm.Models.Account
{
    public class RegisterModel
    {
        [Required(ErrorMessage = "Введите логин")]
        [Display(Name = "Логин")]
        [StringLength(256, ErrorMessage = "Логин не должен превышать 256 символов")]
        public string Login { get; set; }

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

        [Required(ErrorMessage = "Введите фамилию")]
        [DataType(DataType.Text)]
        [Display(Name = "Фамилия")]
        [StringLength(256, ErrorMessage = "Фамилия не должна превышать 256 символов")]
        public string Surname { get; set; }

        [Required(ErrorMessage = "Введите имя")]
        [DataType(DataType.Text)]
        [Display(Name = "Имя")]
        [StringLength(256, ErrorMessage = "Имя не должно превышать 256 символов")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Введите отчество")]
        [DataType(DataType.Text)]
        [Display(Name = "Отчество")]
        [StringLength(256, ErrorMessage = "Отчество не должно превышать 256 символов")]
        public string Patronymic { get; set; }

        [Required(ErrorMessage = "Введите Email")]
        [Display(Name = "Email")]
        [DataType(DataType.EmailAddress)]
        [EmailAddress(ErrorMessage = "Некорректный Email")]
        [StringLength(256, ErrorMessage = "Email не должен превышать 256 символов")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Введите номер телефона")]
        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Телефон")]
        [Phone(ErrorMessage = "Некорректный номер телефона")]
        [StringLength(10, ErrorMessage = "Номер телефона не должен превышать 10 символов")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Пол должен быть указан")]
        [Display(Name = "Пол")]
        public Gender Gender { get; set; }

        [Required(ErrorMessage = "Дата рождения должна быть указана")]
        [Display(Name = "Дата рождения")]
        [DataType(DataType.Text)]
        public string BirthDateString { get; set; }
    }
}