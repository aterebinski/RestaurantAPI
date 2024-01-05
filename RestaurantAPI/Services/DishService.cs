using AutoMapper;
using RestaurantAPI.Entities;
using RestaurantAPI.Exceptions;
using RestaurantAPI.Models;

namespace RestaurantAPI.Services
{
    public interface IDishService
    {
        int Create(int restaurantId, CreateDishDTO dto);
    }

    public class DishService : IDishService
    {
        private readonly RestaurantDbContext _dbContext;
        private readonly IMapper _mapper;

        public DishService(RestaurantDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public int Create(int restaurantId, CreateDishDTO dto)
        {
            var restaurant = _dbContext.Restaurants.FirstOrDefault(i => i.Id == restaurantId);
            if (restaurant is null)
                throw new NotFoundException("RestaurantNotFound");
            var dish = _mapper.Map<Dish>(dto);
            dish.RestaurantId = restaurantId;
            _dbContext.Dishes.Add(dish);
            _dbContext.SaveChanges();
            return dish.Id;
        }
    }
}
