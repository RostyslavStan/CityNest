using Microsoft.AspNetCore.Authorization;

namespace CityNest
{
    public class PermissionAuthorizationHandler
        : AuthorizationHandler<PermissionReqiarement>
    {
        private readonly IServiceScopeFactory serviceScopeFactory;
        public PermissionAuthorizationHandler(IServiceScopeFactory serviceScopeFactory)
        {
            this.serviceScopeFactory = serviceScopeFactory;
        }
        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissionReqiarement requirement)
        {

            var userId = context.User.Claims.FirstOrDefault(c => c.Type == CustomClaims.UserId);

            if (userId == null || !Guid.TryParse(userId.Value, out var id))
            {
                return;
            }

            using var scope = serviceScopeFactory.CreateScope();
            var permissionService = scope.ServiceProvider.GetRequiredService<IPermissionService>();

            var permissions = await permissionService.GetPermissionAsync(id);
            if (permissions.Intersect(requirement.Permissions).Any())
            {
                context.Succeed(requirement);
            }
        }
    }
}

