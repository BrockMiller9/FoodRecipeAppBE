namespace FoodRecipeApp.Models.DTOs
{
    public class RecipeSearchResultsDTO
    {
        public int Offset { get; set; }
        public int Number { get; set; }
        public List<RecipeSearchResultDTO> Results { get; set; }
        public int TotalResults { get; set; }
    }
}
