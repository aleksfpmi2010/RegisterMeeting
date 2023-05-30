using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MeetingRegistry.MongoDbModels
{
    public class Meeting
    {
        [BsonElement("_id")]
        public ObjectId Id { get; set; }
        [BsonElement("MeetingId")]
        public int MeetingId { get; set; }
        [BsonElement("Name")]
        public string Name { get; set; }
        [BsonElement("Description")]
        public string Description { get; set; }
        [BsonElement("Users")]
        public string[] Users { get; set; }
    }
}
