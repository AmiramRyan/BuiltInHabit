using BuiltInHabit.Backend.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace BuiltInHabit.Backend.Services;
public interface IHabitService
{
    Task<List<Habit>> GetHabitsAsync();
    Task<Habit> GetHabitByIdAsync(string id);
    Task<Habit> CreateHabitAsync(Habit habit);
    Task<Habit> UpdateHabitAsync(string id, Habit habit);
    Task<bool> DeleteHabitAsync(string id);
}
