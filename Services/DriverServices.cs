using MongoDB.Driver;
using Driver.Api.Models;
using Microsoft.Extensions.Options;
using Drivers.Api.Configurations;
using System.Reflection.Metadata.Ecma335;


namespace Drivers.Api.Services;

public class DriverServices
{
    private readonly IMongoCollection<Driver.Api.Models.Driver> _driversCollection;

    public DriverServices(
        IOptions<DatabaseSettings> databaseSetting){

            //Inicializar mi cliente de MongoDB
            var mongoClient = new MongoClient(databaseSetting.Value.ConnectionString);

            //Conectar a la base de dato de MongoDB
            var mongoDB =
            mongoClient.GetDatabase(databaseSetting.Value.DatabaseName);
            _driversCollection =
            mongoDB.GetCollection<Driver.Api.Models.Driver>
                (databaseSetting.Value.CollectionName);
            
        }
        public async Task<List<Driver.Api.Models.Driver>> GetAsync()=>
            await _driversCollection.Find(_ => true).ToListAsync();
    

}