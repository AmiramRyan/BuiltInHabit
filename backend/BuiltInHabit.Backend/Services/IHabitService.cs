using BuiltInHabit.Backend.Models;
using MongoDB.Bson;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace BuiltInHabit.Backend.Services;
public interface IHabitService
{
    Task<Habit> CreateHabitAsync(Habit habit);
    Task<List<Habit>> GetHabitsByUserIdAsync(string userId);
    Task<Habit> GetHabitByIdAsync(ObjectId habitId);
    Task<bool> UpdateHabitAsync(string habitId, string fieldName, object newValue);
    Task<bool> DeleteHabitAsync(ObjectId habitId);
}
