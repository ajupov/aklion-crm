using System.ComponentModel.DataAnnotations;

namespace Aklion.Crm.Models.Stores
{
    public class AddStore
    {
        [Display(Name = "Название")]
        [DataType(DataType.Text)]
        [Required(ErrorMessage = "Введите название")]
        [StringLength(256, ErrorMessage = "Название не должно превышать 256 символов")]
        public string Name { get; set; }
    }
}