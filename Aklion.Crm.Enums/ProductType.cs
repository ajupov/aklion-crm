using System.ComponentModel.DataAnnotations;

namespace Aklion.Crm.Enums
{
    public enum ProductType : byte
    {
        [Display(Name = "Не выбрано")]
        None = 0,

        [Display(Name = "Товар")]
        Product = 1,

        [Display(Name = "Услуга")]
        Service = 2
    }
}