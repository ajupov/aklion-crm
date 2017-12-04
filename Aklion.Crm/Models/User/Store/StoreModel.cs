using System;

namespace Aklion.Crm.Models.User.Store
{
    public class StoreModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string ApiSecret { get; set; }

        public DateTime CreateDate { get; set; }
    }
}