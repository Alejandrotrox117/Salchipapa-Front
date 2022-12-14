using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Categorie
    { 
        public string name { get; set; }
        public string _id { get; set; }
        public DateTime updatedAt { get; set; }
        public string img
        {
            get {
                string direction = $"\\\\localhost\\public\\categories\\{this.updatedAt.ToString("dd-MM-yyyy-HH-mm")}-{this.name}.jpg";
                return direction;
            }
        }
    }
}
