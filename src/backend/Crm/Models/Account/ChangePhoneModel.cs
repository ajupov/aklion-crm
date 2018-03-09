using System.ComponentModel.DataAnnotations;

namespace Crm.Models.Account
{
    public class ChangePhoneModel
    {
        [Required(ErrorMessage = "Введите номер телефона")]
        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Телефон")]
        [Phone(ErrorMessage = "Некорректный номер телефона")]
        [StringLength(12, ErrorMessage = "Номер телефона не должен превышать 10 символов")]
        public string Phone { get; set; }
    }
}