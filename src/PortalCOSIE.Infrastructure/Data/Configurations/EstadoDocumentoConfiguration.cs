using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PortalCOSIE.Domain.Entities.Tramites;

namespace PortalCOSIE.Infrastructure.Data.Configurations
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

            builder.HasData(
                new { Id = 1, Nombre = "Sin cargar", IsDeleted = false },
                new { Id = 2, Nombre = "Validado", IsDeleted = false },
                new { Id = 3, Nombre = "Con errores", IsDeleted = false },
                new { Id = 4, Nombre = "Mala calidad", IsDeleted = false },
                new { Id = 5, Nombre = "Documento equivocado", IsDeleted = false },
                new { Id = 6, Nombre = "Corrupto", IsDeleted = false }
            );
        }
    }
}