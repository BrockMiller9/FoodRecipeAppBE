namespace FoodRecipeApp.Models.DTOs
{
    public class RecipeIDDTO
    {

        public int Id { get; set; }
        public string Title { get; set; }
        public string Image { get; set; }
        public int Servings { get; set; }
        public int ReadyInMinutes { get; set; }
        public string SourceUrl { get; set; }
        public string SpoonacularSourceUrl { get; set; }
        public double HealthScore { get; set; }
        public double PricePerServing { get; set; }
        public bool DairyFree { get; set; }
        public bool GlutenFree { get; set; }
        public bool Ketogenic { get; set; }
        public bool Vegan { get; set; }
        public bool Vegetarian { get; set; }
        public List<ExtendedIngredient> ExtendedIngredients { get; set; }
        public string Summary { get; set; }

        public List<AnalyzedInstruction> AnalyzedInstructions { get; set; }

    }

}

