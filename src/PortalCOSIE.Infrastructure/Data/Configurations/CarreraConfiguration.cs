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

            builder.HasData(
                new Carrera { Id = 1, Nombre = "Mecatrónica"},
                new Carrera { Id = 2, Nombre = "Telemática"},
                new Carrera { Id = 3, Nombre = "Biónica"},
                new Carrera { Id = 4, Nombre = "Energía"},
                new Carrera { Id = 5, Nombre = "ISISA"}
            );
        }
    }
}