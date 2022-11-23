using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public struct Products
    {
        public string categorie { get; set; }
        public List<product> products { get; set; }
    }
    public class product
    {
        public string _id { get; set; }
        public string name { get; set; }
        
        public double price { get; set; }

        public Categorie categorie { get; set; }

        public string description { get; set; }
        public int stock { get; set; }

        public List<Topping> toppings { get; set; }
        public string img
        {
            get
            {
                string direction = $"\\\\192.168.0.110\\public\\products\\{this.name}.jpg";
                return direction;
            }
        }
    }
}
