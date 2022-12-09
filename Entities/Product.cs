using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public struct Products
    {
        public Categorie categorie { get; set; }
        public List<Product> products { get; set; }
    }
    public class Product
    {
        public Product product { get; set; }    
        public string _id { get; set; }
        public string name { get; set; }
        
        public double price { get; set; }

        public Categorie categorie { get; set; }

        public string description { get; set; }
        public int stock { get; set; }
        public DateTime updatedAt { get; set; }
        public List<Topping> toppings { get; set; }
        public string img
        {
            get
            {
                string direction = $"\\\\localhost\\public\\products\\{this.updatedAt.ToString("dd-MM-yyyy-HH-mm")}-{this.name}.jpg";
                return direction;
            }
        }
    }
}
