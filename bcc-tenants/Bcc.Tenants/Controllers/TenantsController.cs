using Bcc.Tenants.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace Bcc.Tenants.Controllers;

[ApiController]
[Route("[controller]")]
public class TenantsController : ControllerBase
{
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    private readonly ILogger<TenantsController> _logger;

    public TenantsController(ILogger<TenantsController> logger)
    {
        _logger = logger;
    }

    [HttpGet]
    public IEnumerable<Tenant> Get()
    {
        return new List<Tenant>();
    }
}