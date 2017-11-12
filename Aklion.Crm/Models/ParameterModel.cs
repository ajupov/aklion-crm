﻿namespace Aklion.Crm.Models
{
    public class ParameterModel
    {
        public bool IsSearch { get; set; }

        public long Timestamp { get; set; }

        public string SortingColumn { get; set; }

        public string SortingOrder { get; set; }

        public int Page { get; set; }

        public int Size { get; set; }
    }
}