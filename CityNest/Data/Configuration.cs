using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CityNest
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(u => u.Id);

            builder.HasMany(a => a.Properties)
            .WithOne(u => u.User);

            builder.HasMany(u => u.Roles)
                .WithMany(r => r.Users)
                .UsingEntity<UserRoleEntity>(
                u => u.HasOne<RoleEntity>().WithMany().HasForeignKey(r => r.RoleId),
                r => r.HasOne<User>().WithMany().HasForeignKey(u => u.UserId));
        }
    }
    public class PropertyConfiguration : IEntityTypeConfiguration<Property>
    {
        public void Configure(EntityTypeBuilder<Property> builder)
        {
            builder.HasKey(u => u.Id);

            builder.HasOne(a => a.User)
            .WithMany(u => u.Properties);
        }
    }
}