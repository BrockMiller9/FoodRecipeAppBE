namespace FoodRecipeApp.Models.DTOs
{
    public class RecipeDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Image { get; set; }
        public int Servings { get; set; }
        public int ReadyInMinutes { get; set; }
        public string SourceUrl { get; set; }
        public string SpoonacularSourceUrl { get; set; }
        // ... any other properties you want to include
    }

}
