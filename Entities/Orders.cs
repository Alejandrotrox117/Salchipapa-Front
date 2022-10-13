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
        public int cant { get; set; }
        public List<productos> products { get; set; }


    }
}
