using System.ComponentModel.DataAnnotations;

namespace Infrastructure.Dao.Enums
{
    public enum SortingType
    {
        [Display(Name = "По возрастанию")]
        Ascending = 0,

        [Display(Name = "По убыванию")]
        Descending = 1
    }
}