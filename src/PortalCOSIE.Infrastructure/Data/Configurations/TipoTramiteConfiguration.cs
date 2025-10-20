using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PortalCOSIE.Domain.Entities;

namespace PortalCOSIE.Infrastructure.Data.Configurations
{
    public class TipoTramiteConfiguration : IEntityTypeConfiguration<TipoTramite>
    {
        public void Configure(EntityTypeBuilder<TipoTramite> builder)
        {
            builder.ToTable("TipoTramite");

            builder.Property(t => t.Nombre)
                .IsRequired();

            builder.HasData(
                new TipoTramite() { Id = 1, Nombre = "Dictamen interno (CTE)" },
                new TipoTramite() { Id = 2, Nombre = "Dictamen externo (CGC)" }
                );
        }
    }
}