using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class categories
    {

       
        public string name { get; set; }

        
        public string _id { get; set; }

        public string ToString()
        {
            return this.name;
        }

        

    }
}
