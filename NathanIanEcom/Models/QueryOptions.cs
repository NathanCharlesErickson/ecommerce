using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NathanIanEcom.Models
{
    public class QueryOptions
    {
        public String? PK { get; set; }
        public String? SK { get; set; }
        public String? ProductID { get; set; }
        public String? Username { get; set; }
        public String? Description { get; set; }
        public String? Category { get; set; }
        public String? Price { get; set; }
        public String? Name { get; set; }
        public String[]? IDs { get; set; }

    }
}
