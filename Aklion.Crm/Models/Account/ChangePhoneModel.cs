using System.ComponentModel.DataAnnotations;

namespace Aklion.Crm.Models.Account
{
    public class ChangePhoneModel
    {
        [Required(ErrorMessage = "Введите номер телефона")]
        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Телефон")]
        [Phone(ErrorMessage = "Некорректный номер телефона")]
        [StringLength(10, ErrorMessage = "Номер телефона не должен превышать 10 символов")]
        public string Phone { get; set; }
    }
}