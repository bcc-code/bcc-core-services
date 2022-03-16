namespace Bcc.Tenants.Contracts;

public class Tenant
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public Guid ApplicationId { get; set; }
    public string Culture { get; set; } = null!;
    public OrganizationId Owner { get; set; } = null!;
    public List<OrganizationId> OrganizationsWithAccess { get; set; } = null!;
}