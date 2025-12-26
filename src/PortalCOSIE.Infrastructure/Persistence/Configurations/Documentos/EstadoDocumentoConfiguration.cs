using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PortalCOSIE.Domain.Entities.Documentos;

namespace PortalCOSIE.Infrastructure.Persistence.Configurations.Documentos
{
    public class EstadoDocumentoConfiguration : IEntityTypeConfiguration<EstadoDocumento>
    {
        public void Configure(EntityTypeBuilder<EstadoDocumento> builder)
        {
            builder.ToTable("EstadoDocumento");

            builder.Property(d => d.Nombre)
                .IsRequired()
                .HasMaxLength(100);

            builder.HasIndex(d => d.Nombre)
                .IsUnique();

            // Seed automático usando los valores de la Enumeration
            builder.HasData(
                new { EstadoDocumento.EnRevision.Id, EstadoDocumento.EnRevision.Nombre, IsDeleted = false },
                new { EstadoDocumento.Validado.Id, EstadoDocumento.Validado.Nombre, IsDeleted = false },
                new { EstadoDocumento.ConErrores.Id, EstadoDocumento.ConErrores.Nombre, IsDeleted = false },
                new { EstadoDocumento.Incorrecto.Id, EstadoDocumento.Incorrecto.Nombre, IsDeleted = false }
            );
        }
    }
}