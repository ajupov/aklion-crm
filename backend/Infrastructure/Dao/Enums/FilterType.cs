using System.ComponentModel.DataAnnotations;

namespace Infrastructure.Dao.Enums
{
    public enum FilterType : byte
    {
        [Display(Name = "")]
        None = 0,

        [Display(Name = "Равно")]
        Equal = 1,

        [Display(Name = "Не равно")]
        NotEqual = 2,

        [Display(Name = "Больше")]
        Above = 3,

        [Display(Name = "Больше или равно")]
        AboveOrEqual = 4,

        [Display(Name = "Меньше")]
        Less = 5,

        [Display(Name = "Меньше или равно")]
        LessOrEqual = 6,

        [Display(Name = "Начинается")]
        Begins = 7,

        [Display(Name = "Не начинается")]
        NotBegins = 8,

        [Display(Name = "Заканчивается")]
        Ends = 9,

        [Display(Name = "Не заканчивается")]
        NotEnds = 10,

        [Display(Name = "Содержит")]
        NotContains = 11,

        [Display(Name = "Между")]
        Between = 12,

        [Display(Name = "В списке")]
        Inlist = 13
    }
}