using AutoMapper;
using GadgetBlitzZTPAI.Server.Core.Entities;
using GadgetBlitzZTPAI.Server.Core.Repositories;
using GadgetBlitzZTPAI.Server.Infrastructure.DatabaseSettings;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;

namespace GadgetBlitzZTPAI.Server.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IMongoCollection<User> _usersCollection;
        private readonly IMapper _mapper;

        public UserRepository(IOptions<UsersDatabaseSettings> usersDatabaseSettings, IMapper mapper)
        {
            MongoClientSettings settings = MongoClientSettings.FromConnectionString(usersDatabaseSettings.Value.ConnectionString);
            settings.GuidRepresentation = GuidRepresentation.Standard;
            var mongoClient = new MongoClient(settings);
            var mongoDatabase = mongoClient.GetDatabase(usersDatabaseSettings.Value.DatabaseName);
            _usersCollection = mongoDatabase.GetCollection<User>(usersDatabaseSettings.Value.UsersCollectionName);
            _mapper = mapper;
        }
        public async Task AddOrReplaceUserAsync(User user)
        {
            await _usersCollection.ReplaceOneAsync(x => x.UserId == user.UserId, user, new ReplaceOptions()
            {
                IsUpsert = true
            });
        }

        public async Task DeleteAsync(Guid userId)
        {
            await _usersCollection.DeleteOneAsync(s => s.UserId == userId);
        }

        public async Task<User> GetByUsernameOrEmail(string name, string email)
        {
            return await _usersCollection.Find(s => s.Username == name || s.Email == email).SingleOrDefaultAsync();
        }
    }
}
