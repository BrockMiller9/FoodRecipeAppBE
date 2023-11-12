namespace FoodRecipeApp.Models
{
    public class UserFavoriteRecipe
    {
        public Guid UserId { get; set; }
        public int RecipeId { get; set; }

        // Navigation properties
        public virtual Users User { get; set; }
        public virtual Recipe Recipe { get; set; }
    }
}