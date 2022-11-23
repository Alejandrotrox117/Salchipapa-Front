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
        public string img
        {
            get {
                string direction = $"\\\\192.168.0.110\\public\\categories\\{this.name}.jpg";
                return direction;
            }
        }
    }
}
