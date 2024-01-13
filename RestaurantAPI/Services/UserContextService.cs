using RestaurantAPI.Entities;
using System.Security.Claims;

namespace RestaurantAPI.Services
{
    public interface IUserContextService
    {
        int? userId { get; }
        ClaimsPrincipal User { get; }
    }

    public class UserContextService : IUserContextService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserContextService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public ClaimsPrincipal User => _httpContextAccessor.HttpContext.User;

        public int? userId =>
            User is null ? null : int.Parse(_httpContextAccessor.HttpContext.User.FindFirst(i => i.Type == ClaimTypes.NameIdentifier).Value);

    }
}
