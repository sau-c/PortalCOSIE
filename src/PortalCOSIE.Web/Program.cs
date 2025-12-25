using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PortalCOSIE.Infrastructure.Data;
using PortalCOSIE.Infrastructure.Data.Identity;
using PortalCOSIE.Infrastructure.IoC;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddHttpContextAccessor();

builder.Services.AddInfrastructure(builder.Configuration);

builder.Services.AddIdentity<IdentityUser, IdentityRole>(options =>
{
    options.SignIn.RequireConfirmedEmail = true;

    //Lockout (bloqueo tras intentos fallidos)
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);  // Tiempo bloqueado
    options.Lockout.MaxFailedAccessAttempts = 5;                       // Intentos fallidos antes del bloqueo
})
    .AddEntityFrameworkStores<AppDbContext>()
    .AddDefaultTokenProviders();

builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Cuenta/Ingresar";
    options.LogoutPath = "/Cuenta/Salir";
    options.AccessDeniedPath = "/Cuenta/Denegado";
    options.Cookie.Name = "AuthCookie";
    options.Cookie.HttpOnly = true; //No accesible desde JS (XSS)
    options.Cookie.SecurePolicy = CookieSecurePolicy.Always; //Solo para HTTPS
    options.Cookie.SameSite = SameSiteMode.Strict; //Solo peticiones desde el sitio
    options.SlidingExpiration = true;   //Cada request -> Reinicia ExpireTimeSpan
    options.ExpireTimeSpan = TimeSpan.FromMinutes(5); //Duracion de sesion
});

builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Shared/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();
//MANJEO DE EXCEPCION GLOBAL REVISAR
app.UseMiddleware<GlobalExceptionHandlingMiddleware>();

//using (var scope = app.Services.CreateScope())
//{
//    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
//    db.Database.Migrate();
//}
using (var scope = app.Services.CreateScope())
{
    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();
    var configuration = scope.ServiceProvider.GetRequiredService<IConfiguration>();
    await DataSeeder.SeedIdentityAsync(userManager, configuration);
}

// En caso de tener areas, descomentar la siguiente linea
//app.MapControllerRoute(
//    name: "areas",
//    pattern: "{area:exists}/{controller=Calendario}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Calendario}/{action=Index}/{id?}");

app.Run();
