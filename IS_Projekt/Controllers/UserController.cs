using IS_Projekt.Dtos;
using IS_Projekt.Exceptions;
using IS_Projekt.Models;
using IS_Projekt.Services;
using Microsoft.AspNetCore.Mvc;

namespace IS_Projekt.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var users = await _userService.GetUsers();
            return Ok(users);
        }

        [HttpPost("createuser")]
        public async Task<IActionResult> Create(CreateUserDto userDto)
        {
            User user = new User()
            {
                Username = userDto.Username,
                Password = userDto.Password,
                Email = userDto.Email,
                Role = "user,admin"
            };
            var userCreate = await _userService.CreateUser(user);

            return Ok(userCreate);
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(CreateUserDto userDto)
        {
            try
            {
                var user = new User()
                {
                    Username = userDto.Username,
                    Password = userDto.Password,
                    Email = userDto.Email,
                    Role = "user"
                };
                var userCreate = await _userService.CreateUser(user);
                return Ok();

            }
            catch (Exception ex) when (ex is UsernameExistsException or EmailExistsException)
            {
                return Conflict(ex.Message);

            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error: {ex.Message}");
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginUserDto userDto)
        {
            try
            {
                var user = await _userService.GetUserByUsername(userDto.Username);
                if (user == null)
                    throw new UserNotFoundException();
                if (!_userService.VerifyPassword(user, userDto.Password))
                    throw new InvalidPasswordException();
                string token = _userService.GenerateToken(user);
                return Ok(new { token });
            }
            catch (Exception ex) when (ex is UserNotFoundException or InvalidPasswordException)
            {
                return Unauthorized(ex.Message);
            }
            catch (Exception ex)
            {
                
                return StatusCode(500, $"Error: {ex.Message} {ex.StackTrace}");
            }
        }
    }



}
