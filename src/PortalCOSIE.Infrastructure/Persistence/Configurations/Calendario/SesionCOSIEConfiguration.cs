using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PortalCOSIE.Domain.Entities.SesionesCOSIE;

namespace PortalCOSIE.Infrastructure.Persistence.Configurations.Calendario
{
    public class SesionCOSIEConfiguration : IEntityTypeConfiguration<SesionCOSIE>
    {
        public void Configure(EntityTypeBuilder<SesionCOSIE> builder)
        {
            builder.ToTable("SesionCOSIE");

            builder.Property(s => s.NumeroSesion)
                .IsRequired()
                .HasMaxLength(50);

            builder.HasIndex(s => s.NumeroSesion)
                .IsUnique();

            builder.HasMany(s => s.FechasRecepcion)
                .WithOne(f => f.Sesion)
                .HasForeignKey(f => f.SesionId);

            builder.HasData(
                new
                {
                    Id = 1,
                    NumeroSesion = "PRIMERA",
                    FechaSesion = (DateTime?)new DateTime(2025, 01, 10),
                    IsDeleted = false
                },
                new
                {
                    Id = 2,
                    NumeroSesion = "SEGUNDA",
                    FechaSesion = (DateTime?)new DateTime(2025, 01, 20),
                    IsDeleted = false
                },
                new
                {
                    Id = 3,
                    NumeroSesion = "TERCERA",
                    FechaSesion = (DateTime?)new DateTime(2025, 01, 30),
                    IsDeleted = false
                }
                );
        }
    }
}