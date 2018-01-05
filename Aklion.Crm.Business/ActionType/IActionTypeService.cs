using System.Collections.Generic;

namespace Aklion.Crm.Business.ActionType
{
    public interface IActionTypeService
    {
        Dictionary<string, Enums.AuditLogActionType> Get();
    }
}