using System.ComponentModel.DataAnnotations;

namespace Infrastructure.Dao.Enums
{
    public enum FilterCombination
    {
        [Display(Name = "И")]
        And = 0,

        [Display(Name = "ИЛИ")]
        Or = 1
    }
}