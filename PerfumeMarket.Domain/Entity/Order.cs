﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PerfumeMarket.Domain.Entity
{
    public class Order
    {
        public long Id { get; set; }

        public long? PerfumeId { get; set; }

        public DateTime DateCreated { get; set; }

        public string Address { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string MiddleName { get; set; }

        public long? BasketId { get; set; }

        public virtual Basket Basket { get; set; }
    }
}
