using Microsoft.AspNetCore.Authorization;
namespace CityNest
{
    public class PermissionReqiarement : IAuthorizationRequirement
    {
        public PermissionReqiarement(Permission[] permissions)
        {
            Permissions = permissions;
        }
        public Permission[] Permissions { get; set; } = [];
    }
}
