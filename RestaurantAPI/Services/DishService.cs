using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RestaurantAPI.Entities;
using RestaurantAPI.Exceptions;
using RestaurantAPI.Models;
using System.Collections.Generic;

namespace RestaurantAPI.Services
{
    public interface IDishService
    {
        int Create(int restaurantId, CreateDishDTO dto);
        void Delete(int restaurantId, int dishId);
        bool DeleteAll(int restaurantId);
        List<DishDTO> GetAll(int restaurantId);
        DishDTO getDishById(int restaurantId, int dishId);
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
            var restaurant = getRestaurantById(restaurantId);
            var dish = _mapper.Map<Dish>(dto);
            dish.RestaurantId = restaurantId;
            _dbContext.Dishes.Add(dish);
            _dbContext.SaveChanges();
            return dish.Id;
        }

        public void Delete(int restaurantId, int dishId)
        {
            var restaurant = getRestaurantById(restaurantId);
            var dish = restaurant.Dishes.FirstOrDefault(i=>i.Id == dishId);
            if (dish is null)
                throw new NotFoundException("Dish not found");
            restaurant.Dishes.Remove(dish);
            _dbContext.SaveChanges();
            return;
        }

        public bool DeleteAll(int restaurantId)
        {
            var restaurant = getRestaurantById(restaurantId);
            restaurant.Dishes.Clear();
            _dbContext.SaveChanges();
            return true;
        }

        public List<DishDTO> GetAll(int restaurantId)
        {
            var restaurant = getRestaurantById(restaurantId);
            List<Dish> dishes = restaurant.Dishes.ToList();
            var dishesDTO = _mapper.Map<List<DishDTO>>(dishes);
            return dishesDTO; 
        }

        public DishDTO getDishById(int restaurantId, int dishId)
        {
            var restaurant = getRestaurantById(restaurantId);
            var dish = restaurant.Dishes.FirstOrDefault(i=>i.Id == dishId);
            if (dish is null)
                throw new NotFoundException("Dish not found.");
            var dishDto = _mapper.Map<DishDTO>(dish);
            return dishDto;
        }

        private Restaurant getRestaurantById(int restaurantId)
        {
            var restaurant = _dbContext
                .Restaurants
                .Include(n=>n.Dishes)
                .FirstOrDefault(i => i.Id == restaurantId);
            if (restaurant is null)
                throw new NotFoundException("Restaurant not found.");
            return restaurant;
        }

        
    }
}
