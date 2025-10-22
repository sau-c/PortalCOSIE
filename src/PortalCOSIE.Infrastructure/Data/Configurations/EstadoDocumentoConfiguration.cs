using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PortalCOSIE.Domain.Entities;

namespace PortalCOSIE.Infrastructure.Data.Configurations
{
    public class EstadoDocumentoConfiguration : IEntityTypeConfiguration<EstadoDocumento>
    {
        public void Configure(EntityTypeBuilder<EstadoDocumento> builder)
        {
            builder.ToTable("EstadoDocumento");

            builder.Property(d => d.Nombre)
                .IsRequired()
                .HasMaxLength(200);

            builder.HasData(
                new EstadoDocumento() { Id = 1, Nombre = "Sin cargar" },
                new EstadoDocumento() { Id = 2, Nombre = "Validado" },
                new EstadoDocumento() { Id = 3, Nombre = "Con errores" },
                new EstadoDocumento() { Id = 4, Nombre = "Mala calidad" },
                new EstadoDocumento() { Id = 5, Nombre = "Documento equivocado" },
                new EstadoDocumento() { Id = 6, Nombre = "Corrupto" }
                );
        }
    }
}