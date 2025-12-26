using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PortalCOSIE.Domain.Entities.Usuarios;

namespace PortalCOSIE.Infrastructure.Persistence.Configurations.Usuarios
{
    public class AlumnoConfiguration : IEntityTypeConfiguration<Alumno>
    {
        public void Configure(EntityTypeBuilder<Alumno> builder)
        {
            builder.ToTable("Alumno");

            builder.Property(a => a.NumeroBoleta)
                .IsRequired()
                .HasMaxLength(10);

            builder.HasIndex(a => a.NumeroBoleta)
                .IsUnique();

            builder.Property(a => a.PeriodoIngreso)
                .IsRequired();

            builder.Property(a => a.CarreraId)
                .IsRequired();

            builder.HasOne(a => a.Carrera)
                .WithMany()
                .HasForeignKey(a => a.CarreraId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne<Usuario>()      // Alumno tiene un Usuario
                .WithOne()              // Usuario tiene Alumno
                .HasForeignKey<Alumno>(a => a.Id)  // PK de Alumno = FK a Usuario
                .OnDelete(DeleteBehavior.Restrict); // Permite cascada
        }
    }
}