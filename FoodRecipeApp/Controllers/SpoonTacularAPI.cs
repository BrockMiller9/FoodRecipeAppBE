using FoodRecipeApp.Models.DTOs;
using FoodRecipeApp.Repositories.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FoodRecipeApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SpoonTacularAPI : ControllerBase
    {
        private readonly IRecipeService _recipeService;

        public SpoonTacularAPI(IRecipeService recipeService)
        {
            _recipeService = recipeService;
        }

        [HttpGet("random")]
        public async Task<ActionResult> GetRandomRecipes([FromQuery] string tags, [FromQuery] int number = 10)
        {
            try
            {
                var recipes = await _recipeService.GetRandomRecipes(tags, number);
                return Ok(recipes);
            }
            catch (Exception ex)
            {
                // Log the exception
                return StatusCode(500, "An error occurred while fetching random recipes.");
            }
        }

        [HttpGet("findByIngredients")]
        public async Task<ActionResult<IEnumerable<RecipeDto>>> FindByIngredients(string ingredients, int number = 10, bool limitLicense = true, int ranking = 1, bool ignorePantry = true)
        {
            var recipes = await _recipeService.FindByIngredientsAsync(ingredients, number, limitLicense, ranking, ignorePantry);
            return Ok(recipes);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<RecipeIDDTO>> GetRecipeInformation(int id, [FromQuery] bool includeNutrition = false)
        {
            try
            {
                var recipeInformation = await _recipeService.GetRecipeInformation(id, includeNutrition);
                return Ok(recipeInformation);
            }
            catch (HttpRequestException ex)
            {
                // Log the exception message
                return StatusCode(StatusCodes.Status502BadGateway, "An error occurred while communicating with the external recipe service.");
            }
            catch (Exception ex)
            {
                // Log the exception
                return StatusCode(500, "An error occurred while fetching the recipe information.");
            }
        }


    }
}
