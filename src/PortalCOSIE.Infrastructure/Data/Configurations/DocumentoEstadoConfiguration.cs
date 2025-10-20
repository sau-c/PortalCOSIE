using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PortalCOSIE.Domain.Entities;

namespace PortalCOSIE.Infrastructure.Data.Configurations
{
    public class DocumentoEstadoConfiguration : IEntityTypeConfiguration<DocumentoEstado>
    {
        public void Configure(EntityTypeBuilder<DocumentoEstado> builder)
        {
            builder.ToTable("DocumentoEstado");

            builder.Property(d => d.Nombre)
                .IsRequired()
                .HasMaxLength(200);

            builder.HasData(
                new DocumentoEstado() { Id = 1, Nombre = "Sin cargar" },
                new DocumentoEstado() { Id = 2, Nombre = "Validado" },
                new DocumentoEstado() { Id = 3, Nombre = "Con errores" },
                new DocumentoEstado() { Id = 4, Nombre = "Mala calidad" },
                new DocumentoEstado() { Id = 5, Nombre = "Documento equivocado" },
                new DocumentoEstado() { Id = 6, Nombre = "Corrupto" }
                );
        }
    }
}