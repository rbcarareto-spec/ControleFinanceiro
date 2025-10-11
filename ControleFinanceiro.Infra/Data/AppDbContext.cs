using ControleFinanceiro.Dominio.Entities;
using Microsoft.EntityFrameworkCore;

namespace ControleFinanceiro.Infra.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Categoria> Categorias { get; set; } = null!;
        public DbSet<Transacao> Transacoes { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Categoria>(entity =>
            {
                entity.HasKey(c => c.Id);
                entity.Property(c => c.Nome).IsRequired().HasMaxLength(100);
                entity.Property(c => c.Tipo).IsRequired();
                entity.Property(c => c.Ativo).IsRequired();
            });

            modelBuilder.Entity<Transacao>(entity =>
            {
                entity.HasKey(t => t.Id);
                entity.Property(t => t.Descricao).IsRequired().HasMaxLength(200);
                entity.Property(t => t.Valor).HasColumnType("decimal(18,2)").IsRequired();
                entity.Property(t => t.Data).IsRequired();
                entity.Property(t => t.Observacoes).HasMaxLength(500);
                entity.Property(t => t.DataCriacao).IsRequired();

                entity.HasOne(t => t.Categoria)
                      .WithMany(c => c.Transacoes)
                      .HasForeignKey(t => t.CategoriaId)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}
