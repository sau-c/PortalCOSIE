using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PortalCOSIE.Domain.Entities.Calendario;

namespace PortalCOSIE.Infrastructure.Persistence.Configurations.Calendario
{
    public class PeriodoConfigConfiguration : IEntityTypeConfiguration<PeriodoConfig>
    {
        public void Configure(EntityTypeBuilder<PeriodoConfig> builder)
        {
            builder.ToTable("PeriodoConfig");

            builder.Property(t => t.AnioInicio)
                .IsRequired()
                .HasMaxLength(4); 
            
            builder.Property(t => t.PeriodoInicio)
                .IsRequired()
                .HasMaxLength(1);

            builder.Property(t => t.AnioActual)
                .IsRequired()
                .HasMaxLength(4);

            builder.Property(t => t.PeriodoActual)
                .IsRequired()
                .HasMaxLength(1);

            builder.HasData(
                new { 
                    Id = 1,
                    AnioInicio = 1997,
                    PeriodoInicio = 1,
                    AnioActual = DateTime.Now.Year + 1,
                    PeriodoActual = 1,
                    IsDeleted = false 
                });
        }
    }
}