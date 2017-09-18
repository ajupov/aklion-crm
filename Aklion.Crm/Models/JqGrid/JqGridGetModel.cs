namespace Aklion.Crm.Models.JqGrid
{
    public class JqGridGetModel
    {
        public bool _search { get; set; }
        
        public int page { get; set; }
        
        public int rows { get; set; }
        
        public string sidx { get; set; }
        
        public string sord { get; set; }
    }
}