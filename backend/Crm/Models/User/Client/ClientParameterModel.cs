﻿using Infrastructure.Dao.Enums;
using Infrastructure.Dao.Models;

namespace Crm.Models.User.Client
{
    public class ClientParameterModel
    {
        public FilterModel Id { get; set; }

        public FilterModel Name { get; set; }

        public FilterModel IsDeleted { get; set; }

        public FilterModel CreateDate { get; set; }

        public FilterCombination FilterCombination { get; set; }

        public string SortingColumn { get; set; }

        public string SortingOrder { get; set; }

        public int? Page { get; set; }

        public int? Size { get; set; }
    }
}