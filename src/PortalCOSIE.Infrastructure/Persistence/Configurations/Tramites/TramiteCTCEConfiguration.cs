using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PortalCOSIE.Domain.Entities.Tramites.CTCE;

namespace PortalCOSIE.Infrastructure.Persistence.Configurations.Tramites
{
    public class TramiteCTCEConfiguration : IEntityTypeConfiguration<TramiteCTCE>
    {
        public void Configure(EntityTypeBuilder<TramiteCTCE> builder)
        {
            builder.ToTable("TramiteCTCE");

            builder.Property(t => t.Peticion)
                .HasMaxLength(1000)
                .IsRequired();

            builder.Property(t => t.TieneDictamenesAnteriores)
                .IsRequired();
        }
    }
}