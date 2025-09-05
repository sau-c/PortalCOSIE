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
                new Carrera { Id = 1, Nombre = "Mecatr�nica"},
                new Carrera { Id = 2, Nombre = "Telem�tica"},
                new Carrera { Id = 3, Nombre = "Bi�nica"},
                new Carrera { Id = 4, Nombre = "Energ�a"},
                new Carrera { Id = 5, Nombre = "ISISA"}
            );
        }
    }
}