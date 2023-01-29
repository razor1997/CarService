using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarService
{
    public class AuthenticationSettings
    {
        public string JwtKey { get; set; }
        public int JwtKeyExpiredDays { get; set; }
        public string JwtIssuer { get; set; }
    }
}
