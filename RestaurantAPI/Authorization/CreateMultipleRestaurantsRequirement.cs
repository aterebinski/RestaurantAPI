using Microsoft.AspNetCore.Authorization;

namespace RestaurantAPI.Authorization
{
    public class CreateMultipleRestaurantsRequirement : IAuthorizationRequirement
    {
        public CreateMultipleRestaurantsRequirement(int minimumRestaurantsCreated)
        {
            MinimumRestaurantsCreated = minimumRestaurantsCreated;
        }

        public int MinimumRestaurantsCreated { get; }
    }
}
