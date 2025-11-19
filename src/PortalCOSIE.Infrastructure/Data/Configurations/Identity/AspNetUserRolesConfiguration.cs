using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PortalCOSIE.Infrastructure.Data.Configurations
{
    public class AspNetUserRolesConfiguration : IEntityTypeConfiguration<IdentityUserRole<string>>
    {
        public void Configure(EntityTypeBuilder<IdentityUserRole<string>> builder)
        {
            builder.HasData(
                // Admin user tiene rol Administrador
                new IdentityUserRole<string>
                {
                    UserId = "00000000-0000-0000-0000-000000000000", 
                    RoleId = "11111111-1111-1111-1111-111111111111"
                }
            );
        }
    }
}