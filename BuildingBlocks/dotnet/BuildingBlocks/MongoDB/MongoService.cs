using System.Security.Authentication;
using MongoDB.Driver;

namespace BuildingBlocks.MongoDB
{
    
    public interface IMongoService
    {
        IMongoCollection<T> GetCollection<T>();
    }

    public class MongoService: IMongoService
    {
        private readonly IMongoDatabase _db;

        public MongoService(MongoDbOptions options)
        {
            if (string.IsNullOrEmpty(options.ConnectionString))
            {
                throw new ArgumentNullException(nameof(options.ConnectionString));
            }
            if (string.IsNullOrEmpty(options.DatabaseName))
            {
                throw new ArgumentNullException(nameof(options.ConnectionString));
            }

            var settings = MongoClientSettings.FromUrl(new MongoUrl(options.ConnectionString));
            settings.SslSettings = new SslSettings() { EnabledSslProtocols = SslProtocols.Tls12 };
        
            var mongoClient = new MongoClient(settings);
            _db = mongoClient.GetDatabase(options.DatabaseName);
        }

        public IMongoCollection<T> GetCollection<T>()
        {
            var collectionName = (typeof(T).GetCustomAttributes(typeof(CollectionNameAttribute), true).FirstOrDefault()
                as CollectionNameAttribute)!.CollectionName;

            return _db.GetCollection<T>(collectionName);
        }
    }
}
