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

    public async Task<Habit> GetHabitByIdAsync(ObjectId habitId)
    {
        return await _habits.Find(h => h.Id == habitId).FirstOrDefaultAsync();
    }

    public async Task<bool> UpdateHabitAsync(string habitId, string fieldName, object newValue)
    {
        if(!ObjectId.TryParse(habitId, out ObjectId objectId)){ throw new ArgumentException("Invalid Habit ID format"); }
        var filter = Builders<Habit>.Filter.Eq("_id", objectId);
        var update = Builders<Habit>.Update.Set(fieldName, newValue);
        var result = await _habits.UpdateOneAsync(filter, update);

        return result.ModifiedCount > 0;
    }

    public async Task<bool> DeleteHabitAsync(ObjectId habitId)
    {
        var result = await _habits.DeleteOneAsync(h => h.Id == habitId);
        return result.IsAcknowledged && result.DeletedCount > 0;
    }


}
