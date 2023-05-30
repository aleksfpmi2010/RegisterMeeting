using MongoDB.Bson;
using System.Collections.Generic;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using MongoDB.Bson.Serialization;
using System.Text.Json.Nodes;

namespace MeetingRegistry.MongoDbModels
{
    public class MongoDataManager: IMongoDataManager
    {
        MongoClient _client;
        IMongoDatabase _db;

        public MongoDataManager()
        {
            _client = new MongoClient("mongodb://localhost:27017");
            _db = _client.GetDatabase("MyDatabase");
        }

        public IEnumerable<Meeting> GetMeetings()
        {
            return _db.GetCollection<Meeting>("Meetings")
                .AsQueryable<Meeting>()
                .ToList<Meeting>();
        }


        public async Task<User> GetUserByLoginAndPasswordAsync(string login, string password)
        {
            var builder = Builders<BsonDocument>.Filter;
            var filter = builder.Eq("login", login) & builder.Eq("password", password);
            var allUsers = _db.GetCollection<BsonDocument>("Users");

            var doc = (await allUsers.FindAsync(filter)).FirstOrDefault();
            if (doc != null)
                return BsonSerializer.Deserialize<User>(doc);
            return null;
        }

		public async Task<bool> CheckUserIsValid(string login, string password)
		{
			var builder = Builders<BsonDocument>.Filter;
			var filter = builder.Eq("login", login) & builder.Eq("password", password);
			var allUsers = _db.GetCollection<BsonDocument>("Users");

			var doc = (await allUsers.FindAsync(filter)).FirstOrDefault();
			if (doc != null)
				return true;
			return false;
		}

		public async Task<User> GetUserByLoginAsync(string login)
        {
            var builder = Builders<BsonDocument>.Filter;
            var filter = builder.Eq("login", login);
            var allUsers = _db.GetCollection<BsonDocument>("Users");

            var doc = (await allUsers.FindAsync(filter)).FirstOrDefault();
            if (doc != null)
                return BsonSerializer.Deserialize<User>(doc);
            return null;
        }

        public async Task<bool> CreateMeetingAsync(Meeting meeting)
        {
            try
            {
                await _db.GetCollection<Meeting>("Meetings").InsertOneAsync(meeting);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> Update(int id, string login)
        {
            try
            {
                var filter = new BsonDocument("MeetingId", id);

                var meetingForUpdate = _db.GetCollection<Meeting>("Meetings")
                    .AsQueryable<Meeting>()
                    .FirstOrDefault(x => x.MeetingId == id);
                var newUsersList = meetingForUpdate.Users.Append(login);
                //var updateSettings = new BsonDocument("$set", new BsonDocument("Users", newUsersList));
                var updateSettings = Builders<Meeting>.Update.Set(x => x.Users, newUsersList);

                await _db.GetCollection<Meeting>("Meetings").UpdateOneAsync(filter, updateSettings);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
