using AP.quejasymasquejas.Models.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AP.quejasymasquejas.Data.Context
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
                entity.ToTable("Quejas");
                
                entity.HasKey(q => q.QuejaId);

                entity.Property(q => q.Titulo)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(q => q.Descripcion)
                    .IsRequired();

                entity.Property(q => q.Estado)
                    .HasMaxLength(50)
                    .HasDefaultValue("Pendiente");

                entity.Property(q => q.Prioridad)
                    .HasMaxLength(50)
                    .HasDefaultValue("Normal");

                entity.Property(q => q.Categoria)
                    .HasMaxLength(50)
                    .HasDefaultValue("General");

                entity.Property(q => q.FechaRegistro)
                    .HasDefaultValueSql("GETDATE()");

                entity.HasOne(q => q.Usuario)
                    .WithMany()
                    .HasForeignKey(q => q.UsuarioId)
                    .OnDelete(DeleteBehavior.SetNull);

                entity.HasIndex(q => q.UsuarioId);
                entity.HasIndex(q => q.FechaRegistro);
                entity.HasIndex(q => q.Estado);
            });
        }
    }
}
