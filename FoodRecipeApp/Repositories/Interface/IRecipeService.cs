using FoodRecipeApp.Models.DTOs;

namespace FoodRecipeApp.Repositories.Interface
{
    public interface IRecipeService
    {

        Task<IEnumerable<RecipeDto>> GetRandomRecipes(string tags, int number);
        Task<IEnumerable<RecipeDto>> FindByIngredientsAsync(string ingredients, int number, bool limitLicense, int ranking, bool ignorePantry);
    }
}
