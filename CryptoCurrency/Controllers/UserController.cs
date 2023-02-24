using AutoMapper;
using CryptoCurrency.Dto;
using CryptoCurrency.Interfaces;
using CryptoCurrency.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CryptoCurrency.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUser _userService;
        private readonly IMapper _mapper;

        public UserController(IUser userservice, IMapper mapper)
        {

            _userService = userservice;
            _mapper = mapper;
        }

        [HttpPost("authenticate")]
        public IActionResult Authenticate([FromBody] UserDto userobj)
        {
            if (userobj == null)
                return BadRequest();

            // Find the user by email and password
            var user = _userService.Authenticate(userobj.Email, userobj.Password);

            if (user == null)
                return NotFound(new { Message = "User not found" });


            // Return the token to the client
            return Ok(new
            {
                Message = "Login Success",

            });
        }





        [HttpPost("Registration")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateUser([FromBody] UserDto userobj)
        {
            if (userobj == null)
                return BadRequest(ModelState);

            var existingUser = _userService.GetUsers()
                .Where(x => x.Id == userobj.Id).FirstOrDefault();

            if (existingUser != null)
            {
                ModelState.AddModelError("", " user with this first name already exists ");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var UserMap = _mapper.Map<User>(userobj);

            if (!_userService.CreateUser(UserMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully created");
        }
    }
}
