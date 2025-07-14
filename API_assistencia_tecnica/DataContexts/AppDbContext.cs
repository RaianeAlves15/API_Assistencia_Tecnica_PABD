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
        public DbSet<OrcamentoPeca> OrcamentoPecas { get; set; }
        public DbSet<ReparoEquipamento> ReparoEquipamentos { get; set; }
        public DbSet<FornecedorPeca> FornecedorPecas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Relacionamento N:N - Orcamento × Peca
            modelBuilder.Entity<OrcamentoPeca>()
                .HasKey(op => new { op.OrcamentoId, op.PecaId });

            modelBuilder.Entity<OrcamentoPeca>()
                .HasOne(op => op.Orcamento)
                .WithMany()
                .HasForeignKey(op => op.OrcamentoId);

            modelBuilder.Entity<OrcamentoPeca>()
                .HasOne(op => op.Peca)
                .WithMany()
                .HasForeignKey(op => op.PecaId);

            // Relacionamento N:N - Reparo × Equipamento
            modelBuilder.Entity<ReparoEquipamento>()
                .HasKey(re => new { re.ReparoId, re.EquipamentoId });

            modelBuilder.Entity<ReparoEquipamento>()
                .HasOne(re => re.Reparo)
                .WithMany()
                .HasForeignKey(re => re.ReparoId);

            modelBuilder.Entity<ReparoEquipamento>()
                .HasOne(re => re.Equipamento)
                .WithMany()
                .HasForeignKey(re => re.EquipamentoId);

            // Relacionamento N:N - Fornecedor × Peca
            modelBuilder.Entity<FornecedorPeca>()
                .HasKey(fp => new { fp.FornecedorId, fp.PecaId });

            modelBuilder.Entity<FornecedorPeca>()
                .HasOne(fp => fp.Fornecedor)
                .WithMany()
                .HasForeignKey(fp => fp.FornecedorId);

            modelBuilder.Entity<FornecedorPeca>()
                .HasOne(fp => fp.Peca)
                .WithMany()
                .HasForeignKey(fp => fp.PecaId);

            base.OnModelCreating(modelBuilder);
        }
    }
}