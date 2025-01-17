using BuiltInHabit.Backend.Models;
using BuiltInHabit.Backend.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BuiltInHabit.Backend.Controllers
{
    //Define the swagger CRUD Operations
    [Route("api/[controller]")]
    [ApiController]
    public class HabitController : ControllerBase
    {
        private readonly IHabitService _habitService;

        public HabitController(IHabitService habitService)
        {
            _habitService = habitService;
        }

        // GET: api/Habit
        [HttpGet]
        public async Task<ActionResult<List<Habit>>> GetHabits()
        {
            return await _habitService.GetHabitsAsync();
        }

        // GET: api/Habit/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Habit>> GetHabitById(string id)
        {
            var habit = await _habitService.GetHabitByIdAsync(id);
            if (habit == null)
            {
                return NotFound();
            }
            return habit;
        }

        // POST: api/Habit
        [HttpPost]
        public async Task<ActionResult<Habit>> CreateHabit(Habit habit)
        {
            var createdHabit = await _habitService.CreateHabitAsync(habit);
            return CreatedAtAction(nameof(GetHabitById), new { id = createdHabit.Id }, createdHabit);
        }

        // PUT: api/Habit/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateHabit(string id, Habit habit)
        {
            var updatedHabit = await _habitService.UpdateHabitAsync(id, habit);
            if (updatedHabit == null)
            {
                return NotFound();
            }
            return NoContent();
        }

        // DELETE: api/Habit/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHabit(string id)
        {
            var deleted = await _habitService.DeleteHabitAsync(id);
            if (!deleted)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
