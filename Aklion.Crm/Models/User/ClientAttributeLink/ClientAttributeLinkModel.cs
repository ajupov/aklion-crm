﻿namespace Aklion.Crm.Models.User.ClientAttributeLink
{
    public class ClientAttributeLinkModel
    {
        public int Id { get; set; }

        public int ClientId { get; set; }

        public string ClientName { get; set; }

        public int AttributeId { get; set; }

        public string AttributeName { get; set; }

        public string AttributeDescription { get; set; }

        public string Value { get; set; }

        public string CreateDate { get; set; }
    }
}