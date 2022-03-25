namespace Bcc.Tenants.Contracts;

public class ApplicationTenant
{
    public Guid ApplicationId { get; set; }
    public Guid TenantId { get; set; }
}