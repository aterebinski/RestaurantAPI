using System.ComponentModel.DataAnnotations;

namespace RestaurantAPI.Models
{
    public class UpdateRestaurantDTO
    {
        [Required]
        [MaxLength(25)]
        public string Name;
        [Required]
        [MaxLength(50)]
        public string Description;
        public bool HasDelivery;

    }
}
