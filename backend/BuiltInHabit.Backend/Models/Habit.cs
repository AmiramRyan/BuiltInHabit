using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
namespace BuiltInHabit.Backend.Models;
public class Habit
{
    //What dose this object consist of?

    [BsonId] // MongoDB will automatically generate a unique Id for each document.
    public ObjectId Id { get; set; }

    [BsonElement("name")]
    public string Name { get; set; } = string.Empty;

    [BsonElement("description")]
    public string Description { get; set; } = string.Empty;

    [BsonElement("completed")]
    public bool Completed { get; set; }

}
