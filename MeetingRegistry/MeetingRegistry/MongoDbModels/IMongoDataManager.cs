using MongoDB.Bson;
using System.Collections.Generic;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using MongoDB.Bson.Serialization;
using System.Text.Json.Nodes;

namespace MeetingRegistry.MongoDbModels
{
    public interface IMongoDataManager
    {
        public IEnumerable<Meeting> GetMeetings();
        public Task<User> GetUserByLoginAndPasswordAsync(string login, string password);
        public Task<User> GetUserByLoginAsync(string login);
        public Task<bool> CreateMeetingAsync(Meeting meeting);
        public Task<bool> Update(int id, string login);
    }
}
