using MongoDB.Driver;
using MongoDB.Bson.Serialization;
using Drivers.Api.Models;
using Drivers.Api.Configurations;
using Microsoft.Extensions.Options;
using MongoDB.Bson;

namespace Drivers.Api.Services
{
    public class CarreraServices
    {
        private readonly IMongoCollection<Drivers.Api.Models.Carrera> _driverCollection;
        
        public CarreraServices(IOptions<DatabaseSettings> databaseSettings)
        {
            var mongoClient = new MongoClient(databaseSettings.Value.ConnectionString);
            
            var mongoDB =
            mongoClient.GetDatabase(databaseSettings.Value.DatabaseName);
                _driverCollection =
                    mongoDB.GetCollection<Carrera>(databaseSettings.Value.Collections["Carreras"]);
        }

        public async Task<List<Drivers.Api.Models.Carrera>> GetAsync() =>
            await _driverCollection.Find(_ => true).ToListAsync();

        public async Task InsertCarrera(Carrera carreraInsert)
        {
            await _driverCollection.InsertOneAsync(carreraInsert);
        }

        public async Task DeleteCarrera(string carreraId)
        {
            var filter = Builders<Carrera>.Filter.Eq(s=>s.Id, carreraId);
            await _driverCollection.DeleteOneAsync(filter);
        }

        public async Task UpdateCarrera(Carrera dataToUpdate)
        {
            var filter = Builders<Carrera>.Filter.Eq(s=>s.Id, dataToUpdate.Id);
            await _driverCollection.ReplaceOneAsync(filter,dataToUpdate);
        }

        public async Task<Carrera> GetCarreraById(string idToSearch)
        {
            return await _driverCollection.FindAsync(new BsonDocument{{"_id", new ObjectId(idToSearch)}}).Result.FirstAsync();    
        }
        
    }
}