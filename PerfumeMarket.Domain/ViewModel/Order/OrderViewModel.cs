using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PerfumeMarket.Domain.ViewModel.Order
{
    public class OrderViewModel
    {
        public long Id { get; set; }

        public string PerfumeName { get; set; }

        public string Brand { get; set; }

        public string GroupOfFragrance { get; set; }

        public string TypePerfume { get; set; }

        public byte[]? Image { get; set; }

        public string Address { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string MiddleName { get; set; }

        public string DateCreate { get; set; }
    }
}
