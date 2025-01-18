using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace BuiltInHabit.Backend.Models
{
    public class User
    {
        [BsonId]
        public ObjectId Id { get; set; }
        [BsonElement("Username")]
        public string UserName { get; set; }
        [BsonElement("Email")]
        public string Email { get; set; }
        [BsonElement("PasswordHash")]
        public string PasswordHash { get; set; }
    }
}
