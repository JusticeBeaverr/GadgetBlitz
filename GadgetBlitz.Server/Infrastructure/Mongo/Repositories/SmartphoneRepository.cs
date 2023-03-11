using AutoMapper;
using GadgetBlitz.Server.Core.Repositories;
using GadgetBlitz.Server.Infrastructure.Mongo.DatabaseSettings;
using GadgetBlitz.Server.Infrastructure.Mongo.Document;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace GadgetBlitz.Server.Infrastructure.Mongo.Repositories
{
    public class SmartphoneRepository : ISmartphoneRepository
    {
        private readonly IMongoCollection<SmartphoneDocument> _smartphoneCollection;
        private readonly IMapper _mapper;

        public SmartphoneRepository(IOptions<SmartphonesDatabaseSettings> smartphonesDatabaseSettings, IMapper mapper)
        {
            var mongoClient = new MongoClient(smartphonesDatabaseSettings.Value.ConnectionString);
            var mongoDatabase = mongoClient.GetDatabase(smartphonesDatabaseSettings.Value.DatabaseName);
            _smartphoneCollection = mongoDatabase.GetCollection<SmartphoneDocument>(smartphonesDatabaseSettings.Value.SmartphonesCollectionName);
            _mapper = mapper;
        }   
    }
}
