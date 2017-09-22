using System;
using System.Collections.Generic;

namespace Aklion.Crm.Models
{
    public class BaseGetListResponseModel<TModel>
    {
        public BaseGetListResponseModel(List<TModel> items, int totalSize, int page, int size)
        {
            Items = items;
            TotalSize = totalSize;
            Page = page;
            Size = (int) Math.Ceiling((double) totalSize / size);
        }

        public List<TModel> Items { get; set; }

        public int Page { get; set; }

        public int Size { get; set; }

        public int TotalSize { get; set; }
    }
}