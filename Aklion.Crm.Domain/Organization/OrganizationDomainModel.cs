﻿using System;

namespace Aklion.Crm.Domain.Organization
{
    public class OrganizationDomainModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime? ModifyDate { get; set; }
    }
}