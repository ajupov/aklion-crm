﻿namespace Crm.Models.User.ClientAttribute
{
    public class ClientAttributeModel
    {
        public int Id { get; set; }

        public string Key { get; set; }

        public string Name { get; set; }

        public bool IsDeleted { get; set; }

        public string CreateDate { get; set; }

        public string ModifyDate { get; set; }
    }
}