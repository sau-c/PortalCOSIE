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

            builder.Property(c => c.Descripcion)
                .HasMaxLength(250);

            builder.HasData(
                new Carrera { Id = 1, Nombre = "Mecatr�nica", Descripcion = "Carrera centrada en la optimizaci�n de procesos tecnologicos e industriales." },
                new Carrera { Id = 2, Nombre = "Telem�tica", Descripcion = "Carrera enfocada en el desarrollo de telecomunicaciones y sistemas computacionales." },
                new Carrera { Id = 3, Nombre = "Bi�nica", Descripcion = "Carrera centrada en la optimizaci�n de procesos." },
                new Carrera { Id = 4, Nombre = "Energ�a", Descripcion = "Carrera centrada en la optimizaci�n de procesos." },
                new Carrera { Id = 5, Nombre = "ISISA", Descripcion = "Carrera centrada en la optimizaci�n de procesos." }
            );
        }
    }
}