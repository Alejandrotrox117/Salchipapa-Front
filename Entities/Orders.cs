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
        public float total { get; set; }
        public List<Product> products { get; set; }
        public int number { get; set; }
        public string status { get; set; }
        public Employe madeBy { get; set; }
        public Employe attendedBy { get; set; }
        public bool IsProgress
        {
            get { return status == "PROCESO"; }
        }
        public string IsFinish
        {
            get 
            {
                if (status == "LISTO")
                    return "Entregar pedido";
                else
                    return "Aceptar pedido";
            }
        }
    }
}
