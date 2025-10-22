using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PortalCOSIE.Domain.Entities;

namespace PortalCOSIE.Infrastructure.Data.Configurations
{
    public class PeriodoConfigConfiguration : IEntityTypeConfiguration<PeriodoConfig>
    {
        public void Configure(EntityTypeBuilder<PeriodoConfig> builder)
        {
            builder.ToTable("PeriodoConfig");

            builder.Property(t => t.PeriodoInicio)
                .IsRequired()
                .HasMaxLength(5);

            builder.Property(t => t.PeriodoFin)
                .IsRequired()
                .HasMaxLength(5);

            builder.Property(t => t.PeriodosPorAnio)
                .IsRequired();

            builder.HasData(
                new PeriodoConfig() {
                    Id = 1,
                    PeriodoInicio = "20101",
                    PeriodoFin = "20202",
                    PeriodosPorAnio = 2
                });
        }
    }
}