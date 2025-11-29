using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PortalCOSIE.Domain.Entities.Documentos;

namespace PortalCOSIE.Infrastructure.Data.Configurations.Documentos
{
    public class DocumentoConfiguration : IEntityTypeConfiguration<Documento>
    {
        public void Configure(EntityTypeBuilder<Documento> builder)
        {
            builder.ToTable("Documento");

            // 1. Configuración del RowGuid (Requisito obligatorio de FILESTREAM)
            //builder.Property(e => e.RowGuid)
            //      .HasColumnName("RowGuid")
            //      .HasDefaultValueSql("NEWID()") // SQL genera el ID automáticamente
            //      .IsRequired();

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