using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PerfumeMarket.Domain.Enum
{
    public enum StatusCode
    {
        UserNotFound = 0,
        UserAlreadyExists = 1,
        PerfumeNotFound = 10,
        OrderNotFound = 20,
        OK = 200,
        InternalServerError = 500
    }
}

