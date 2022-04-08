using Bcc.Tenants.Contracts;
using BuildingBlocks.MongoDB;
using MongoDB.Driver;
using SharpCompress.Common;

namespace Bcc.Tenants.Api.Queries;

public interface ITenantsQueries
{
    Task<List<Tenant>> GetTenants();
    Task CreateTenant(Tenant tenant);
}

public class TenantsQueries : ITenantsQueries
{
    private IMongoCollection<Tenant> Collection { get; }
    
    public TenantsQueries(IMongoService mongoService)
    {
        Collection = mongoService.GetCollection<Tenant>();
    }

    public async Task<List<Tenant>> GetTenants()
    {
        var tenants = await Collection.FindAsync(tenant => true);
        return tenants.ToList();
    }

    public async Task CreateTenant(Tenant tenant)
    {
        if (string.IsNullOrEmpty(tenant.TenantKey))
        {
            throw new ArgumentNullException(nameof(tenant.TenantKey));
        }
        if (string.IsNullOrEmpty(tenant.Name))
        {
            throw new ArgumentNullException(nameof(tenant.Name));
        }
        if (tenant.Owners.Any() == false)
        {
            throw new ArgumentNullException(nameof(tenant.Owners));
        }
        if (tenant.Owners.Any(x => x == 0))
        {
            throw new ArgumentException("Invalid Owner (OrgId)");
        }
        
        await Collection.InsertOneAsync(tenant);
    }
}