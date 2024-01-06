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

            if (!_dbContext.Roles.Any())
            {
                var roles = GetRoles();
                _dbContext.Roles.AddRange(roles);
                _dbContext.SaveChanges();
            }
            /*
            if(!_dbContext.Users.Any())
            {
                var users = GetUsers();
                _dbContext.Users.AddRange(users);
                _dbContext.SaveChanges();
            }
            */
        }

        private IEnumerable<Role> GetRoles()
        {
            var roles = new List<Role>()
            {
                new Role()
                {
                    Name = "User"
                },
                new Role()
                {
                    Name = "Manager"
                },
                new Role()
                {
                    Name = "Admin"
                }
            };
            return roles;
        }
        /*
        private object GetUsers()
        {
            var users = new List<User>()
            {
                new User() {
                    FirstName = "Adam",
                    LastName = "Kowalski",
                    Email = "akowalski@wp.pl",
                    Nationality = "Polish",
                    DateOfBirth = new DateTime(1991,12,11),
                    Role = _dbContext.Roles.FirstOrDefault(i=>i.Name=="Admin")
                },
                new User() {
                    FirstName = "Adam",
                    LastName = "Kowalski",
                    Email = "akowalski@wp.pl",
                    Nationality = "Polish",
                    DateOfBirth = new DateTime(1991,12,11),
                    Role = _dbContext.Roles.FirstOrDefault(i=>i.Name=="Manager")
                },
                new User() {
                    FirstName = "Adam",
                    LastName = "Kowalski",
                    Email = "akowalski@wp.pl",
                    Nationality = "Polish",
                    DateOfBirth = new DateTime(1991,12,11),
                    Role = _dbContext.Roles.FirstOrDefault(i=>i.Name=="Admin")
                }
            };
        }
        */

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
