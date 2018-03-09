using System.Collections.Generic;
using Crm.Models.Administration.AuditLog;
using Infrastructure.Mapper;
using DomainAuditLogModel = Infrastructure.AuditLogger.Models.AuditLogModel;
using DomainAuditLogParameterModel = Infrastructure.AuditLogger.Models.AuditLogParameterModel;

namespace Crm.Mappers.Administration.AuditLog
{
    public static class AuditLogMapper
    {
        public static List<AuditLogModel> MapNew(this List<DomainAuditLogModel> List)
        {
            return List.MapListNew<AuditLogModel>();
        }

        public static DomainAuditLogParameterModel MapNew(this AuditLogParameterModel model)
        {
            return model.MapParameterNew<DomainAuditLogParameterModel>();
        }
    }
}