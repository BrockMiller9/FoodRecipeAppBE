using FoodRecipeApp.Models;
using FoodRecipeApp.Models.DTOs;

namespace FoodRecipeApp.Repositories.Interface
{
    public interface IFavoriteRepository
    {
        Task AddToFavorites(Guid userId, int recipeId);
        Task RemoveFromFavorites(Guid userId, int recipeId);
        Task<IEnumerable<Recipe>> GetUserFavorites(Guid userId);
        Task AddRecipeAndFavorite(Guid userId, RecipeDto recipeDto);
        Task<bool> IsFavorite(Guid userId, int recipeId);
    }
}
