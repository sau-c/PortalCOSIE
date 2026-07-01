using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PortalCOSIE.Domain.Entities.Usuarios;

namespace PortalCOSIE.Infrastructure.Persistence.Configurations.Usuarios
{
    public class CertificadoConfiguration : IEntityTypeConfiguration<Certificado>
    {
        public void Configure(EntityTypeBuilder<Certificado> builder)
        {
            builder.ToTable("Certificado");

            builder.Property(c => c.Id)
                .HasMaxLength(64)
                .IsRequired();

            builder.Property(c => c.Sujeto)
                .IsRequired()
                .HasMaxLength(500);

            builder.Property(c => c.NumeroSerie)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(c => c.VigenteDesde)
                .IsRequired();

            builder.Property(c => c.VigenteHasta)
                .IsRequired();

            builder.Property(c => c.CertificadoDer)
                .IsRequired()
                .HasColumnType("varbinary(max)");
        }
    }
}