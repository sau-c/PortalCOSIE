using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PortalCOSIE.Domain.Entities.Documentos;

namespace PortalCOSIE.Infrastructure.Persistence.Configurations.Documentos
{
    public class FirmaElectronicaConfiguration : IEntityTypeConfiguration<FirmaElectronica>
    {
        public void Configure(EntityTypeBuilder<FirmaElectronica> builder)
        {
            builder.ToTable("FirmaElectronica");

            builder.HasKey(f => f.Id);

            builder.Property(f => f.Id)
                .ValueGeneratedOnAdd();

            builder.Property(f => f.CertificadoId)
                .IsRequired()
                .HasMaxLength(64);

            builder.Property(f => f.FirmaCms)
                .HasColumnType("varbinary(max)")
                .IsRequired();

            builder.Property(f => f.Algoritmo)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(f => f.FechaFirmaUtc)
                .IsRequired();

            builder.Property(f => f.TokenVerificacion)
                .HasMaxLength(64);

            builder.HasIndex(f => f.TokenVerificacion)
                .IsUnique()
                .HasFilter("[TokenVerificacion] IS NOT NULL");

            builder.HasOne(f => f.Certificado)
                .WithMany()
                .HasForeignKey(f => f.CertificadoId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasIndex(f => f.CertificadoId);
        }
    }
}