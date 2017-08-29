using System;

namespace Aklion.Crm.ApiV1.Models
{
    public class Organization
    {
        public int Id;

        public string Name;

        public bool IsDeleted;

        public DateTime CreateDate;

        public DateTime? ModifyDate;
    }
}