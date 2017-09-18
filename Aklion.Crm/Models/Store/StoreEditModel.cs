using System;
using Aklion.Crm.Models.JqGrid;

namespace Aklion.Crm.Models.Store
{
    public class StoreEditModel : JqGridEditModel
    {
        public int Id { get; set; }

        public int CreateUserId { get; set; }

        public string Name { get; set; }

        public string ApiKey { get; set; }

        public string ApiSecret { get; set; }

        public bool IsLocked { get; set; }

        public bool IsDeleted { get; set; }
    }
}