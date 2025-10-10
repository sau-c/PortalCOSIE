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
                new Carrera("Mecatr�nica") { Id = 1 },
                new Carrera("Telem�tica") { Id = 2 },
                new Carrera("Bi�nica") { Id = 3 },
                new Carrera("Energ�a") { Id = 4 },
                new Carrera("ISISA") { Id = 5 }
            );
        }
    }
}