using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;

namespace RestaurantAPI.Authorization
{
    public class ResourceOperationRequirementHandler : AuthorizationHandler<ResourceOperationRequirement, ResourceOperation>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, ResourceOperationRequirement requirement, ResourceOperation resourceOperation)
        {
            requirement.ResourceOperation.
        }
    }
}
