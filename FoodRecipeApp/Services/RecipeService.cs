using FoodRecipeApp.Models.DTOs;
using FoodRecipeApp.Repositories.Interface;
using Newtonsoft.Json;

namespace FoodRecipeApp.Services
{
    public class RecipeService : IRecipeService
    {
        private readonly HttpClient _httpClient;
        private readonly string _spoonacularApiKey;

        public RecipeService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _spoonacularApiKey = configuration["AppSettings:SpoonacularApiKey"];
        }

        public class RandomRecipesResponse
        {
            public List<RecipeDto> Recipes { get; set; }
        }



        public async Task<IEnumerable<RecipeDto>> GetRandomRecipes(string tags, int number)
        {
            var response = await _httpClient.GetAsync($"https://api.spoonacular.com/recipes/random?number={number}&tags={tags}&apiKey={_spoonacularApiKey}");
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<RandomRecipesResponse>(content);

            // Map to RecipeDto (assuming you have a suitable mapping function or logic)
            return result.Recipes;
        }

        public async Task<IEnumerable<RecipeDto>> FindByIngredientsAsync(string ingredients, int number, bool limitLicense, int ranking, bool ignorePantry)
        {
            var response = await _httpClient.GetAsync($"https://api.spoonacular.com/recipes/findByIngredients?ingredients={ingredients}&number={number}&limitLicense={limitLicense}&ranking={ranking}&ignorePantry={ignorePantry}&apiKey={_spoonacularApiKey}");
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            var recipes = JsonConvert.DeserializeObject<List<RecipeDto>>(content);
            return recipes;
        }


    }
}
