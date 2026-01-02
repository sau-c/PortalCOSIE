using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PortalCOSIE.Domain.Entities.EntradaBitacoras;

namespace PortalCOSIE.Infrastructure.Persistence.Configurations.Bitacoras
{
    public class EntradaBitacoraConfiguration : IEntityTypeConfiguration<EntradaBitacora>
    {
        public void Configure(EntityTypeBuilder<EntradaBitacora> builder)
        {
            builder.ToTable("Bitacora");

            //Conexion con IdentityUser desacoplada de la tabla Usuario
            builder.HasOne<IdentityUser>()
            .WithMany()
            .HasForeignKey(a => a.IdentityUserId);
        }
    }
}
