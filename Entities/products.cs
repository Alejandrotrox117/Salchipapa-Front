using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class products
    {

        public string _id { get; set; }
      public string name { get; set; }
        
        public double price { get; set; }

        public categories categorie { get; set; }

        public string description { get; set; }
        public int stock { get; set; }

        public List<toppings> toppings { get; set; }
    }
}
