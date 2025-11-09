using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PortalCOSIE.Domain.Entities;
using PortalCOSIE.Domain.Entities.Calendario;
using PortalCOSIE.Domain.Entities.Carreras;
using PortalCOSIE.Domain.Entities.Tramites;
using PortalCOSIE.Domain.Entities.Usuarios;
using System.Linq.Expressions;

namespace PortalCOSIE.Infrastructure.Data
{
    public class AppDbContext : IdentityDbContext<IdentityUser, IdentityRole, string>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
        {
        }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Alumno> Alumnos { get; set; }
        public DbSet<Carrera> Carreras { get; set; }
        public DbSet<UnidadAprendizaje> UnidadesAprendizaje { get; set; }
        public DbSet<PeriodoConfig> PeriodosConfig { get; set; }
        public DbSet<FechaRecepcion> FechasRecepcion { get; set; }
        public DbSet<SesionCOSIE> SesionesCOSIE { get; set; }
        public DbSet<Tramite> Tramites { get; set; }
        public DbSet<EstadoDocumento> EstadosTramite { get; set; }
        public DbSet<TipoTramite> TiposTramite { get; set; }
        public DbSet<Documento> Documentos { get; set; }
        public DbSet<EstadoDocumento> EstadosDocumento { get; set; }
        public DbSet<Personal> Personales { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
        }
    }
}
