using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PortalCOSIE.Domain.Entities.Tramites;

namespace PortalCOSIE.Infrastructure.Data.Configurations
{
    public class DocumentoConfiguration : IEntityTypeConfiguration<Documento>
    {
        public void Configure(EntityTypeBuilder<Documento> builder)
        {
            builder.ToTable("Documento");

            builder.Property(d => d.Nombre)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(d => d.Observaciones)
                .HasMaxLength(1000);

            builder.HasOne(d => d.Tramite)
                .WithMany(t => t.Documentos)
                .HasForeignKey(d => d.TramiteId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}