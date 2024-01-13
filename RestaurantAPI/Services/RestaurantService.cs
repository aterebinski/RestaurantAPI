using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration.UserSecrets;
using RestaurantAPI.Authorization;
using RestaurantAPI.Entities;
using RestaurantAPI.Exceptions;
using RestaurantAPI.Models;
using System.Security.Claims;

namespace RestaurantAPI.Services
{
    public interface IRestaurantService
    {
        int Create(CreateRestaurantDTO dto, int userId);
        IEnumerable<RestaurantDTO> GetAll();
        RestaurantDTO GetById(int id);
        void Delete(int id, ClaimsPrincipal user);
        public void Update(int id, RestaurantDTO dto, ClaimsPrincipal user);
    }

    public class RestaurantService : IRestaurantService
    {
        private readonly RestaurantDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly ILogger<RestaurantService> _logger;
        private readonly IAuthorizationService _authorizationService;

        public RestaurantService(RestaurantDbContext dbContext, IMapper mapper, 
            ILogger<RestaurantService> logger, IAuthorizationService authorizationService)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _logger = logger;
            _authorizationService = authorizationService;
        }

        public RestaurantDTO GetById(int id)
        {
            var restaurant = _dbContext.Restaurants
                .Include(a => a.Address)
                .Include(a => a.Dishes)
                .FirstOrDefault(x => x.Id == id);
            if (restaurant == null)
                throw new NotFoundException("Restaurant not found");
            var restaurantDto = _mapper.Map<RestaurantDTO>(restaurant);
            return restaurantDto;
        }

        public IEnumerable<RestaurantDTO> GetAll()
        {
            var restaurants = _dbContext
                .Restaurants
                .Include(a => a.Address)
                .Include(a => a.Dishes)
                .ToList();
            var restaurantsDto = _mapper.Map<List<RestaurantDTO>>(restaurants);
            return restaurantsDto;
        }

        public int Create(CreateRestaurantDTO dto, int userId)
        {
            var restaurant = _mapper.Map<Restaurant>(dto);
            restaurant.CreatedById = userId;
            _dbContext.Restaurants.Add(restaurant);
            _dbContext.SaveChanges();
            return restaurant.Id;
        }

        public void Delete(int id, ClaimsPrincipal User)
        {
            _logger.LogWarning($"Restaurant with Id: {id} - DELETE operation executed!");
            var restaurant = _dbContext.Restaurants.FirstOrDefault(i=>i.Id == id);
            if (restaurant is null)
                throw new NotFoundException("Restaurant not found");

            var authorizationResult = _authorizationService.AuthorizeAsync(User, restaurant, new ResourceOperationRequirement(ResourceOperationType.Delete)).Result;

            if (!authorizationResult.Succeeded) 
            {
                throw new ForbidException();
            }
            
                _dbContext.Restaurants.Remove(restaurant);
                _dbContext.SaveChanges();
        }

        public void Update(int  id, RestaurantDTO dto, ClaimsPrincipal user)
        {
            var restaurant = _dbContext.Restaurants.FirstOrDefault(i => i.Id == id);
            if (restaurant is null)
                throw new NotFoundException("Restaurant not found");

            var authorizationResult = _authorizationService.AuthorizeAsync(user, restaurant, new ResourceOperationRequirement(ResourceOperationType.Update)).Result;
            if (!authorizationResult.Succeeded)
            {
                throw new ForbidException();
            }

            restaurant.Name = dto.Name;
            restaurant.Description = dto.Description;
            restaurant.HasDelivery = dto.HasDelivery;
            _dbContext.SaveChanges();
        }
    }
}
