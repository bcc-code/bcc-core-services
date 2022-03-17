namespace BuildingBlocks.Api.Authentication;

public class TestUser : IUser
{
    public int Id { get; } = 1;
    public string Name { get; } = "TestUser";
    public int TeamId { get; } = 10000; 
    public bool IsAuthenticated() => true;

    public string GetClaimsIdentity(string claim) => string.Empty;

    public List<int> FamilyIds { get; } = new List<int>();
    public int? SpouseId { get; }
    public string OrganizationIdsWithAccess { get; }
}