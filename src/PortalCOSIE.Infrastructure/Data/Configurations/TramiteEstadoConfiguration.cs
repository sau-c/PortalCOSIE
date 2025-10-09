using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PortalCOSIE.Domain.Entities;
using PortalCOSIE.Domain.Enums;

namespace PortalCOSIE.Infrastructure.Data.Configurations
{
    public class TramiteEstadoConfiguration : IEntityTypeConfiguration<TramiteEstado>
    {
        public void Configure(EntityTypeBuilder<TramiteEstado> builder)
        {
            builder.ToTable("TramiteEstado");

            builder.Property(t => t.Nombre)
                .IsRequired();

            //builder.HasData(
            //    new TramiteEstado (){ Id = 1, Nombre = "Solicitado" },
            //    new TramiteEstado (){ Id = 2, Nombre = "En revision" },
            //    new TramiteEstado (){ Id = 3, Nombre = "Documentos erroneos" },
            //    new TramiteEstado (){ Id = 4, Nombre = "Concluido" }
            //    );
        }
    }
}