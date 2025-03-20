using FitnessApi.DBModel;
using FitnessApi.Model;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace FitnessApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SignupController : ControllerBase
    {
        FitnessDbContext context = new FitnessDbContext();

        [HttpPost]
        public IActionResult SignUp([FromBody] SignUp login)
        {
            if (context.Users.Any(u => u.Username == login.username))
            {
                return BadRequest("Username already exists.");
            }

            var user = new User
            {
                Username = login.username,
                Password = login.password, 
                
            };

            context.Users.Add(user);
            context.SaveChanges();

            return Ok("User registered successfully!");
        }
    }
}
