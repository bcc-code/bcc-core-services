using BuildingBlocks.Api.Authentication;

namespace Bcc.Activities.Api.Authentication
{
    public class ApplicationUser : IUser
    {
        private readonly IHttpContextAccessor _accessor;

        public ApplicationUser(IHttpContextAccessor accessor)
        {
            _accessor = accessor;
        }

        public int Id
        {
            get
            {
                var userMetaDataClaim =
                    _accessor.HttpContext?.User?.Claims?.FirstOrDefault(c => c.Type == BuildingBlocks.Api.Authentication.Claims.PersonId);
                int.TryParse(userMetaDataClaim?.Value, out var personId);
                return personId;
            }
        }

        public string Name => _accessor.HttpContext?.User.Identity?.Name ?? string.Empty;
        public int OrganizationId
        {
            get
            {
                var orgId = _accessor.HttpContext?.User?.Claims?.FirstOrDefault(c => c.Type == BuildingBlocks.Api.Authentication.Claims.OrganizationId)?.Value;
                return string.IsNullOrEmpty(orgId) == false ? int.Parse(orgId) : 0;
            }
        }

        public int? SpouseId
        {
            get
            {
                var spouseId = _accessor.HttpContext?.User?.Claims?.FirstOrDefault(c => c.Type == BuildingBlocks.Api.Authentication.Claims.SpouseId)?.Value;
                return string.IsNullOrEmpty(spouseId) == false ? int.Parse(spouseId) : null;
            }
        }

        public bool IsAuthenticated()
        {
            return _accessor.HttpContext?.User.Identity?.IsAuthenticated ?? false;
        }

        public string GetClaimsIdentity(string claim)
        {
            return _accessor.HttpContext?.User?.Claims?.FirstOrDefault(c => c.Type == claim)?.Value ?? string.Empty;
        }
    }
}