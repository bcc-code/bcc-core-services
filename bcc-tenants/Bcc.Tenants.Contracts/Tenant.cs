using BuildingBlocks.MongoDB;
using MongoDB.Bson.Serialization.Attributes;

namespace Bcc.Tenants.Contracts;

[CollectionName("tenants")]
public class Tenant
{
    public string Name { get; set; } = null!;
    [BsonId]
    public string TenantKey { get; set; } = null!;
    public List<int> Owners { get; set; } = new();
}