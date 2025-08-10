using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PortalCOSIE.Domain.Entities;

namespace PortalCOSIE.Infrastructure.Data.Configurations
{
    public class TramiteConfiguration : IEntityTypeConfiguration<Tramite>
    {
        public void Configure(EntityTypeBuilder<Tramite> builder)
        {
            builder.ToTable("Tramite");

            builder.Property(t => t.AlumnoId)
                .IsRequired();

            builder.Property(t => t.PersonalId)
                .IsRequired();

            builder.Property(t => t.FechaSolicitud)
                .IsRequired();

            builder.HasOne(t => t.Alumno)
                .WithMany()
                .HasForeignKey(t => t.AlumnoId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(t => t.Personal)
                .WithMany()
                .HasForeignKey(t => t.PersonalId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}