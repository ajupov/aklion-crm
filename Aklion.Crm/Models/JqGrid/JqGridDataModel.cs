using System;
using System.Collections.Generic;
using System.Linq;

namespace Aklion.Crm.Models.JqGrid
{
    public class JqGridDataModel
    {
        public JqGridDataModel(IEnumerable<object> rows, int rowsCount, int page, int size)
        {
            this.rows = rows.ToList();
            records = rowsCount;
            this.page = page;
            total = (int) Math.Ceiling((double) rowsCount / size);
        }
        
        public List<object> rows { get; set; }
        
        public int page { get; set; }

        public int records { get; set; }

        public int total { get; set; }
    }
}