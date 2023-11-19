using PerfumeMarket.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PerfumeMarket.Domain.Entity
{
    public class Perfume
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string Brand { get; set; }

        public string GroupOfFragrances { get; set; }

        public decimal Price { get; set; }

        public DateTime DateCreate { get; set; }

        public TypePerfume TypePerfume { get; set; }

        public byte[]? Avatar { get; set; }
    }
}
