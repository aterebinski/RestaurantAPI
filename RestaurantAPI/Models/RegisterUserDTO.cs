using RestaurantAPI.Entities;
using System.ComponentModel.DataAnnotations;

namespace RestaurantAPI.Models
{
    public class RegisterUserDTO
    {
        [Required]
        [MaxLength(25)]
        public string Email { get; set; }
        [Required]
        [MaxLength(25)]
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string Nationality { get; set; }
        [Required]
        [MinLength(8)]
        public string Password { get; set; }
        public int RoleId { get; set; } = 1;
    }
}
