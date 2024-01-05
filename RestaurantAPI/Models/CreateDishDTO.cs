using System.ComponentModel.DataAnnotations;

namespace RestaurantAPI.Models
{
    public class CreateDishDTO
    {
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }

        public int RestaurantId { get; set; }
    }
}
