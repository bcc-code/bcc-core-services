using Bcc.Tenants.Api.Queries;
using Bcc.Tenants.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace Bcc.Tenants.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class TenantsController : ControllerBase
{
    private readonly ILogger<TenantsController> _logger;
    private readonly ITenantsQueries _tenantsQueries;

    public TenantsController(ILogger<TenantsController> logger, ITenantsQueries tenantsQueries)
    {
        _logger = logger;
        _tenantsQueries = tenantsQueries;
    }
    
    [HttpGet]
    public async Task<IList<Tenant>> Get()
    {
        return await _tenantsQueries.GetAllTenants();
    }

    [HttpGet]
    public async Task<IList<ApplicationTenant>> GetTenantsForApplication(Guid applicationId)
    {
        
    }
}