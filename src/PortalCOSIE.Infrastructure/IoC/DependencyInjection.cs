using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PortalCOSIE.Application.Interfaces;
using PortalCOSIE.Application.Services;
using PortalCOSIE.Domain.Entities.EntradaBitacoras;
using PortalCOSIE.Domain.Entities.SesionesCOSIE;
using PortalCOSIE.Domain.Entities.Carreras;
using PortalCOSIE.Domain.Entities.Tramites;
using PortalCOSIE.Domain.Entities.Usuarios;
using PortalCOSIE.Domain.Interfaces;
using PortalCOSIE.Infrastructure.Data;
using PortalCOSIE.Infrastructure.Data.Email;
using PortalCOSIE.Infrastructure.Data.Identity;
using PortalCOSIE.Infrastructure.Persistence;
using PortalCOSIE.Infrastructure.Repositories;
using PortalCOSIE.Infrastructure.QueryHandlers;

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

            // 1. Registrar la interfaz e implementación del Mediador con Scrutor
            services.AddTransient<IMediator, Mediator>();
            services.Scan(scan => scan
                .FromAssembliesOf(typeof(IMediator))
                .AddClasses(classes => classes.AssignableTo(typeof(IRequestHandler<,>)))
                .AsImplementedInterfaces()
                .WithTransientLifetime());

            //Servicios Infraestructura
            services.AddScoped<ISecurityService, SecurityService>();
            services.AddScoped<ICuentaCorreoService, CuentaCorreoService>();
            services.AddScoped<IDashboardQueryService, DashboardQueryService>();
            services.AddScoped<IEmailSender, SmtpEmailSender>();

            //Servicios Aplicacion
            services.AddScoped(typeof(ICatalogoService<,>), typeof(CatalogoService<,>));

            //Repositorios
            services.AddScoped(typeof(IBaseRepository<,>), typeof(BaseRepository<,>));
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddScoped<IUsuarioRepository, UsuarioRepository>();
            services.AddScoped<IBitacoraRepository, BitacoraRepository>();
            services.AddScoped<ISesionRepository, SesionRepository>();
            services.AddScoped<ICarreraRepository, CarreraRepository>();
            services.AddScoped<ITramiteRepository, TramiteRepository>();
            services.AddScoped<IDocumentoQueryService, DocumentoQueryService>();

            return services;
        }
    }
}
