using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using PersonalFinanceTrackerAPI.Data;
using PersonalFinanceTrackerAPI.Models;

namespace PersonalFinanceTrackerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : Controller
    {
        private readonly UserRepository _repository;
        public UsersController(UserRepository repository)
        {
            _repository = repository;
        }


        // GET: api/User/5
        [HttpGet("{userid}")]
        public ActionResult<User> Get(int userid)
        {
            var user = _repository.GetUserById(userid);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        // POST: api/User
        [HttpPost]
        public IActionResult Post([FromBody] User user)
        {
            if (user == null)
            {
                return BadRequest();
            }

            _repository.InsertUser(user);
            return CreatedAtAction(nameof(Get), new { userid = user.UserId }, user);

        }

        // PUT: api/User/5
        [HttpPut("{userid}")]
        public IActionResult Put(int userid, [FromBody] User user)
        {
            if (user == null || user.UserId != userid)
            {
                return BadRequest();
            }

            var existingUser = _repository.GetUserById(userid);
            if (existingUser == null)
            {
                return NotFound();
            }

            _repository.UpdateUser(user);
            return NoContent();
        }

        [HttpPost("register")]
        public IActionResult Register([FromBody] User user)
        {
            // No hashing, just use the plain password as is
            _repository.RegisterUser(user);

            return Ok("User registered successfully.");
        }


        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequest loginRequest)
        {
            // No hashing, just pass the password as is
            User user = _repository.LoginUser(loginRequest.UserName, loginRequest.PasswordHash);

            if (user != null)
            {
                return Ok(new { message = "Login successful", user });
            }
            else
            {
                return Unauthorized("Invalid username or password.");
            }
        }




    }
}
