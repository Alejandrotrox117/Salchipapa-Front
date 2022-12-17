using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Payments
    {
        public Payment payment { get; set; }
        public float count { get; set; }
    }
    public class Sales
    {
        public string _id { get; set; }
        public float total { get; set; }  
        public int number { get; set; }
        public List<Orders> orders { get; set; }
        public Employe sellerBy { get; set; }
        public DateTime createdAt { get; set; }
        public string CreatedAt { get { return createdAt.ToString("d"); } }

        public Client client { get; set; }
        public List<Payments> payments { get; set; }
    }
}
