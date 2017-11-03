using System;

namespace Aklion.Crm.Domain.Store
{
    public class StoreModel
    {
        public int Id { get; set; }

        public int CreateUserId { get; set; }

        public string Name { get; set; }

        public string ApiSecret { get; set; }

        public bool IsLocked { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime? ModifyDate { get; set; }
    }
}