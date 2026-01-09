using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PortalCOSIE.Domain.Entities.Tramites;

namespace PortalCOSIE.Infrastructure.Persistence.Configurations.Tramites
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
               new { EstadoTramite.Solicitado.Id, EstadoTramite.Solicitado.Nombre, IsDeleted = false },
               new { EstadoTramite.EnRevision.Id, EstadoTramite.EnRevision.Nombre, IsDeleted = false },
               new { EstadoTramite.DocumentosPendientes.Id, EstadoTramite.DocumentosPendientes.Nombre, IsDeleted = false },
               new { EstadoTramite.Concluido.Id, EstadoTramite.Concluido.Nombre, IsDeleted = false },
               new { EstadoTramite.Cancelado.Id, EstadoTramite.Cancelado.Nombre, IsDeleted = false },
               new { EstadoTramite.EsperandoAcuse.Id, EstadoTramite.EsperandoAcuse.Nombre, IsDeleted = false }
           );
        }
    }
}