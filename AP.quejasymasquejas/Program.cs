using AP.quejasymasquejas.Data;
using Microsoft.EntityFrameworkCore;

namespace AP.quejasymasquejas;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // DbContext (Foodbank)
        builder.Services.AddDbContext<ApplicationDbContext>(opts =>
            opts.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

        // Solo MVC (sin Identity)
        builder.Services.AddControllersWithViews();

        var app = builder.Build();

        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Home/Error");
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();
        app.UseRouting();
        app.UseAuthorization();

        // Página inicial → FoodItems/Index
        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=FoodItems}/{action=Index}/{id?}");

        app.Run();
    }
}
