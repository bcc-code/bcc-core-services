using Bcc.Tenants.Contracts;

namespace Bcc.Tenants.Api.Queries;

public interface ITenantsQueries
{
    List<Tenant> GetTenants();
    IList<Tenant> GetTenantsForOrganisation(int orgId);
}

public class TenantsQueries : ITenantsQueries
{
    private readonly TenantsDataService _tenantsDataService;
    
    public TenantsQueries(TenantsDataService tenantsDataService)
    {
        _tenantsDataService = tenantsDataService;
    }

    public List<Tenant> GetTenants()
    {
        var tenants = _tenantsDataService.GetTenants();
        return tenants.ToList();
    }
    public IList<Tenant> GetTenantsForOrganisation(int orgId)
    {
        var tenants = _tenantsDataService.GetTenants().Where(x => x.Owners.Contains(orgId));
        return tenants.ToList();
    }
}