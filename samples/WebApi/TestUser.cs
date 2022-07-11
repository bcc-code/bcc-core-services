using BuildingBlocks.Api.Authentication;

namespace WebApi;

public class TestUser : IUser
{
    public int Id { get; }
    public string Name { get; }
    public int OrganizationId { get; }
    public bool IsAuthenticated()
    {
        throw new System.NotImplementedException();
    }

    public string GetClaimsIdentity(string claim)
    {
        throw new System.NotImplementedException();
    }

    public int? SpouseId { get; }
}