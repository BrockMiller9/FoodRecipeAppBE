using AutoMapper;
using FoodRecipeApp.Models.DTOs;
using FoodRecipeApp.Models.Mapping;
using FoodRecipeApp.Repositories.Interface;
using FoodRecipeApp.Services;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace FoodRecipeApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController: ControllerBase
    {
            // DI for DbContext and Configuration to read JWT settings
            private readonly AppDbContext _context;
            private readonly IUserRepository _userRepository;
            private readonly PasswordService _passwordService;
            private readonly TokenService _tokenService;
            private readonly IMapper _mapper;

        public AuthController(AppDbContext context, IUserRepository userRepository, 
            PasswordService passwordService, 
            TokenService tokenService, IMapper mapper)
            {
                _context = context;
            _userRepository = userRepository;
            _passwordService = passwordService;
            _tokenService = tokenService;
            _mapper = mapper;
        }

        // TODO: Add methods for Register and Login
        [HttpPost("register")]
        public async Task<ActionResult> Register(UserRegistrationDTO userForRegistration)
        {
            if (await _userRepository.UserExists(userForRegistration.Username, userForRegistration.Email))
            {
                return new BadRequestObjectResult("Username or email already exists");
            }

            // Hash the password

            var (passwordHash, passwordSalt) = _passwordService.CreatePasswordHashAndSalt(userForRegistration.Password);


            Users newUser = _mapper.Map<Users>(userForRegistration);
            newUser.PasswordHash = passwordHash;
            newUser.PasswordSalt = passwordSalt;

            bool userAdded = await _userRepository.AddUser(newUser);
            if (!userAdded)
            {
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }

            string token = _tokenService.GenerateToken(newUser.Id, newUser.Username);
            return Ok(new { token = token });
        }


        [HttpPost("login")]
        public async Task<ActionResult> Login(UserLoginDTO userForLogin)
        {
            // Check if the user exists
            var user = await _userRepository.GetUserByUsername(userForLogin.Username);

            if(user == null)
            {
                return new BadRequestObjectResult("User does not exist");
            }

            // Validate the password
            if (!_passwordService.ValidatePassword(userForLogin.Password, user.PasswordSalt, user.PasswordHash))
            {
                return BadRequest("Invalid password");
            }

            string token = _tokenService.GenerateToken(user.Id, user.Username);

            return Ok(new { token = token });


        }

    }
}
