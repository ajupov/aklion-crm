using System;
using System.Collections.Generic;

namespace Aklion.Crm.Models
{
    public class BaseGetListResponseModel<TModel>
    {
        public BaseGetListResponseModel(List<TModel> items, int totalCount, int page, int size)
        {
            Items = items;
            TotalCount = totalCount;
            Page = page > 0 ? page : 1;
            Size = size > 0 ? size : 10;
            PageCount = size > 0 ? (int) Math.Ceiling((double) totalCount / size) : 0;
        }

        public List<TModel> Items { get; set; }

        public int Page { get; set; }

        public int Size { get; set; }

        public int PageCount { get; set; }

        public int TotalCount { get; set; }
    }
}