using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class prueba
    {
        public ObservableCollection<Orders> Fields { get; private set; }
        public prueba()
        {
            Fields = new ObservableCollection<Orders>()
            {
            new Orders()
            };

        }

    }
}
