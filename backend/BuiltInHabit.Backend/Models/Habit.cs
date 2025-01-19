using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace BuiltInHabit.Backend.Models
{
    public class Habit
    {
        public enum Frequency { Daily, Weekly, Monthly }

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonElement("id")]
        public string Id { get; set; }

        [BsonElement("Name")]
        [BsonRequired]
        public string Name { get; set; }

        [BsonElement("Description")]
        public string Description { get; set; } = string.Empty;

        [BsonElement("Completed")]
        public bool Completed { get; set; } = false;

        [BsonElement("UserId")]
        [BsonRequired]
        [BsonRepresentation(BsonType.ObjectId)]
        public string UserId { get; set; }

        [BsonElement("HabitFrequency")]

        [BsonRepresentation(BsonType.String)]
        public Frequency HabitFrequency { get; set; } = Frequency.Daily;

        
    }
}
