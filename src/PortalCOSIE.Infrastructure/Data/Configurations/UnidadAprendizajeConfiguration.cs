using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PortalCOSIE.Domain.Entities;
using PortalCOSIE.Domain.Enums;

namespace PortalCOSIE.Infrastructure.Data.Configurations
{
    public class UnidadAprendizajeConfiguration : IEntityTypeConfiguration<UnidadAprendizaje>
    {
        public void Configure(EntityTypeBuilder<UnidadAprendizaje> builder)
        {
            builder.ToTable("UnidadAprendizaje");

            builder.Property(c => c.Nombre)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(c => c.Semestre)
                .IsRequired();
                
            builder.HasData(
                new UnidadAprendizaje(1, "Calculo diferencial e integral", 1, Semestre.PRIMERO),
                new UnidadAprendizaje(2, "Calculo vectorial", 1, Semestre.SEGUNDO),
                new UnidadAprendizaje(3, "Senales y sistemas", 1, Semestre.OCTAVO)
            );
        }
    }
}