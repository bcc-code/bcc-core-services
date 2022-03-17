namespace BuildingBlocks.Api.Authentication
{
    public interface IUser
    {
        int Id { get; }
        string Name { get; }
        int TeamId { get; }
        bool IsAuthenticated();
        string GetClaimsIdentity(string claim);
        List<int> FamilyIds { get; }
        int? SpouseId { get; }
    }
}