using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarService.Authorization
{
    public class MinimumCarMarketsCreatedRequirements : IAuthorizationRequirement
    {
        public int MinimumCarMarketsCreated { get; }

        public MinimumCarMarketsCreatedRequirements(int minimumCarMarketsCreated)
        {
            MinimumCarMarketsCreated = minimumCarMarketsCreated;
        }
    }
}
