using System.Collections.Generic;
using Aklion.Crm.Models;
using Aklion.Crm.Models.Administration.AuditLog;
using Aklion.Infrastructure.Mapper;
using DomainAuditLogModel = Aklion.Infrastructure.AuditLogger.Models.AuditLogModel;
using DomainAuditLogParameterModel = Aklion.Infrastructure.AuditLogger.Models.AuditLogParameterModel;

namespace Aklion.Crm.Mappers.Administration.AuditLog
{
    public static class AuditLogMapper
    {
        public static PagingModel<AuditLogModel> MapNew(this (int TotalCount, List<DomainAuditLogModel> List) tuple, int? page, int? size)
        {
            return new PagingModel<AuditLogModel>(tuple.List.MapListNew<AuditLogModel>(), tuple.TotalCount, page, size);
        }

        public static DomainAuditLogParameterModel MapNew(this AuditLogParameterModel model)
        {
            return model.MapParameterNew<DomainAuditLogParameterModel>();
        }
    }
}