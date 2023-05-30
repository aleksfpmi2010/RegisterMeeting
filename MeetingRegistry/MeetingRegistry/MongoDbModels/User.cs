using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MeetingRegistry.MongoDbModels
{
    public class User
    {
        [BsonElement("_id")]
        public ObjectId Id { get; set; }
        [BsonElement("login")]
        public string Login { get; set; }
        [BsonElement("password")]
        public string Password { get; set; }
        [BsonElement("email")]
        public string Email { get; set; }
    }
}
