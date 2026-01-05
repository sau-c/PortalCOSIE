using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PortalCOSIE.Domain.Entities.Documentos;

namespace PortalCOSIE.Infrastructure.Persistence.Configurations.Documentos
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

            builder.Property(d => d.BlobPath)
                .IsRequired()
                .HasMaxLength(300);

            builder.Property(builder => builder.HashOriginal)
                .IsRequired();

            builder.HasOne(d => d.Tramite)
                .WithMany(t => t.Documentos)
                .HasForeignKey(d => d.TramiteId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}