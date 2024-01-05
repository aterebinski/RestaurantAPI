using AutoMapper;
using RestaurantAPI.Entities;
using RestaurantAPI.Models;

namespace RestaurantAPI
{
    public class RestaurantMappingProfile :Profile
    {
        public RestaurantMappingProfile()
        {
            CreateMap<Restaurant, RestaurantDTO>()
                .ForMember(r => r.City, c => c.MapFrom(z => z.Address.City))
                .ForMember(r => r.PostalCode, c => c.MapFrom(z => z.Address.PostalCode))
                .ForMember(r => r.Street, c => c.MapFrom(z => z.Address.Street));
            CreateMap<Dish, DishDTO>();

            CreateMap<CreateRestaurantDTO, Restaurant>()
                .ForMember(r => r.Address, z => z.MapFrom(c => new Address()
                {
                    City = c.City, PostalCode = c.PostalCode, Street = c.Street
                }));
                //.ForMember(r => r.Address.Street, c => c.MapFrom(z => z.Street))
                //.ForMember(r => r.Address.City, z => z.MapFrom(c => c.City))
                //.ForMember(r => r.Address.PostalCode, z => z.MapFrom(c => c.PostalCode));
        }
    }
}
