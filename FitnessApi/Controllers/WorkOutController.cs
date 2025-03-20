using FitnessApi.DBModel;
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
    public class WorkOutController : ControllerBase
    {
        private readonly FitnessDbContext _context;

        public WorkOutController(FitnessDbContext context)
        {
            _context = context;
        }


        [HttpPost("LogWorkout")]
        public async Task<IActionResult> LogWorkout([FromBody] Tblworkout workout)
        {
            if (workout == null)
            {
                return BadRequest("Invalid workout data.");
            }

            var usernameClaim = User.FindFirst("UserName")?.Value; 
            if (string.IsNullOrEmpty(usernameClaim))
            {
                return Unauthorized("Username not found in token.");
            }

            
            var user = _context.Users.FirstOrDefault(u => u.Username == usernameClaim);
            if (user == null)
            {
                return Unauthorized("User not found.");
            }

            
            workout.Userid = user.Id;
            workout.Indate = DateTime.UtcNow;


            _context.Tblworkouts.Add(workout);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Workout logged successfully!", workoutId = workout.Id });
        }

        [HttpGet("WorkoutTrends")]
        public IActionResult GetWorkoutTrends()
        {
           
                
                var usernameClaim = User.FindFirst("UserName")?.Value;
                if (string.IsNullOrEmpty(usernameClaim))
                    return Unauthorized("Username not found in token.");

                var user = _context.Users.FirstOrDefault(u => u.Username == usernameClaim);
                if (user == null)
                    return Unauthorized("User not found.");

                
                var workoutTrends = _context.Tblworkouts
                    .Where(w => w.Userid == user.Id)
                    .OrderByDescending(w => w.Id)  
                    .Select(w => new
                    {
                        Date = w.Indate,  
                        WorkoutType = w.WorkoutName,
                        Duration = w.Duration,
                        CaloriesBurned = w.Maxcalories
                    })
                    .ToList();

                if (workoutTrends.Count == 0)
                    return NotFound("No workout trends found.");

                return Ok(workoutTrends);
           
        }

    }
}
