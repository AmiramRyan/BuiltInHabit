using BuiltInHabit.Backend.Models;
using BuiltInHabit.Backend.Services;
using MongoDB.Bson;
using MongoDB.Driver;

public class HabitService : IHabitService
{
    private readonly IMongoCollection<Habit> _habits;

    public HabitService(IMongoClient mongoClient)
    {
        var database = mongoClient.GetDatabase("HabitTracker"); // Specify the database name
        _habits = database.GetCollection<Habit>("Habits"); // Specify the collection name
    }

    public async Task<List<Habit>> GetHabitsAsync()
    {
        // Retrieve all habits from the collection
        return await _habits.Find(habit => true).ToListAsync();
    }

    public async Task<Habit> GetHabitByIdAsync(string id)
    {
        // Retrieve a single habit by its ID
        var filter = Builders<Habit>.Filter.Eq(h => h.Id, new ObjectId(id));
        return await _habits.Find(filter).FirstOrDefaultAsync();
    }

    public async Task<Habit> CreateHabitAsync(Habit habit)
    {
        // Insert a new habit into the collection
        await _habits.InsertOneAsync(habit);
        return habit;
    }

    public async Task<Habit> UpdateHabitAsync(string id, Habit habit)
    {
        // Update an existing habit by its ID
        var filter = Builders<Habit>.Filter.Eq(h => h.Id, new ObjectId(id));
        var update = Builders<Habit>.Update
            .Set(h => h.Name, habit.Name)
            .Set(h => h.Description, habit.Description)
            .Set(h => h.Completed, habit.Completed);

        var result = await _habits.FindOneAndUpdateAsync(filter, update);
        return result;
    }

    public async Task<bool> DeleteHabitAsync(string id)
    {
        // Delete a habit by its ID
        var filter = Builders<Habit>.Filter.Eq(h => h.Id, new ObjectId(id));
        var result = await _habits.DeleteOneAsync(filter);
        return result.DeletedCount > 0;
    }
}
