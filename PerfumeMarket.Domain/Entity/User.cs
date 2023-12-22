using PerfumeMarket.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace PerfumeMarket.Domain.Entity
{
    public class User
    {
        public long Id { get; set; }

        public string Password { get; set; }
        
        public string Name { get; set; }

        public Role Role { get; set; }

        public Profile Profile { get; set; }

        public Basket Basket { get; set; }
    }
}
