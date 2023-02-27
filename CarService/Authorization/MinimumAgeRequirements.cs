using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarService.Authorization
{
    public class MinimumAgeRequirements : IAuthorizationRequirement
    {
        public int MinimumAge { get; }
        public MinimumAgeRequirements(int minimumAge)
        {
            MinimumAge = minimumAge;
        }    
    }
}
