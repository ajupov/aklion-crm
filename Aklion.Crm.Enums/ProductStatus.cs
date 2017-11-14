using System.ComponentModel.DataAnnotations;

namespace Aklion.Crm.Enums
{
    public enum ProductStatus : byte
    {
        [Display(Name = "Не выбрано")]
        None = 0,

        [Display(Name = "В наличии")]
        Available = 1,

        [Display(Name = "На складе")]
        InStock = 2,

        [Display(Name = "На заказ")]
        ToOrder = 3,

        [Display(Name = "Отсутствует")]
        Missing = 4
    }
}