using System.ComponentModel.DataAnnotations;

namespace Aklion.Infrastructure.AuditLogger.Enums
{
    public enum AuditLogActionType : byte
    {
        [Display(Name = "Не выбрано")]
        None = 0,

        [Display(Name = "Вставка")]
        Insert = 1,

        [Display(Name = "Обновление")]
        Update = 2,

        [Display(Name = "Удаление")]
        Delete = 3
    }
}