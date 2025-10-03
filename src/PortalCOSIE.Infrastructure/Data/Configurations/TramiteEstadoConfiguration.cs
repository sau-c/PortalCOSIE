using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PortalCOSIE.Domain.Entities;

namespace PortalCOSIE.Infrastructure.Data.Configurations
{
    public class TramiteEstadoConfiguration : IEntityTypeConfiguration<TramiteEstado>
    {
        public void Configure(EntityTypeBuilder<TramiteEstado> builder)
        {
            builder.ToTable("TramiteEstado");

            builder.Property(t => t.Nombre)
                .IsRequired();
        }
    }
}