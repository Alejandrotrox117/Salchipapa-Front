using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Orders
    {

        public string origin { get; set; }
        
        public List<products> products { get; set; }


    }
}
