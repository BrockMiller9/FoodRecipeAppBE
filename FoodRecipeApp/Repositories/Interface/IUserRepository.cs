using FoodRecipeApp.Models.DTOs;

namespace FoodRecipeApp.Repositories.Interface
{
    public interface IUserRepository
    {
        Task<Users> GetUserByUsername(string username);
        Task<bool> AddUser(Users user);
        Task<bool> UserExists(string username, string email);
    }
}
