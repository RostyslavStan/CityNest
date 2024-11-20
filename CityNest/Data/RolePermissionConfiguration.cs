using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace CityNest.Data
{
    public class RolePermissionConfiguration : IEntityTypeConfiguration<RolePermissionEntity>
    {
        private readonly AuthorizationOptions authorizationOptions;

        public RolePermissionConfiguration(AuthorizationOptions authorizationOptions)
        {
            this.authorizationOptions = authorizationOptions;
        }

        public void Configure(EntityTypeBuilder<RolePermissionEntity> builder)
        {
            builder.HasKey(r => new { r.RoleId, r.PermissionId });

            builder.HasData(ParseRolePermissions());
        }

        private RolePermissionEntity[] ParseRolePermissions()
        {
            return authorizationOptions.RolePermissions
                .SelectMany(rp => rp.Permissions
                .Select(p => new RolePermissionEntity
                {
                    RoleId = (int)Enum.Parse<Role>(rp.Role),
                    PermissionId = (int)Enum.Parse<Permission>(p)
                }))
                .ToArray();
        }
    }
}
