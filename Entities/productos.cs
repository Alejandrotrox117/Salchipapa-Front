using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class productos
    {

      public string name { get; set; }
        
        public int price { get; set; }

        public string categorie { get; set; }

        public string description { get; set; }
        public int  stock { get; set; }

        public List<Toppings> toppings { get; set; }
    }
}
