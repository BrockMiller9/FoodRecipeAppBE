namespace FoodRecipeApp.Models.DTOs
{
    public class AnalyzedInstruction
    {
        public string Name { get; set; }
        public List<Steps> Steps { get; set; }   
    }
}