using System;

namespace Aklion.Crm.Domain.Organization
{
    public class OrganizationDomainModel
    {
        public int Id;

        public string Name;

        public bool IsDeleted;

        public DateTime CreateDate;

        public DateTime? ModifyDate;
    }
}