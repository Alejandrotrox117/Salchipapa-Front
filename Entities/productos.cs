using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class productos
    {
        public int id { get; set; }
        public string name { get; set; }

        public double price { get; set; }

        public List<Toppings> toppings { get; set; }
    }
}
