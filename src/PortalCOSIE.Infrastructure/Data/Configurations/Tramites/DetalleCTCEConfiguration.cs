using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PortalCOSIE.Domain.Entities.Tramites;

namespace PortalCOSIE.Infrastructure.Data.Configurations.Tramites
{
    public class DetalleCTCEConfiguration : IEntityTypeConfiguration<DetalleCTCE>
    {
        public void Configure(EntityTypeBuilder<DetalleCTCE> builder)
        {
            builder.ToTable("DetalleCTCE");

            builder.Property(t => t.Situacion)
                .HasMaxLength(1000)
                .IsRequired();

            builder.Property(t => t.TieneDictamenesAnteriores)
                .IsRequired();
        }
    }
}