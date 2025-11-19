using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PortalCOSIE.Domain.Entities.Usuarios;

namespace PortalCOSIE.Infrastructure.Data.Configurations.Usuarios
{
    public class UsuarioConfiguration : IEntityTypeConfiguration<Usuario>
    {
        public void Configure(EntityTypeBuilder<Usuario> builder)
        {
            builder.ToTable("Usuario");

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

            builder.HasOne(u => u.Alumno)
                .WithOne(a => a.Usuario)
                .HasForeignKey<Alumno>(a => a.Id)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(u => u.Personal)
                .WithOne(p => p.Usuario)
                .HasForeignKey<Personal>(p => p.Id)
                .OnDelete(DeleteBehavior.Cascade);
            ;
        }
    }
}
