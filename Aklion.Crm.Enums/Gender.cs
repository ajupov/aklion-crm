using System.ComponentModel.DataAnnotations;

namespace Aklion.Crm.Enums
{
    public enum Gender : byte
    {
        [Display(Name = "Не выбрано")]
        None = 0,

        [Display(Name = "Мужской")]
        Male = 1,

        [Display(Name = "Женский")]
        Female = 2
    }
}