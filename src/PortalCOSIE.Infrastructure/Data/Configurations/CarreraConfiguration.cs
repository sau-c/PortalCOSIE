using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PortalCOSIE.Domain.Entities;

namespace PortalCOSIE.Infrastructure.Data.Configurations
{
    public class CarreraConfiguration : IEntityTypeConfiguration<Carrera>
    {
        public void Configure(EntityTypeBuilder<Carrera> builder)
        {
            builder.ToTable("Carrera");

            builder.Property(c => c.Nombre)
                .IsRequired()
                .HasMaxLength(100);

            builder.HasIndex(d => d.Nombre)
                .IsUnique();

            builder.HasData(
                new { Id = 1, Nombre = "Mecatrónica", IsDeleted = false },
                new { Id = 2, Nombre = "Telemática", IsDeleted = false },
                new { Id = 3, Nombre = "Biónica", IsDeleted = false },
                new { Id = 4, Nombre = "Energía", IsDeleted = false },
                new { Id = 5, Nombre = "ISISA", IsDeleted = false }
            );
        }
    }
}