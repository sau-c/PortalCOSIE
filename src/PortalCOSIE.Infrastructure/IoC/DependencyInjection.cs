using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PortalCOSIE.Domain.Entities;
using PortalCOSIE.Domain.Entities.Usuarios;
using PortalCOSIE.Domain.Entities.Tramites;
using PortalCOSIE.Domain.Interfaces;
using PortalCOSIE.Application;
using PortalCOSIE.Application.Interfaces;
using PortalCOSIE.Application.Services;
using PortalCOSIE.Infrastructure.Data;
using PortalCOSIE.Infrastructure.Data.Email;
using PortalCOSIE.Infrastructure.Data.Identity;
using PortalCOSIE.Infrastructure.Repositories;
using Infrastructure.Data;

namespace PortalCOSIE.Infrastructure.IoC
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<AuditInterceptor>();
            services.AddDbContext<AppDbContext>((serviceProvider, options) =>
            {
                var auditInterceptor = serviceProvider.GetRequiredService<AuditInterceptor>();

                options.UseSqlServer(
                    configuration.GetConnectionString("DefaultConnection"),
                    b => b.MigrationsAssembly(typeof(AppDbContext).Assembly.FullName))
                    .AddInterceptors(auditInterceptor);
            });

            //Servicios Infraestructura
            services.AddScoped<ISecurityService, SecurityService>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IEmailSender, SmtpEmailSender>();

            //Servicios Aplicacion
            services.AddScoped<IUsuarioService, UsuarioService>();
            services.AddScoped<ITramiteService, TramiteService>();
            services.AddScoped<ICatalogoService, CatalogoService>();
            services.AddScoped<ICarreraService, CarreraService>();
            services.AddScoped<IEstadoDocumentoService, EstadoDocumentoService>();
            services.AddScoped<IEstadoTramiteService, EstadoTramiteService>();
            services.AddScoped<ITipoTramiteService, TipoTramiteService>();
            services.AddScoped<IBitacoraService, BitacoraService>();

            //Repositorios
            services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddScoped<IUsuarioRepository, UsuarioRepository>();
            services.AddScoped<ISesionRepository, SesionRepository>();
            services.AddScoped<ICarreraRepository, CarreraRepository>();
            services.AddScoped<IBaseRepository<Alumno>, BaseRepository<Alumno>>();
            services.AddScoped<IBaseRepository<Tramite>, BaseRepository<Tramite>>();
            services.AddScoped<IBaseRepository<EstadoDocumento>, BaseRepository<EstadoDocumento>>();
            services.AddScoped<IBaseRepository<TipoTramite>, BaseRepository<TipoTramite>>();
            services.AddScoped<IBaseRepository<Documento>, BaseRepository<Documento>>();
            services.AddScoped<IBaseRepository<EstadoDocumento>, BaseRepository<EstadoDocumento>>();
            services.AddScoped<IBaseRepository<PeriodoConfig>, BaseRepository<PeriodoConfig>>();

            return services;
        }
    }
}
