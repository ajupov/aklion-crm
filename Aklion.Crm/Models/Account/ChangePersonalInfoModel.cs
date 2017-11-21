using System.ComponentModel.DataAnnotations;
using Aklion.Crm.Enums;

namespace Aklion.Crm.Models.Account
{
    public class ChangePersonalInfoModel
    {
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

        [Required(ErrorMessage = "Пол должен быть указан")]
        [Display(Name = "Пол")]
        public Gender Gender { get; set; }

        [Required(ErrorMessage = "Дата рождения должна быть указана")]
        [Display(Name = "Дата рождения")]
        [DataType(DataType.Text)]
        public string BirthDateString { get; set; }
    }
}