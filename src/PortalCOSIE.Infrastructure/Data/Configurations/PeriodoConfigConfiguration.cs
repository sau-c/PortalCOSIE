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

            builder.Property(t => t.AnioInicio)
                .IsRequired()
                .HasMaxLength(4); 
            
            builder.Property(t => t.PeriodoInicio)
                .IsRequired()
                .HasMaxLength(1);

            builder.Property(t => t.AnioFin)
                .IsRequired()
                .HasMaxLength(4);

            builder.Property(t => t.PeriodoFin)
                .IsRequired()
                .HasMaxLength(1);

            builder.HasData(
                new { 
                    Id = 1,
                    AnioInicio = 2010,
                    PeriodoInicio = 1,
                    AnioFin = (DateTime.Now.Year + 1),
                    PeriodoFin = 2,
                    IsDeleted = false 
                });
        }
    }
}