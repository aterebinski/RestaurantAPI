using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using RestaurantAPI.Entities;
using System.Security.Claims;

namespace RestaurantAPI.Authorization
{
    public class ResourceOperationRequirementHandler : AuthorizationHandler<ResourceOperationRequirement, Restaurant>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, ResourceOperationRequirement requirement, Restaurant restaurant)
        {
            if(requirement.ResourceOperation == ResourceOperationType.Create || requirement.ResourceOperation == ResourceOperationType.View)
            {
                context.Succeed(requirement);
            }

            var userId = int.Parse(context.User.FindFirst(i => i.Type == ClaimTypes.NameIdentifier).Value);

            if(restaurant.CreatedById == userId)
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}
