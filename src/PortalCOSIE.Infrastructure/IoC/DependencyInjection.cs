using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
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
using PortalCOSIE.Domain.Entities.Carreras;
using PortalCOSIE.Domain.Entities.Calendario;
using PortalCOSIE.Domain.Entities.Bitacoras;
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
            services.AddScoped<IDashboardService, DashboardService>();
            services.AddScoped<IEmailSender, SmtpEmailSender>();

            //Servicios Aplicacion
            services.AddScoped(typeof(ICatalogoService<,>), typeof(CatalogoService<,>));

            services.AddScoped<IUsuarioService, UsuarioService>();
            services.AddScoped<ITramiteService, TramiteService>();
            services.AddScoped<IPeriodosService, PeriodosService>();
            services.AddScoped<ICarreraService, CarreraService>();
            services.AddScoped<IBitacoraService, BitacoraService>();

            //Repositorios
            services.AddScoped(typeof(IBaseRepository<,>), typeof(BaseRepository<,>));
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddScoped<IUsuarioRepository, UsuarioRepository>();
            services.AddScoped<IBitacoraRepository, BitacoraRepository>();
            services.AddScoped<ISesionRepository, SesionRepository>();
            services.AddScoped<ICarreraRepository, CarreraRepository>();
            services.AddScoped<ITramiteRepository, TramiteRepository>();
            services.AddScoped<IDocumentoRepository, DocumentoRepository>();

            return services;
        }
    }
}
