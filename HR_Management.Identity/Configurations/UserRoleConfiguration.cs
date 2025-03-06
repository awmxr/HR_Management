using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HR_Management.Identity.Configurations
{
    public class UserRoleConfiguration : IEntityTypeConfiguration<IdentityUserRole<string>>
    {
        public void Configure(EntityTypeBuilder<IdentityUserRole<string>> builder)
        {
            builder.HasData(
                new IdentityUserRole<string>
                {
                    RoleId = "97b9d189-4cf5-4fba-8d78-86a1045eb97c",
                    UserId = "4616f3e3-6197-483e-a4d8-9bd6708725e9"
                },
                new IdentityUserRole<string>
                {
                    UserId = "de95326f-c8ca-4aad-9e3f-44e38754dbc7",
                    RoleId = "74b645d8-eb91-45a2-8e28-b43e2a677173"
                }
                );
        }
    }
}
