using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PortalCOSIE.Domain.Entities.Documentos;

namespace PortalCOSIE.Infrastructure.Persistence.Configurations.Documentos
{
    public class TipoDocumentoConfiguration : IEntityTypeConfiguration<TipoDocumento>
    {
        public void Configure(EntityTypeBuilder<TipoDocumento> builder)
        {
            builder.ToTable("TipoDocumento");

            builder.Property(t => t.Nombre)
                .IsRequired()
                .HasMaxLength(100);

            builder.HasIndex(d => d.Nombre)
                .IsUnique();

            builder.HasData(
               new { TipoDocumento.Identificacion.Id, TipoDocumento.Identificacion.Nombre, IsDeleted = false },
               new { TipoDocumento.BoletaGlobal.Id, TipoDocumento.BoletaGlobal.Nombre, IsDeleted = false },
               new { TipoDocumento.CartaExposicionMotivos.Id, TipoDocumento.CartaExposicionMotivos.Nombre, IsDeleted = false },
               new { TipoDocumento.Probatorios.Id, TipoDocumento.Probatorios.Nombre, IsDeleted = false }
            );
        }
    }
}