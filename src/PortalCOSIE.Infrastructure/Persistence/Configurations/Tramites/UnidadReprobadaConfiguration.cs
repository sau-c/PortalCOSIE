using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PortalCOSIE.Domain.Entities.Tramites.CTCE;

namespace PortalCOSIE.Infrastructure.Persistence.Configurations.Tramites
{
    public class UnidadReprobadaConfiguration : IEntityTypeConfiguration<UnidadReprobada>
    {
        public void Configure(EntityTypeBuilder<UnidadReprobada> builder)
        {
            builder.ToTable("UnidadReprobada");

            builder.Property(p => p.PeriodoCurso)
                .IsRequired();

            builder.Property(p => p.PeriodoRecurse)
                .IsRequired();

            builder.HasOne(m => m.UnidadAprendizaje)
                .WithMany().
                HasForeignKey(m => m.UnidadAprendizajeId);
            
            builder.HasOne(m => m.TramiteCTCE)
                .WithMany(t => t.UnidadesReprobadas)
                .HasForeignKey(m => m.TramiteCTCEId);
        }
    }
}