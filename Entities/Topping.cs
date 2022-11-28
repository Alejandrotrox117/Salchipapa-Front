using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Topping
    {

        public string name { get; set; }

        public double price { get; set; }
        public bool stock { get; set; }
        public string _id { get; set; }
        public DateTime updatedAt { get; set; }
        public string img
        {
            get
            {
                string direction = $"\\\\192.168.137.1\\public\\toppings\\{this.updatedAt.ToString("dd-MM-yyyy-HH-mm")}-{this.name}.jpg";
                return direction;
            }
        }

    }
}
