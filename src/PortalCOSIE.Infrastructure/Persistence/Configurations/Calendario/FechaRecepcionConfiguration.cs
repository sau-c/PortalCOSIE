using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PortalCOSIE.Domain.Entities.SesionesCOSIE;

namespace PortalCOSIE.Infrastructure.Persistence.Configurations.Calendario
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

            builder.HasData(
                new { Id = 1, SesionId = 1, Fecha = new DateTime(2025, 01, 1), IsDeleted = false},
                new { Id = 2, SesionId = 1, Fecha = new DateTime(2025, 01, 2), IsDeleted = false},
                new { Id = 3, SesionId = 2, Fecha = new DateTime(2025, 01, 15), IsDeleted = false},
                new { Id = 4, SesionId = 2, Fecha = new DateTime(2025, 01, 16), IsDeleted = false},
                new { Id = 5, SesionId = 3, Fecha = new DateTime(2025, 01, 25), IsDeleted = false}
            );
        }
    }
}