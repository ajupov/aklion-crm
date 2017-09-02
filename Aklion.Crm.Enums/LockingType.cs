using System.ComponentModel.DataAnnotations;

namespace Aklion.Crm.Enums
{
    public enum LockingType : byte
    {
        [Display(Name = "Не выбрано")]
        None = 0,

        [Display(Name = "Блокированные")]
        Locked = 1,

        [Display(Name = "Не блокированные")]
        NonLocked = 2
    }
}