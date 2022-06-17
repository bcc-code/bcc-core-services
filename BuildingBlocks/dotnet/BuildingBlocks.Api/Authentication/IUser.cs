namespace BuildingBlocks.Api.Authentication
{
    public interface IUser
    {
        int Id { get; }
        string Name { get; }
        int OrganizationId { get; }
        bool IsAuthenticated();
        string GetClaimsIdentity(string claim);
        int? SpouseId { get; }
        
    }
}