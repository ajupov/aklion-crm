using System.ComponentModel.DataAnnotations;

namespace Aklion.Crm.Enums
{
    public enum DeletingType : byte
    {
        [Display(Name = "Не выбрано")]
        None = 0,

        [Display(Name = "Удаленные")]
        Deleted = 1,

        [Display(Name = "Не удаленные")]
        NonDeleted = 2
    }
}