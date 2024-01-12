using Microsoft.AspNetCore.Authorization;

namespace RestaurantAPI.Authorization
{
    public enum ResourceOperation
    {
        Create,
        Delete,
        Update,
        View
    }

    public class ResourceOperationRequirement : IAuthorizationRequirement
    {
        public ResourceOperation ResourceOperation { get; }

        public ResourceOperationRequirement(ResourceOperation resourceOperation) 
        { 
            ResourceOperation = resourceOperation;
        }
    }
}
