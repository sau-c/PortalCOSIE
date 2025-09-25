using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PortalCOSIE.Domain.Entities;
using PortalCOSIE.Domain.Interfaces;
using PortalCOSIE.Application;
using PortalCOSIE.Application.Interfaces;
using PortalCOSIE.Infrastructure.Data;
using PortalCOSIE.Infrastructure.Data.Email;
using PortalCOSIE.Infrastructure.Data.Identity;
using PortalCOSIE.Infrastructure.Repositories;
using PortalCOSIE.Application.Services;

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


            services.AddScoped<ISecurityService, SecurityService>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IEmailSender, SmtpEmailSender>();
            services.AddScoped<ICatalogoService, CatalogoService>();

            //Servicios
            services.AddScoped<IUsuarioService, UsuarioService>();
            services.AddScoped<ITramiteService, TramiteService>();

            //Repositorios
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IGenericRepo<Usuario>, GenericRepo<Usuario>>();
            services.AddScoped<IGenericRepo<Alumno>, GenericRepo<Alumno>>();
            services.AddScoped<IGenericRepo<Carrera>, GenericRepo<Carrera>>();
            services.AddScoped<IGenericRepo<Tramite>, GenericRepo<Tramite>>();
            services.AddScoped<IGenericRepo<UnidadAprendizaje>, UnidadAprendizajeRepo>();

            return services;
        }
    }
}
