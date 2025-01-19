using BuiltInHabit.Backend.Models;
using BuiltInHabit.Backend.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using MongoDB.Bson;
using MongoDB.Driver;

public class HabitService : IHabitService
{
    private readonly IMongoCollection<Habit> _habits;

    public HabitService(MongoDbContext context)
    {
        _habits = context.Habits;
    }
    //CRUDs
    public async Task<Habit> CreateHabitAsync(Habit habit)
    {
        await _habits.InsertOneAsync(habit);
        return habit;
    }

    public async Task<List<Habit>> GetHabitsByUserIdAsync(string userId)
    {
        var filter = Builders<Habit>.Filter.Eq("UserId", ObjectId.Parse(userId));
        var habits = await _habits.Find(filter).ToListAsync();
        return habits;
    }

    public async Task<Habit> GetHabitByIdAsync(string habitId)
    {
        if (!ObjectId.TryParse(habitId, out ObjectId objectId)) { throw new ArgumentException("Invalid Habit ID format"); }
        var filter = Builders<Habit>.Filter.Eq("_id", objectId);
        var habit = await _habits.Find(filter).FirstOrDefaultAsync();
        return habit;
    }

    public async Task<bool> UpdateHabitAsync(string habitId, string fieldName, string newValue)
    {
        if (!ObjectId.TryParse(habitId, out ObjectId objectId)) { throw new ArgumentException("Invalid Habit ID format");}

        var updatableFields = new[] { "Name", "Description", "Completed", "HabitFrequency" };
        if (!updatableFields.Contains(fieldName)) { throw new ArgumentException($"Field '{fieldName}' is not allowed for updates."); }

        //Check fields are valid
        if (fieldName == "Completed")
        {
            try { bool newCompleted = Convert.ToBoolean(newValue); }
            catch (ArgumentException ex) { throw new ArgumentException("Only values allowed: true | false"); }
        }
        if ((fieldName == "Name" || fieldName == "Description") && string.IsNullOrEmpty(newValue?.ToString())){ throw new ArgumentException($"The value for '{fieldName}' cannot be null or empty."); }
        if (fieldName == "HabitFrequency" && newValue is not string){ throw new ArgumentException("The value for 'HabitFrequency' must be a string."); }

        var filter = Builders<Habit>.Filter.Eq("_id", objectId);
        var update = Builders<Habit>.Update.Set(fieldName, newValue);
        var result = await _habits.UpdateOneAsync(filter, update);
        return result.ModifiedCount > 0;
    }


    public async Task<bool> DeleteHabitAsync(string habitId)
    {
        if (!ObjectId.TryParse(habitId, out ObjectId objectId)) { throw new ArgumentException("Invalid Habit ID format"); }
        var filter = Builders<Habit>.Filter.Eq("_id", objectId);
        var result = await _habits.DeleteOneAsync(filter);

        return result.IsAcknowledged && result.DeletedCount > 0;
    }


}
