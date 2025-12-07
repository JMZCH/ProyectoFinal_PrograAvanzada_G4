using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using AP.quejasymasquejas.Data.Context;
using AP.quejasymasquejas.Data.Repositories.Interfaces;
using AP.quejasymasquejas.Data.Repositories.Implementations;
using AP.quejasymasquejas.Business.Interfaces;
using AP.quejasymasquejas.Business.Services;

var builder = WebApplication.CreateBuilder(args);

// ===========================================
// Configuración de Servicios (Dependency Injection)
// Principio SOLID: Dependency Inversion
// ===========================================

// Configuración de base de datos
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
    ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

// Configuración de Identity
builder.Services.AddDefaultIdentity<IdentityUser>(options => 
{
    options.SignIn.RequireConfirmedAccount = false; // Cambiar a true en producción
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireUppercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequiredLength = 6;
})
.AddEntityFrameworkStores<ApplicationDbContext>();

// ===========================================
// Registro de Repositorios (Data Layer)
// ===========================================
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped<IQuejaRepository, QuejaRepository>();

// ===========================================
// Registro de Servicios (Business Layer)
// ===========================================
builder.Services.AddScoped<IQuejaService, QuejaService>();

// Configuración de MVC y Razor Pages
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

var app = builder.Build();

// ===========================================
// Configuración del Pipeline HTTP
// ===========================================

if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

// Rutas
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();
