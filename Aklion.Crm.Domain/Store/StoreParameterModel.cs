using System;
using Aklion.Infrastructure.Storage.DataBaseExecutor.Models;

namespace Aklion.Crm.Domain.Store
{
    public class StoreParameterModel : Parameter
    {
        public int? Id { get; set; }

        public int? CreateUserId { get; set; }

        public string CreateUserLogin { get; set; }

        public string Name { get; set; }

        public string ApiSecret { get; set; }

        public bool? IsLocked { get; set; }

        public bool? IsDeleted { get; set; }

        public DateTime? CreateDate { get; set; }

        public DateTime? ModifyDate { get; set; }
    }
}