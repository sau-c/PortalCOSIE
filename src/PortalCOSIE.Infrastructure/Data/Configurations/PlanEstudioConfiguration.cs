using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PortalCOSIE.Domain.Entities;

namespace PortalCOSIE.Infrastructure.Data.Configurations
{
    public class PlanEstudioConfiguration : IEntityTypeConfiguration<PlanEstudio>
    {
        public void Configure(EntityTypeBuilder<PlanEstudio> builder)
        {
            builder.ToTable("PlanEstudio");

            builder.Property(p => p.Nombre)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(p => p.Descripcion)
                .HasMaxLength(250);

            builder.HasData(
                new PlanEstudio { Id = 1, Nombre = "1998", Descripcion = "Primer plan" },
                new PlanEstudio { Id = 2, Nombre = "2009", Descripcion = "Segundo plan" }
                );
        }
    }
}