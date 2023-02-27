using CarService.Entities;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace CarService.Authorization
{
    public class MinimumCarMarketsCreatedRequirementsHandler : AuthorizationHandler<MinimumCarMarketsCreatedRequirements>
    {
        private readonly CarServiceDbContext _context;

        public MinimumCarMarketsCreatedRequirementsHandler(CarServiceDbContext context)
        {
            _context = context;
        }
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, MinimumCarMarketsCreatedRequirements requirement)
        {
            var userId = int.Parse((context.User.FindFirst(c => c.Type == ClaimTypes.NameIdentifier)).Value);
            var countCreatedCarMarkets = _context.CarMarkets.Count(c => c.OwnerId == userId);

            if(countCreatedCarMarkets >= requirement.MinimumCarMarketsCreated)
            {
                context.Succeed(requirement);
            }
            return Task.CompletedTask;
        }
    }
}
