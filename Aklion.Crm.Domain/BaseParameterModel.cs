using System;

namespace Aklion.Crm.Domain
{
    public class BaseParameterModel
    {
        public int? Id { get; set; }

        public DateTime? CreateDate { get; set; }

        public DateTime? ModifyDate { get; set; }

        public bool IsSearch { get; set; }

        public string SortingColumn { get; set; }

        public string SortingOrder { get; set; }

        public int Page { get; set; }

        public int Size { get; set; }
    }
}