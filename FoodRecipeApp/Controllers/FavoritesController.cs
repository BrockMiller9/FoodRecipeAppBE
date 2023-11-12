using FoodRecipeApp.Models.DTOs;
using FoodRecipeApp.Repositories.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace FoodRecipeApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FavoritesController : ControllerBase
    {
        private readonly IFavoriteRepository _favoriteRepository;
        private readonly AppDbContext _context;

        public FavoritesController(IFavoriteRepository favoriteRepository, AppDbContext context)
        {
            _favoriteRepository = favoriteRepository;
            _context = context;
        }

        //[HttpPost("add-to-favorites")]
        //public async Task<ActionResult> AddToFavorites([FromBody] RecipeDto recipeDto, [FromQuery] string userId)
        //{
        //    // Convert userId to a Guid and proceed with your logic
        //    Guid userGuid;
        //    if (!Guid.TryParse(userId, out userGuid))
        //    {
        //        return BadRequest("Invalid user ID");
        //    }
        //    // Call your method to add to favorites
        //    await _favoriteRepository.AddRecipeAndFavorite(userGuid, recipeDto);
        //    return Ok();
        //}

        [HttpPost("add-to-favorites")]
        [Authorize] // This will ensure that the endpoint is protected and the user is authenticated
        public async Task<ActionResult> AddToFavorites([FromBody] RecipeDto recipeDto)
        {
            // The user's ID is retrieved from the User claims, which is populated by the authentication middleware
            Guid userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? string.Empty);

            if (userId == Guid.Empty)
            {
                return BadRequest("Invalid user ID");
            }

            // Call your method to add to favorites
            try
            {
                await _favoriteRepository.AddRecipeAndFavorite(userId, recipeDto);
                return Ok();
            }
            catch (Exception ex)
            {
                // Log the exception
                return StatusCode(500, "An error occurred while adding to favorites.");
            }
        }


        // DELETE: api/Favorites/5
        [HttpDelete("{recipeId}")]
        public async Task<IActionResult> RemoveFromFavorites(int recipeId)
        {
            Guid userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? string.Empty);


            try
            {
                await _favoriteRepository.RemoveFromFavorites(userId, recipeId);
                return Ok();
            }
            catch (Exception ex)
            {
                // Log the exception
                // Return an appropriate error response
                return StatusCode(500, "An error occurred while removing from favorites.");
            }
        }

        // GET: api/Favorites
        [HttpGet]
        public async Task<IActionResult> GetUserFavorites()
        {
            Guid userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? string.Empty);

            try
            {
                var favorites = await _favoriteRepository.GetUserFavorites(userId);
                return Ok(favorites);
            }
            catch (Exception ex)
            {
                // Log the exception
                // Return an appropriate error response
                return StatusCode(500, "An error occurred while retrieving favorites.");
            }
        }

        // FavoritesController.cs

        [HttpGet("is-favorite/{recipeId}")]
        //[Authorize]
        public async Task<ActionResult<bool>> IsFavorite(int recipeId)
        {
            //var userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? string.Empty);
            Guid userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? string.Empty);
            Console.WriteLine(userId);
            if (userId == Guid.Empty)
            {
                return BadRequest("Invalid user ID");
            }

            var isFavorite = await _favoriteRepository.IsFavorite(userId, recipeId);
            return Ok(isFavorite);
        }


    }
}
