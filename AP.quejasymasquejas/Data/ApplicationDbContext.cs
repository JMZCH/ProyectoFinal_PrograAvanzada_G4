using AP.quejasymasquejas.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AP.quejasymasquejas.Data
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Queja> Quejas { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Queja>(entity =>
            {
                entity.HasKey(q => q.QuejaID);

                entity.Property(q => q.Titulo)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(q => q.Descripcion)
                    .IsRequired();

                entity.HasOne(q => q.Usuario)
                    .WithMany()
                    .HasForeignKey(q => q.UsuarioId)
                    .OnDelete(DeleteBehavior.SetNull);
            });
        }
    }
}
