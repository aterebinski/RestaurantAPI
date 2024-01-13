using Microsoft.AspNetCore.Authorization;

namespace RestaurantAPI.Authorization
{
    public enum ResourceOperationType
    {
        Create,
        Delete,
        Update,
        View
    }

    public class ResourceOperationRequirement : IAuthorizationRequirement
    {
        public ResourceOperationType ResourceOperation { get; }

        public ResourceOperationRequirement(ResourceOperationType resourceOperation) 
        { 
            ResourceOperation = resourceOperation;
        }
    }
}
