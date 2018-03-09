using System.ComponentModel.DataAnnotations;

namespace Infrastructure.AuditLogger.Enums
{
    public enum AuditLogActionType : byte
    {
        [Display(Name = "Не выбрано")]
        None = 0,

        [Display(Name = "Чтение")]
        Select = 1,

        [Display(Name = "Вставка")]
        Insert = 2,

        [Display(Name = "Обновление")]
        Update = 3,

        [Display(Name = "Удаление")]
        Delete = 4
    }
}