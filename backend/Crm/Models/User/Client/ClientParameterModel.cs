﻿using Infrastructure.Dao.Models;

namespace Crm.Models.User.Client
{
    public class ClientParameterModel : BaseParameterModel
    {
        public int? Id { get; set; }

        public string Name { get; set; }

        public bool? IsDeleted { get; set; }

        public string MinCreateDate { get; set; }

        public string MaxCreateDate { get; set; }
    }
}