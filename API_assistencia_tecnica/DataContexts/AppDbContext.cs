using Microsoft.EntityFrameworkCore;
using API_assistencia_tecnica.Models;

namespace API_assistencia_tecnica.DataContexts
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Equipamento> Equipamentos { get; set; }
        public DbSet<Fornecedor> Fornecedores { get; set; }
        public DbSet<Orcamento> Orcamentos { get; set; }
        public DbSet<Peca> Pecas { get; set; }
        public DbSet<Reparo> Reparos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Aqui você pode configurar relações e chaves estrangeiras
            // Exemplo (ajuste conforme sua modelagem):

            // modelBuilder.Entity<Reparo>()
            //     .HasOne(r => r.Cliente)
            //     .WithMany()
            //     .HasForeignKey(r => r.ClienteId)
            //     .IsRequired();

            base.OnModelCreating(modelBuilder);
        }
    }
}