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
        [HttpGet]
        public IActionResult Get()
        {
            var users = _userService.GetUsers();
            var userDtos = _mapper.Map<IEnumerable<UserDto>>(users);

            return Ok(userDtos);
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var user = _userService.GetUser(id);

            if (user == null)
            {
                return NotFound();
            }

            var userDto = _mapper.Map<UserDto>(user);

            return Ok(userDto);
        }

        [HttpPost]
        public IActionResult Post([FromBody] UserDto userDto)
        {
            if (userDto == null)
            {
                return BadRequest();
            }

            var user = _mapper.Map<User>(userDto);

            _userService.CreateUser(user);

            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] UserDto userDto)
        {
            if (userDto == null)
            {
                return BadRequest();
            }

            var user = _userService.GetUser(id);

            if (user == null)
            {
                return NotFound();
            }

            _mapper.Map(user, userDto);

            _userService.UpdateUser(user);

            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var user = _userService.GetUser(id);

            if (user == null)
            {
                return NotFound();
            }

            _userService.DeleteUser(user);

            return Ok();
        }

        [HttpPost("authenticate")]
        public IActionResult Authenticate([FromBody] LoginDto userDto)
        {
            var user = _userService.Authenticate(userDto.Email, userDto.Password);

            if (user == null)
            {
                return BadRequest(new { message = "Email or password is incorrect" });
            }
            // Set session variable to mark user as authenticated
            //HttpContext.Session.SetInt32("UserId", user.Id);

            // Remove the password from the user object to avoid exposing it
            //user.Password = null;
            return Ok(user);

        }

        [HttpPost("register")]
        public IActionResult Register([FromBody] UserDto userDto)
        {
            if (userDto == null)
            {
                return BadRequest();
            }

            var user = _mapper.Map<User>(userDto);

            // You may want to validate the user input before creating a new user

            _userService.CreateUser(user);

            // Remove the password from the user object to avoid exposing it
            user.Password = null;
            return Ok(new { message = "", user = user });



        }

        [HttpGet("{userId}/transactions")]
        public ActionResult<IEnumerable<TransactionDto>> GetUserTransactions(int userId)
        {
            var transactions = _userService.GetUserTransactions(userId);
            var transactionDtos = _mapper.Map<IEnumerable<TransactionDto>>(transactions);
            return Ok(transactionDtos);
        }


    }
}

        //[HttpPost("register")]
        //public IActionResult Register([FromBody] UserDto userDto)
        //{
        //    // Validate user input
        //    if (string.IsNullOrWhiteSpace(userDto.Email) || string.IsNullOrWhiteSpace(userDto.Password))
        //    {
        //        return BadRequest(new { message = "Email and password are required" });
        //    }

        //    // Check if user already exists
        //    if (_userService.GetUserByEmail(userDto.Email) != null)
        //    {
        //        return BadRequest(new { message = "Email is already registered" });
        //    }

        //    // Map the UserDto to a User object
        //    var user = _mapper.Map<User>(userDto);

        //    //// Hash the password before storing it in the database
        //    //user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);

        //    // Add the user to the database
        //    _userService.CreateUser(user);

        //    // Remove the password from the user object to avoid exposing it
        //    user.Password = null;

        //    return Ok(user);
        //}

        //[HttpGet("{userId}/transactions")]
        //public IActionResult GetUserTransactions(int userId)
        //{
        //    var transactions = _userService.GetUserTransactions(userId);

        //    if (transactions == null)
        //    {
        //        return NotFound();
        //    }

        //    return Ok(transactions);
        //}
        