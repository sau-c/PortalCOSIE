using Azure.Storage.Blobs;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PortalCOSIE.Application.Services;
using PortalCOSIE.Application.Services.Crypto;
using PortalCOSIE.Application.Services.Query;
using PortalCOSIE.Application.Services.Storage;
using PortalCOSIE.Domain.Entities.Carreras;
using PortalCOSIE.Domain.Entities.EntradaBitacoras;
using PortalCOSIE.Domain.Entities.SesionesCOSIE;
using PortalCOSIE.Domain.Entities.Tramites;
using PortalCOSIE.Domain.Entities.Usuarios;
using PortalCOSIE.Domain.Interfaces;
using PortalCOSIE.Infrastructure.Persistence;
using PortalCOSIE.Infrastructure.QueryHandlers;
using PortalCOSIE.Infrastructure.QueryService;
using PortalCOSIE.Infrastructure.Repositories;
using PortalCOSIE.Infrastructure.Services;

namespace PortalCOSIE.Infrastructure.IoC
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            // --- DbContext ---
            services.AddScoped<AuditInterceptor>();
            services.AddDbContext<AppDbContext>((serviceProvider, options) =>
            {
                var auditInterceptor = serviceProvider.GetRequiredService<AuditInterceptor>();

                options.UseSqlServer(
                    configuration.GetConnectionString("DefaultConnection"),
                    b => b.MigrationsAssembly(typeof(AppDbContext).Assembly.FullName))
                    .AddInterceptors(auditInterceptor);
            });

            // --- BlobStorage ---
            var blobConnectionString = configuration.GetConnectionString("AzureBlobStorage");
            var containerName = configuration["AzureBlob:ContainerName"];

            services.AddSingleton(sp =>
            {
                var blobServiceClient = new BlobServiceClient(blobConnectionString);
                var containerClient = blobServiceClient.GetBlobContainerClient(containerName);
                containerClient.CreateIfNotExists(); // opcional, crea contenedor si no existe
                return containerClient;
            });

            // --- Mediador con Handlers usando Scrutor ---
            services.AddTransient<IMediator, Mediator>();
            services.Scan(scan => scan
                .FromAssembliesOf(typeof(IMediator))
                .AddClasses(classes => classes.AssignableTo(typeof(IRequestHandler<,>)))
                .AsImplementedInterfaces()
                .WithTransientLifetime());

            //Servicios Infraestructura
            services.AddScoped<ISecurityService, SecurityService>();
            services.AddScoped<IDashboardQueryService, DashboardQueryService>();
            services.AddScoped<IStorageService, AzureStorageService>();
            services.AddScoped<IUsuarioQueryService, UsuarioQueryService>();
            services.AddScoped<IEmailSender, SmtpEmailSender>();
            services.AddScoped<ICriptoService, CriptoService>();

            //Repositorios
            services.AddScoped(typeof(IBaseRepository<,>), typeof(BaseRepository<,>));
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddScoped<IUsuarioRepository, UsuarioRepository>();
            services.AddScoped<IBitacoraRepository, BitacoraRepository>();
            services.AddScoped<ISesionRepository, SesionRepository>();
            services.AddScoped<ICarreraRepository, CarreraRepository>();
            services.AddScoped<ITramiteRepository, TramiteRepository>();

            return services;
        }
    }
}
