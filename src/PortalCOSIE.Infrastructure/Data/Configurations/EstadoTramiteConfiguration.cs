using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PortalCOSIE.Domain.Entities.Tramites;

namespace PortalCOSIE.Infrastructure.Data.Configurations
{
    public class EstadoTramiteConfiguration : IEntityTypeConfiguration<EstadoTramite>
    {
        public void Configure(EntityTypeBuilder<EstadoTramite> builder)
        {
            builder.ToTable("EstadoTramite");

            builder.Property(t => t.Nombre)
                .IsRequired()
                .HasMaxLength(100);

            builder.HasIndex(d => d.Nombre)
                .IsUnique();

            builder.HasData(
                new { Id = 1, Nombre = "Solicitado", IsDeleted = false },
                new { Id = 2, Nombre = "En revision", IsDeleted = false },
                new { Id = 3, Nombre = "Documentos pendientes", IsDeleted = false },
                new { Id = 4, Nombre = "Concluido", IsDeleted = false },
                new { Id = 5, Nombre = "Cancelado", IsDeleted = false }
                );
        }
    }
}