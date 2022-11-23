using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Reports
    {
        public string action { get; set; }
        public string module { get; set; }

        public Employe employe { get; set; }
    }
}
