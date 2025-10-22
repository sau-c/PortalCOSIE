using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PortalCOSIE.Domain.Entities;

namespace PortalCOSIE.Infrastructure.Data.Configurations
{
    public class EstadoTramiteConfiguration : IEntityTypeConfiguration<EstadoTramite>
    {
        public void Configure(EntityTypeBuilder<EstadoTramite> builder)
        {
            builder.ToTable("EstadoTramite");

            builder.Property(t => t.Nombre)
                .IsRequired();

            builder.HasData(
                new EstadoTramite() { Id = 1, Nombre = "Solicitado" },
                new EstadoTramite() { Id = 2, Nombre = "En revision" },
                new EstadoTramite() { Id = 3, Nombre = "Documentos pendientes" },
                new EstadoTramite() { Id = 4, Nombre = "Concluido" },
                new EstadoTramite() { Id = 5, Nombre = "Cancelado" }
                );
        }
    }
}