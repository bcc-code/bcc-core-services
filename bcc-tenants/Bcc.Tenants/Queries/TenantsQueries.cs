using Bcc.Tenants.Contracts;
using BuildingBlocks.MongoDB;
using MongoDB.Driver;

namespace Bcc.Tenants.Queries;

public interface ITenantsQueries
{
    Task<List<Tenant>> GetAllTenants();
    Task CreateTenant(Tenant tenant);
}

public class TenantsQueries : ITenantsQueries
{
    private IMongoCollection<Tenant> Collection { get; }
    
    public TenantsQueries(IMongoService mongoService)
    {
        Collection = mongoService.GetCollection<Tenant>();
    }

    public async Task<List<Tenant>> GetAllTenants()
    {
        var tenants = await Collection.FindAsync(tenant => true);
        return tenants.ToList();
    }

    public async Task CreateTenant(Tenant tenant)
    {
        await Collection.InsertOneAsync(new Tenant
        {
            Id = Guid.NewGuid(),
            Name = "Testowy tenant",
            ApplicationId = Guid.NewGuid(),
            Culture = "pl-PL",
            Owner = OrganizationId.From(Guid.NewGuid()),
            OrganizationsWithAccess = new List<OrganizationId> {OrganizationId.From(Guid.NewGuid())}
        });
    }
}