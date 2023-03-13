using AutoMapper;
using GadgetBlitz.Server.Core.Entities;
using GadgetBlitz.Server.Core.Repositories;
using GadgetBlitz.Server.Infrastructure.Mongo.DatabaseSettings;
using GadgetBlitz.Server.Infrastructure.Mongo.Document;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;

namespace GadgetBlitz.Server.Infrastructure.Mongo.Repositories
{
    public class SmartphoneRepository : ISmartphoneRepository
    {
        private readonly IMongoCollection<SmartphoneDocument> _smartphoneCollection;
        private readonly IMapper _mapper;

        public SmartphoneRepository(IOptions<SmartphonesDatabaseSettings> smartphonesDatabaseSettings, IMapper mapper)
        {
            MongoClientSettings settings = MongoClientSettings.FromConnectionString(smartphonesDatabaseSettings.Value.ConnectionString);
            settings.GuidRepresentation = GuidRepresentation.Standard;
            var mongoClient = new MongoClient(smartphonesDatabaseSettings.Value.ConnectionString);
            var mongoDatabase = mongoClient.GetDatabase(smartphonesDatabaseSettings.Value.DatabaseName);
            _smartphoneCollection = mongoDatabase.GetCollection<SmartphoneDocument>(smartphonesDatabaseSettings.Value.SmartphonesCollectionName);
            _mapper = mapper;
        }

        public async Task AddAsync(Smartphone smartphone)
        {
            await _smartphoneCollection.InsertOneAsync(_mapper.Map<SmartphoneDocument>(smartphone));
        }

        public async Task DeleteAsync(Guid smartphoneID)
        {
            await _smartphoneCollection.DeleteOneAsync(s => s.SmartphoneID == smartphoneID);
        }

        public async Task<IEnumerable<Smartphone>> GetAllAsync()
        {
            var smartphone = await _smartphoneCollection.Find(_ => true)
                .SortBy(s => s.SmartphoneID)
                .ToListAsync();
            return _mapper.Map<IEnumerable<Smartphone>>(smartphone);
                
        }

        public async Task<Smartphone> GetByIDAsync(Guid smartphoneID)
        {
            var smartphone = await _smartphoneCollection.Find(s => s.SmartphoneID == smartphoneID).SingleOrDefaultAsync();
            return _mapper.Map<Smartphone>(smartphone);
        }
    }
}
