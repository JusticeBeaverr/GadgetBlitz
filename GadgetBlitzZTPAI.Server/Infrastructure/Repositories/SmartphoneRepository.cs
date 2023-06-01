using AutoMapper;
using GadgetBlitzZTPAI.Server.Core.Entities;
using GadgetBlitzZTPAI.Server.Core.Repositories;
using GadgetBlitzZTPAI.Server.Infrastructure.DatabaseSettings;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;

namespace GadgetBlitzZTPAI.Server.Infrastructure.Repositories
{
    public class SmartphoneRepository : ISmartphoneRepository
    {
        private readonly IMongoCollection<Smartphone> _smartphoneCollection;
        private readonly IMapper _mapper;

        public SmartphoneRepository(IOptions<SmartphonesDatabaseSettings> smartphonesDatabaseSettings, IMapper mapper)
        {
            MongoClientSettings settings = MongoClientSettings.FromConnectionString(smartphonesDatabaseSettings.Value.ConnectionString);
            settings.GuidRepresentation = GuidRepresentation.Standard;
            var mongoClient = new MongoClient(settings);
            var mongoDatabase = mongoClient.GetDatabase(smartphonesDatabaseSettings.Value.DatabaseName);
            _smartphoneCollection = mongoDatabase.GetCollection<Smartphone>(smartphonesDatabaseSettings.Value.SmartphonesCollectionName);
            _mapper = mapper;
        }

        

        public async Task AddOrReplaceSmartphoneAsync(Smartphone smartphone)
        {
            await _smartphoneCollection.ReplaceOneAsync(x => x.SmartphoneId == smartphone.SmartphoneId, smartphone, new ReplaceOptions()
            {
                IsUpsert = true
            }); ;
        }

        public async Task DeleteAsync(Guid smartphoneID)
        {
            await _smartphoneCollection.DeleteOneAsync(s => s.SmartphoneId == smartphoneID);
        }

        public async Task<List<Smartphone>> GetAllAsync()
        {
            var smartphone = await _smartphoneCollection.Find(_ => true)

                .ToListAsync();
            return _mapper.Map<List<Smartphone>>(smartphone);

        }

        public async Task<Smartphone> GetByIDAsync(Guid smartphoneID)
        {
            var smartphone = await _smartphoneCollection.Find(s => s.SmartphoneId == smartphoneID).SingleOrDefaultAsync();
            return _mapper.Map<Smartphone>(smartphone);
        }

        public async Task<Smartphone> GetByName(string name)
        {
            return await _smartphoneCollection.Find(s => s.Name == name).SingleOrDefaultAsync();
            
        }
        public async Task AddReviewAsync(Guid smartphoneId, Review review)
        {
            var filter = Builders<Smartphone>.Filter.Eq(s => s.SmartphoneId, smartphoneId);
            var update = Builders<Smartphone>.Update.Push(s => s.Reviews, review);
            await _smartphoneCollection.UpdateOneAsync(filter, update);
        }

        public async Task UpdateSmartphoneAsync(Smartphone smartphone)
        {
            var filter = Builders<Smartphone>.Filter.Eq(s => s.SmartphoneId, smartphone.SmartphoneId);
            await _smartphoneCollection.ReplaceOneAsync(filter, smartphone);
        }
    }
}
