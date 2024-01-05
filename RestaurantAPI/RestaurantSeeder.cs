using Microsoft.EntityFrameworkCore;
using RestaurantAPI.Entities;

namespace RestaurantAPI
{
    public class RestaurantSeeder
    {
        private readonly RestaurantDbContext _dbContext;
        public RestaurantSeeder(RestaurantDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Seed()
        {
            if (!_dbContext.Restaurants.Any())
            {
                var restaurants = GetRestaurants();
                _dbContext.Restaurants.AddRange(restaurants);
                _dbContext.SaveChanges();
            }
        }

        private IEnumerable<Restaurant> GetRestaurants()
        {
            List<Restaurant> restaurants = new List<Restaurant>()
            {
                new Restaurant()
                {
                    Name = "McDonalds",
                    Description = "Restauracja McDonalds",
                    HasDelivery = true,
                    Category = "Fast Food",
                    ContactEmail = "contact@mcdonalds.com",
                    ContactNumber = "697452135",
                    Address = new Address()
                    {
                        City = "Nashville",
                        Street = "Elvis Presley Street 1A",
                        PostalCode = "62510"
                    },
                    Dishes = new List<Dish>(){
                        new Dish()
                        {
                            Name = "Frytki",
                            Description = "Frytki na zimno",
                            Price = 6
                        },
                        new Dish()
                        {
                            Name = "Cheesburger",
                            Description = "Burger z serem",
                            Price = 8
                        },
                    }
                },
                new Restaurant()
                {
                    Name = "KFC",
                    Description = "Kentucjy Fried Chicken",
                    HasDelivery = true,
                    Category = "Fast Food",
                    ContactEmail = "contact@kfc.com",
                    ContactNumber = "697333135",
                    Address = new Address()
                    {
                        City = "Dallas",
                        Street = "Jan Stockton Street 2m",
                        PostalCode = "62500"
                    },
                }
            };

            return restaurants;
        }
    }
}
