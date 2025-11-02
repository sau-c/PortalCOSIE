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
                .IsRequired()
                .HasMaxLength(100);

            builder.HasIndex(d => d.Nombre)
                .IsUnique();

            builder.HasData(
                new { Id = 1, Nombre = "Dictamen interno (CTCE)", IsDeleted = false },
                new { Id = 2, Nombre = "Dictamen externo (CGC)", IsDeleted = false }
                );
        }
    }
}