using Microsoft.AspNetCore.Authorization;
using RestaurantAPI.Entities;
using System.Security.Claims;

namespace RestaurantAPI.Authorization
{
    public class CreateMultipleRestaurantsRequirementHandler : AuthorizationHandler<CreateMultipleRestaurantsRequirement>
    {
        private readonly RestaurantDbContext _dbContext;

        public CreateMultipleRestaurantsRequirementHandler(RestaurantDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, CreateMultipleRestaurantsRequirement requirement)
        {
            //todo skończyłem 46 na 3:30
            int userId = int.Parse(context.User.FindFirst(i => i.Type == ClaimTypes.NameIdentifier).Value);

            int howManyRestaurantsCreatedByUser = _dbContext.Restaurants
                //.Where(i => i.CreatedById == userId)
                //.Count();
                .Count(i=>i.CreatedById == userId);

            if(howManyRestaurantsCreatedByUser > requirement.MinimumRestaurantsCreated)
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}
