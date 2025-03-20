using FitnessApi.DBModel;
using FitnessApi.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace FitnessApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]  
    public class CalorieIntakeController : ControllerBase
    {
        private readonly FitnessDbContext _context;

        public CalorieIntakeController(FitnessDbContext context)
        {
            _context = context;
        }

        
        [HttpPost]
        public async Task<IActionResult> AddCalorie([FromBody] Calory calorie)
        {
            if (calorie == null)
            {
                return BadRequest("Invalid data.");
            }

            
            var usernameClaim = User.FindFirst("UserName")?.Value; 

            if (string.IsNullOrEmpty(usernameClaim))
            {
                return Unauthorized("Username not found in token.");
            }

            var user = _context.Users.FirstOrDefault(u => u.Username == usernameClaim);
            if (user == null)
            {
                return Unauthorized("User not found in the system.");
            }

            calorie.Userid = user.Id;
            calorie.Indate = DateTime.UtcNow; 

            _context.Calories.Add(calorie);
            await _context.SaveChangesAsync();

            return Ok("Calorie entry added successfully.");
        }
        [HttpGet("CalorieTrends")]
        public IActionResult GetCalorieTrends()
        {

            var usernameClaim = User.FindFirst("UserName")?.Value;
            if (string.IsNullOrEmpty(usernameClaim))
                return Unauthorized("Username not found in token.");

            var user = _context.Users.FirstOrDefault(u => u.Username == usernameClaim);
            if (user == null)
                return Unauthorized("User not found.");

            var calorieTrends = _context.Calories
                .Where(c => c.Userid == user.Id)
                .Select(c => new
                {
                    Date = c.Indate,
                    Meal = c.Mealtype,
                    Calories = c.Calories
                })
                .ToList();

            return Ok(calorieTrends);
        }
    }
}
