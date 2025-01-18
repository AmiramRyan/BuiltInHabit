using BuiltInHabit.Backend.DTOs;
using BuiltInHabit.Backend.Models;
using BuiltInHabit.Backend.Services;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using System.Security.Claims;
using static BuiltInHabit.Backend.Models.Habit;

namespace BuiltInHabit.Backend.Controllers
{
    [Route("api/habits")]
    [ApiController]
    public class HabitController : ControllerBase
    {
        private readonly IHabitService _habitService;

        public HabitController(IHabitService habitService)
        {
            _habitService = habitService;
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateHabit([FromBody] Habit habit)
        {
            if (string.IsNullOrWhiteSpace(habit.Name) || string.IsNullOrWhiteSpace(habit.UserId)) 
            {
                return BadRequest("Habit name and UserId is required.");
            }
            habit.Completed = false;
            habit.HabitFrequency = 0;
            var createdHabit = await _habitService.CreateHabitAsync(habit);
            return Ok(new { message = "Habit created successfully", habit = createdHabit });
        }

        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetHabitsByUserId(string userId)
        {
            if (!ObjectId.TryParse(userId, out _)) { return BadRequest("Invalid UserId format"); }
            var habits = await _habitService.GetHabitsByUserIdAsync(userId);
            return Ok(habits);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetHabitById(ObjectId id)
        {
            var habit = await _habitService.GetHabitByIdAsync(id);
            if (habit == null) return NotFound();
            return Ok(habit);
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> UpdateHabit(string id, [FromBody] UpdateHabitRequest request)
        {
            try
            {
                var succsess = await _habitService.UpdateHabitAsync(id, request.FieldName, request.NewValue);
                if (succsess) { return Ok("Habit has been updated"); }
                return NotFound("Habit not found");
            }

            catch (ArgumentException ex) { return BadRequest(ex.Message); }
            catch (Exception ex) { return StatusCode(500, "An error occurred while updating the habit"); }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHabit(ObjectId id)
        {
            var success = await _habitService.DeleteHabitAsync(id);
            if (success) return NoContent();
            return NotFound();
        }
    }
}
