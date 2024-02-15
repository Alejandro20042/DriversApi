using MongoDB.Driver;
using MongoDB.Bson.Serialization;
using Drivers.Api.Models;
using Drivers.Api.Configurations;
using Microsoft.Extensions.Options;
using MongoDB.Bson;

namespace Drivers.Api.Services
{
   public class DriverServices
    {
        private readonly IMongoCollection<Models.Driver> _driversCollection;
        
       public DriverServices(IOptions<DatabaseSettings> databaseSetting)
        {
            var mongoClient = new MongoClient(databaseSetting.Value.ConnectionString);
            var mongoDB = mongoClient.GetDatabase(databaseSetting.Value.DatabaseName);
            _driversCollection = mongoDB.GetCollection<Models.Driver>(databaseSetting.Value.CollectionName);
        }
        public async Task<List<Models.Driver>> GetAsync() =>
            await _driversCollection.Find(_ => true).ToListAsync();
    
        public async Task InsertDriver(Driver driver)
        {
            await _driversCollection.InsertOneAsync(driver);
            // await _driversCollection.DeleteOneAsync(filter, driver);
        }

        public async Task UpdateDriver(string id)
        {
            var filter = Builders<Driver>.Filter.Eq(s => s.Id, id);
            await _driversCollection.DeleteOneAsync(filter);
        }

        public async Task<Driver> GetDriverById(string idTosearch)
        {

            return await _driversCollection.FindAsync(new BsonDocument{{"_id", new ObjectId(idTosearch)}}).Result.FirstAsync();
        }
    }
}
