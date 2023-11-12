namespace FoodRecipeApp.Models
{
    public class Recipe
    {
        public int Id { get; set; }
        public int SpoonacularId { get; set; }
        public string Title { get; set; }
        public string Image { get; set; }
        public int Servings { get; set; }
        public int ReadyInMinutes { get; set; }
        public string SourceUrl { get; set; }
        public string SpoonacularSourceUrl { get; set; }

        // Navigation property for the relationship
        public virtual ICollection<UserFavoriteRecipe> UserFavoriteRecipes { get; set; }
    }
}
