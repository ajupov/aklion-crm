using System;
using System.Collections.Generic;
using Aklion.Crm.Models;
using Aklion.Crm.Models.Administration.AuditLog;
using Aklion.Infrastructure.Mapper;
using DomainAuditLogModel = Aklion.Crm.Domain.AuditLog.AuditLogModel;
using DomainAuditLogParameterModel = Aklion.Crm.Domain.AuditLog.AuditLogParameterModel;

namespace Aklion.Crm.Mappers.Administration.AuditLog
{
    public static class AuditLogMapper
    {
        public static PagingModel<AuditLogModel> MapNew(this Tuple<int, List<DomainAuditLogModel>> tuple, int? page, int? size)
        {
            return new PagingModel<AuditLogModel>(tuple.Item2.MapListNew<AuditLogModel>(), tuple.Item1, page, size);
        }

        public static DomainAuditLogParameterModel MapNew(this AuditLogParameterModel model)
        {
            return model.MapNew<DomainAuditLogParameterModel>();
        }
    }
}