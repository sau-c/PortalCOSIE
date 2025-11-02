using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PortalCOSIE.Domain.Entities;

namespace PortalCOSIE.Infrastructure.Data.Configurations
{
    public class FechaRecepcionConfiguration : IEntityTypeConfiguration<FechaRecepcion>
    {
        public void Configure(EntityTypeBuilder<FechaRecepcion> builder)
        {
            builder.ToTable("FechaRecepcion");

            builder.Property(s => s.SesionId)
                .IsRequired();

            builder.Property(s => s.Fecha)
                .IsRequired();

            builder.HasOne(f => f.Sesion)
            .WithMany(s => s.FechasRecepcion)
            .HasForeignKey(f => f.SesionId);
        }
    }
}