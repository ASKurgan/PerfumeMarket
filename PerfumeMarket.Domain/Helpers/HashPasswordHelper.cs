using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace PerfumeMarket.Domain.Helpers
{
    public static class HashPasswordHelper
    {
        public static string HashPassowrd(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                byte[]? hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                string? hash = BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();

                return hash;
            }
        }
    }
}
