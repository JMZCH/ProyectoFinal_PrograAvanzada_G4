using AP.quejasymasquejas.Models;
using AP.quejasymasquejas.web.Models;
using Microsoft.EntityFrameworkCore;

namespace AP.quejasymasquejas.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        public DbSet<Queja> Quejas { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<FoodItem> FoodItems { get; set; }

        public DbSet<Role> Roles { get; set; }       // dbo.Roles
        public DbSet<User> Users { get; set; }       // dbo.Users
        public DbSet<UserRole> UserRoles { get; set; } // dbo.UserRoles

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Mapea al esquema dbo explícitamente
            modelBuilder.Entity<FoodItem>().ToTable("FoodItems", "dbo");
            modelBuilder.Entity<Queja>().ToTable("Quejas", "dbo");
            modelBuilder.Entity<Usuario>().ToTable("Usuarios", "dbo");

            modelBuilder.Entity<Role>(e =>
            {
                e.ToTable("Roles", "dbo");
                e.HasKey(x => x.RoleId);
                e.Property(x => x.RoleId);
                e.Property(x => x.RoleName).IsRequired();
            });

            modelBuilder.Entity<User>(e =>
            {
                e.ToTable("Users", "dbo");
                e.HasKey(x => x.Id);
                e.Property(x => x.Id);
                e.Property(x => x.Email).IsRequired();
            });

            modelBuilder.Entity<UserRole>(e =>
            {
                e.ToTable("UserRoles", "dbo");
                e.HasKey(x => new { x.UserId, x.RoleId });  // PK compuesta
                e.Property(x => x.UserId);
                e.Property(x => x.RoleId);
            });
        }
    }
}
