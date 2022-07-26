using System.Collections.Generic;
using System.Threading.Tasks;
using BuildingBlocks.MongoDB;
using MongoDB.Driver;

namespace WebApi.Services;
/// <summary>
/// In order to use Azure Cosmos DB API for MongoDB you need to:
/// 1. provide connection information in appsettings.json
/// 2. register IMongoService in DependencyInjection ( services.AddMongoDb(IConfiguration) )
/// </summary>
public class AzureCosmoDbWithMongoApi
{
    private IMongoCollection<TestCollection> Collection { get; }

    public AzureCosmoDbWithMongoApi(IMongoService mongoService)
    {
        Collection = mongoService.GetCollection<TestCollection>();
    }

    public async Task<IEnumerable<TestCollection>> GetCollection()
    {
        var result = await Collection.FindAsync(_ => true);
        return result.ToList();
    }
}

public class TestCollection
{
    
}