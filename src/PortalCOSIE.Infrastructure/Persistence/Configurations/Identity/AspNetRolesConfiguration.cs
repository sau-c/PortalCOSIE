using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PortalCOSIE.Infrastructure.Persistence.Configurations.Identity
{
    public class AspNetRolesConfiguration : IEntityTypeConfiguration<IdentityRole>
    {
        private const string adminId = "11111111-1111-1111-1111-111111111111";
        private const string personalId = "22222222-2222-2222-2222-222222222222";
        private const string alumnoId = "33333333-3333-3333-3333-333333333333";
        public void Configure(EntityTypeBuilder<IdentityRole> builder)
        {
            builder.HasData(
                new IdentityRole
                {
                    Id = adminId,
                    Name = "Administrador",
                    NormalizedName = "ADMINISTRADOR",
                },
                new IdentityRole
                {
                    Id = personalId,
                    Name = "Personal",
                    NormalizedName = "PERSONAL",
                },
                new IdentityRole
                {
                    Id = alumnoId,
                    Name = "Alumno",
                    NormalizedName = "ALUMNO",
                }
            );
        }
    }
}