using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PortalCOSIE.Domain.Entities;
using System;
using System.Reflection.Emit;

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
        public DbSet<PlanEstudio> PlanesEstudio { get; set; }
        public DbSet<Tramite> Tramites { get; set; }
        public DbSet<Documento> Documentos { get; set; }
        public DbSet<Personal> Personales { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            var identityUsers = new List<IdentityUser>();
            var usuarios = new List<Usuario>();

            for (int i = 0; i < 20; i++)
            {
                identityUsers.Add(new IdentityUser
                {
                    Id = $"00000000-0000-0000-0000-00000000000{i + 1}",
                    UserName = $"correo{i}@gmail.com",
                    NormalizedUserName = $"CORREO{i}@GMAIL.COM",
                    Email = $"correo{i}@gmail.com",
                    NormalizedEmail = $"CORREO{i}@GMAIL.COM",
                    EmailConfirmed = true,
                    PasswordHash = "AQAAAAIAAYagAAAAEEFsRVgAvdDEsT2uuQ2Jt59XwO2oGIKQoqpJboUjJRIe6LjN1aSqtor4Jo1ioVdWYg==", // Usa un hash real
                    SecurityStamp = $"00000000-0000-0000-0000-00000000000{i + 1}",
                    ConcurrencyStamp = $"00000000-0000-0000-0000-00000000000{i + 1}"
                });

                usuarios.Add(new Usuario
                {
                    Id = (i + 1),
                    IdentityUserId = identityUsers[i].Id,
                    Nombre = $"Nombre{i}",
                    ApellidoPaterno = $"ApellidoPaterno{i}",
                    ApellidoMaterno = $"ApellidoMaterno{i}"
                });
            }

            modelBuilder.Entity<IdentityUser>().HasData(identityUsers);
            modelBuilder.Entity<Usuario>().HasData(usuarios);

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
        }
    }
}
