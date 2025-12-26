using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PortalCOSIE.Domain.Entities.Tramites;

namespace PortalCOSIE.Infrastructure.Persistence.Configurations.Tramites
{
    public class TipoTramiteConfiguration : IEntityTypeConfiguration<TipoTramite>
    {
        public void Configure(EntityTypeBuilder<TipoTramite> builder)
        {
            builder.ToTable("TipoTramite");

            builder.Property(t => t.Nombre)
                .IsRequired()
                .HasMaxLength(100);

            builder.HasIndex(d => d.Nombre)
                .IsUnique();

            builder.HasData(
               new { TipoTramite.DictamenInterno.Id, TipoTramite.DictamenInterno.Nombre, IsDeleted = false },
               new { TipoTramite.DictamenExterno.Id, TipoTramite.DictamenExterno.Nombre, IsDeleted = false }
            );
        }
    }
}