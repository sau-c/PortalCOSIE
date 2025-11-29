using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PortalCOSIE.Domain.Entities.Usuarios;

namespace PortalCOSIE.Infrastructure.Data.Configurations.Usuarios
{
    public class PersonalConfiguration : IEntityTypeConfiguration<Personal>
    {
        public void Configure(EntityTypeBuilder<Personal> builder)
        {
            builder.ToTable("Personal");

            builder.HasOne<Usuario>()      // Personal tiene un Usuario
                .WithOne()              // Usuario tiene Alumno
                .HasForeignKey<Personal>(a => a.Id)  // PK de Personal = FK a Usuario
                .OnDelete(DeleteBehavior.Restrict); // No Permite cascada
        }
    }
}