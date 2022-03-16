namespace BuildingBlocks.MongoDB
{
    public class MongoDbOptions
    {
        public string ConnectionString { get; set; } = null!;
        public string? ShardKey { get; set; }
        public string DatabaseName { get; set; } = null!;
    }
}