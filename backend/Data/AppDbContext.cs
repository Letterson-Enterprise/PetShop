using Microsoft.EntityFrameworkCore;
using backend_petshop.Entities;

namespace backend_petshop.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Usuario> Usuarios { get; set; } = null!;
        public DbSet<Endereco> Enderecos { get; set; } = null!;
        public DbSet<Tutor> Tutores { get; set; } = null!;
        public DbSet<Animal> Animais { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Usuario>()
                .HasIndex(u => u.Login)
                .IsUnique();

            modelBuilder.Entity<Tutor>()
                .HasOne(t => t.Endereco)
                .WithMany()
                .HasForeignKey(t => t.EnderecoId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Animal>()
                .HasOne(a => a.Tutor)
                .WithMany(t => t.Animais)
                .HasForeignKey(a => a.TutorId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
