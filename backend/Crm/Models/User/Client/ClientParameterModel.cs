using System.Collections.Generic;
using Infrastructure.Dao.Models;

namespace Crm.Models.User.Client
{
    public class ClientParameterModel : BaseParameterModel
    {
        public string Name { get; set; }

        public bool? IsDeleted { get; set; }

        public string MinCreateDate { get; set; }

        public string MaxCreateDate { get; set; }

        public Dictionary<int, string> Attributes { get; set; }
    }
}