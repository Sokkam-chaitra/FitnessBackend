using FitnessApi.DBModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FitnessApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WorkOutController : ControllerBase
    {
        FitnessDbContext context = new FitnessDbContext();

        // GET: api/WorkOut/GetWorkouts
        [Authorize]
        [HttpGet("GetWorkouts")]
        public async Task<IActionResult> GetWorkouts()
        {
            var workouts = await context.Tblworkouts
                .Select(w => new
                {
                    WorkoutID = w.Id,
                    WorkoutName = w.WorkoutName,
                    Description = w.Description,
                    RecommendedDuration = w.Duration,
                    MaxCaloriesBurn = w.Maxcalories
                })
                .ToListAsync();

            if (workouts == null || !workouts.Any())
            {
                return NotFound(new { message = "No workouts found" });
            }

            return Ok(workouts);
        }
    }
}
