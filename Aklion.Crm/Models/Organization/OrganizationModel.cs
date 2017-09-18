using System;

namespace Aklion.Crm.Models
{
    public class OrganizationModel
    {
        public int Id;

        public string Name;

        public bool IsDeleted;

        public DateTime CreateDate;

        public DateTime? ModifyDate;
    }
}