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
        }
    }
}