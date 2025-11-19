using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PortalCOSIE.Infrastructure.Data.Configurations.Identity
{
    public class AspNetRolesConfiguration : IEntityTypeConfiguration<IdentityRole>
    {
        public void Configure(EntityTypeBuilder<IdentityRole> builder)
        {
            builder.HasData(
                new IdentityRole
                {
                    Id = "11111111-1111-1111-1111-111111111111",
                    Name = "Administrador",
                    NormalizedName = "ADMINISTRADOR",
                    ConcurrencyStamp = "aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa",
                },
                new IdentityRole
                {
                    Id = "22222222-2222-2222-2222-222222222222",
                    Name = "Personal",
                    NormalizedName = "PERSONAL",
                    ConcurrencyStamp = "bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb",
                },
                new IdentityRole
                {
                    Id = "33333333-3333-3333-3333-333333333333",
                    Name = "Alumno",
                    NormalizedName = "ALUMNO",
                    ConcurrencyStamp = "cccccccc-cccc-cccc-cccc-cccccccccccc",
                }
            );
        }
    }
}