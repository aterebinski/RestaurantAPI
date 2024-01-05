using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RestaurantAPI.Entities;
using RestaurantAPI.Exceptions;
using RestaurantAPI.Models;

namespace RestaurantAPI.Services
{
    public interface IRestaurantService
    {
        int Create(CreateRestaurantDTO dto);
        IEnumerable<RestaurantDTO> GetAll();
        RestaurantDTO GetById(int id);
        void Delete(int id);
        public void Update(int id, RestaurantDTO dto);
    }

    public class RestaurantService : IRestaurantService
    {
        private readonly RestaurantDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly ILogger<RestaurantService> _logger;

        public RestaurantService(RestaurantDbContext dbContext, IMapper mapper, ILogger<RestaurantService> logger)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _logger = logger;
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

        public int Create(CreateRestaurantDTO dto)
        {
            var restaurant = _mapper.Map<Restaurant>(dto);
            _dbContext.Restaurants.Add(restaurant);
            _dbContext.SaveChanges();
            return restaurant.Id;
        }

        public void Delete(int id)
        {
            _logger.LogWarning($"Restaurant with Id: {id} - DELETE operation executed!");
            var restaurant = _dbContext.Restaurants.FirstOrDefault(i=>i.Id == id);
            if (restaurant is null)
                throw new NotFoundException("Restaurant not found");
            
                _dbContext.Restaurants.Remove(restaurant);
                _dbContext.SaveChanges();
        }

        public void Update(int  id, RestaurantDTO dto)
        {
            var restaurant = _dbContext.Restaurants.FirstOrDefault(i => i.Id == id);
            if (restaurant is null)
                throw new NotFoundException("Restaurant not found");
            restaurant.Name = dto.Name;
            restaurant.Description = dto.Description;
            restaurant.HasDelivery = dto.HasDelivery;
            _dbContext.SaveChanges();
        }
    }
}
