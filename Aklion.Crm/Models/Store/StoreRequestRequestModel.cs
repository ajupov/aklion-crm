using System;

namespace Aklion.Crm.Models.Store
{
    public class StoreRequestRequestModel : ParameterModel
    {
        public string Name { get; set; }

        public string ApiKey { get; set; }

        public int? CreateUserId { get; set; }

        public string CreateUserEmail { get; set; }

        public string ApiSecret { get; set; }

        public bool? IsLocked { get; set; }

        public bool? IsDeleted { get; set; }

        public DateTime? CreateDate { get; set; }

        public DateTime? ModifyDate { get; set; }
    }
}