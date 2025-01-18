using MongoDB.Driver;
using Microsoft.Extensions.Configuration;
using BuiltInHabit.Backend.Models;

namespace BuiltInHabit.Backend.Services
{
    public class MongoDbContext
    {
        private readonly IMongoDatabase _database;

        public MongoDbContext(IConfiguration configuration)
        {
            var connectionString = configuration.GetValue<string>("MongoDb:ConnectionString");
            if (string.IsNullOrEmpty(connectionString))
            {
                throw new InvalidOperationException("MongoDB connection string is not configured properly.");
            }

            var client = new MongoClient(connectionString);
            _database = client.GetDatabase("HabitTracker");
        }

        public IMongoCollection<User> Users => _database.GetCollection<User>("Users");
        public IMongoCollection<Habit> Habits => _database.GetCollection<Habit>("Habits");
    }
}
