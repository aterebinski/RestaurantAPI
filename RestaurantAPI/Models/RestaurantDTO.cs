﻿namespace RestaurantAPI.Models
{
    public class RestaurantDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public bool HasDelivery { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string PostalCode { get; set; }

        List<DishDTO> dishes { get; set; }

    }
}
