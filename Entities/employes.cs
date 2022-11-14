using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public struct account
    {
        public  string password { get; set; }
        public string rol { get; set; }
        public bool admin { get;set; }
    }


    public class employes
    {
        
        public string _id { get; set; }
        
        public string ci { get; set; }
        public string name { get; set; }

        public string surname { get; set; }

        public account account { get; set; }

    }
}
