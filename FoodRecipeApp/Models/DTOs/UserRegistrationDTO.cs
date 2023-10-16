using System.ComponentModel.DataAnnotations;

namespace FoodRecipeApp.Models.DTOs
{
    public class UserRegistrationDTO
    {
        
            [Required]
            public string Username { get; set; }

            [Required]
            [EmailAddress]
            public string Email { get; set; }

            [Required]
            [MinLength(8)]
            public string Password { get; set; }
        
    }
}
