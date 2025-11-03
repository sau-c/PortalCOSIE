using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PortalCOSIE.Domain.Entities;
using PortalCOSIE.Domain.Entities.Usuarios;
using PortalCOSIE.Domain.Entities.Carreras;
using PortalCOSIE.Domain.Entities.Tramites;
using PortalCOSIE.Domain.Entities.Calendario;
using PortalCOSIE.Domain.Interfaces;
using PortalCOSIE.Application;
using PortalCOSIE.Application.Interfaces;
using PortalCOSIE.Application.Services;
using PortalCOSIE.Infrastructure.Data;
using PortalCOSIE.Infrastructure.Data.Email;
using PortalCOSIE.Infrastructure.Data.Identity;
using PortalCOSIE.Infrastructure.Repositories;

namespace PortalCOSIE.Infrastructure.IoC
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            // Configuración de Infrastructura
            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(
                    configuration.GetConnectionString("DefaultConnection"),
                    b => b.MigrationsAssembly(typeof(AppDbContext).Assembly.FullName)));


            //Servicios Infraestructura
            services.AddScoped<ISecurityService, SecurityService>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IEmailSender, SmtpEmailSender>();

            //Servicios Aplicacion
            services.AddScoped<IUsuarioService, UsuarioService>();
            services.AddScoped<ITramiteService, TramiteService>();
            services.AddScoped<ICatalogoService, CatalogoService>();
            services.AddScoped<ICarreraService, CarreraService>();

            //Repositorios
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IBaseRepository<Usuario>, BaseRepository<Usuario>>();
            services.AddScoped<IBaseRepository<Alumno>, BaseRepository<Alumno>>();
            services.AddScoped<IBaseRepository<Carrera>, BaseRepository<Carrera>>();
            services.AddScoped<IBaseRepository<Tramite>, BaseRepository<Tramite>>();
            services.AddScoped<IBaseRepository<EstadoTramite>, BaseRepository<EstadoTramite>>();
            services.AddScoped<IBaseRepository<TipoTramite>, BaseRepository<TipoTramite>>();
            services.AddScoped<IBaseRepository<Documento>, BaseRepository<Documento>>();
            services.AddScoped<IBaseRepository<EstadoDocumento>, BaseRepository<EstadoDocumento>>();
            services.AddScoped<IBaseRepository<UnidadAprendizaje>, BaseRepository<UnidadAprendizaje>>();
            services.AddScoped<IBaseRepository<SesionCOSIE>, BaseRepository<SesionCOSIE>>();
            services.AddScoped<IBaseRepository<PeriodoConfig>, BaseRepository<PeriodoConfig>>();

            return services;
        }
    }
}
