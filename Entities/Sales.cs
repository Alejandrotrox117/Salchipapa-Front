﻿using System;
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
        public List<Orders> orders { get; set; }
        public Employe sellerBy { get; set; }
        public Client client { get; set; }
        List<Payments> payments { get; set; }

    }
}