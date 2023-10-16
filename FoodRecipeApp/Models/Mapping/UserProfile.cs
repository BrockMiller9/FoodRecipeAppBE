using AutoMapper;

namespace FoodRecipeApp.Models.Mapping
{
    public class UserProfile: Profile
    {
        public UserProfile()
        {
            CreateMap<DTOs.UserRegistrationDTO, Users>();
            CreateMap<DTOs.UserLoginDTO, Users>();
        }
    }
}
