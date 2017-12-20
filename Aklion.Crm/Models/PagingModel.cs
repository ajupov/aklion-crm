using System;
using System.Collections.Generic;

namespace Aklion.Crm.Models
{
    public class PagingModel<T>
    {
        public PagingModel(List<T> items, int totalCount, int? page, int? size)
        {
            Items = items;
            TotalCount = totalCount;
            Page = page > 0 ? page.Value : 1;
            Size = size > 0 ? size.Value : 10;
            PageCount = size > 0 ? (int) Math.Ceiling((double) totalCount / (size.Value > 0 ? size.Value : 0)) : 0;
        }

        public List<T> Items { get; set; }

        public int Page { get; set; }

        public int Size { get; set; }

        public int PageCount { get; set; }

        public int TotalCount { get; set; }
    }
}