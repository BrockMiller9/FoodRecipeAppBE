using System.ComponentModel.DataAnnotations;

namespace FoodRecipeApp.Models.DTOs
{
    public class UserLoginDTO
    {
            [Required]
            public string Username { get; set; }

            [Required]
            public string Password { get; set; }

    }
}
