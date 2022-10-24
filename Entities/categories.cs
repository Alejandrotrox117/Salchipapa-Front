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
        public List<productos> products { get; set; }  
        public List<Toppings> toppings { get; set; }  
        


    }
}
