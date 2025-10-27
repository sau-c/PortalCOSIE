using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PortalCOSIE.Domain.Entities;

namespace PortalCOSIE.Infrastructure.Data.Configurations
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

            builder.HasData(
                new
                {
                    Id = 1,
                    NumeroSesion = "PRIMERA",
                    FechaSesion = (DateTime?)new DateTime(2025, 7, 20),
                    IsDeleted = false
                },
                new
                {
                    Id = 2,
                    NumeroSesion = "SEGUNDA",
                    FechaSesion = (DateTime?)new DateTime(2025, 8, 21),
                    IsDeleted = false
                },
                new
                {
                    Id = 3,
                    NumeroSesion = "TERCERA",
                    FechaSesion = (DateTime?)new DateTime(2025, 9, 22),
                    IsDeleted = false
                }
                );
        }
    }
}