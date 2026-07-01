using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PortalCOSIE.Domain.Entities.Usuarios;
using System.Reflection.Emit;

namespace PortalCOSIE.Infrastructure.Persistence.Configurations.Usuarios
{
    public class UsuarioConfiguration : IEntityTypeConfiguration<Usuario>
    {
        public void Configure(EntityTypeBuilder<Usuario> builder)
        {
            builder.ToTable("Usuario");
            builder.UseTptMappingStrategy();

            builder.Property(u => u.Nombre)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(u => u.ApellidoPaterno)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(u => u.ApellidoMaterno)
                .IsRequired()
                .HasMaxLength(100);

            //Conexion con IdentityUser desacoplada de la tabla Usuario
            builder.HasOne<IdentityUser>()
            .WithMany()
            .HasForeignKey(a => a.IdentityUserId);

            builder.Property(u => u.CertificadoId)
                .HasMaxLength(64);

            builder.HasOne(u => u.Certificado)
                .WithOne()
                .HasForeignKey<Usuario>(u => u.CertificadoId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasIndex(u => u.CertificadoId)
                .IsUnique()
                .HasFilter("[CertificadoId] IS NOT NULL");
        }
    }
}
