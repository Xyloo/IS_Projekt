using IS_Projekt.Dtos;
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

        [HttpPost]
        public async Task<IActionResult> Create(CreateUserDto userDto)
        {
            var user = await _userService.CreateUser(userDto.Username, userDto.Password);

            return Ok();
        }

        //create function for register
        public async Task<IActionResult> Register(CreateUserDto userDto) {
            User user = new User() {
                Username = userDto.Username,
                Password = userDto.Password,
                Role = "user"
            };

            var userCreate = await _userService.CreateUser(userDto.Username, userDto.Password);
            if(userCreate == null) {
                return BadRequest(new { message = "Username already exists" });
            }
            string token = _userService.GenerateToken(user);
            return Ok(new {token});
        }


    }
}
