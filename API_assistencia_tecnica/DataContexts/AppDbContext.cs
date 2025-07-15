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
        public DbSet<ReparoPeca> ReparoPecas { get; set; } // NOVO
        public DbSet<ReparoEquipamento> ReparoEquipamentos { get; set; }
        public DbSet<FornecedorPeca> FornecedorPecas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Relacionamento Orcamento -> Cliente
            modelBuilder.Entity<Orcamento>()
                .HasOne(o => o.Cliente)
                .WithMany()
                .HasForeignKey(o => o.ClienteId)
                .OnDelete(DeleteBehavior.Restrict); // Evita exclusão em cascata

            // Relacionamento Orcamento -> Equipamento
            modelBuilder.Entity<Orcamento>()
                .HasOne(o => o.Equipamento)
                .WithMany()
                .HasForeignKey(o => o.EquipamentoId)
                .OnDelete(DeleteBehavior.Restrict);

            // Relacionamento Reparo -> Cliente
            modelBuilder.Entity<Reparo>()
                .HasOne(r => r.Cliente)
                .WithMany()
                .HasForeignKey(r => r.ClienteId)
                .OnDelete(DeleteBehavior.Restrict);

            // Relacionamento Reparo -> Equipamento
            modelBuilder.Entity<Reparo>()
                .HasOne(r => r.Equipamento)
                .WithMany()
                .HasForeignKey(r => r.EquipamentoId)
                .OnDelete(DeleteBehavior.Restrict);

            // Relacionamento Reparo -> Orcamento (opcional)
            modelBuilder.Entity<Reparo>()
                .HasOne(r => r.Orcamento)
                .WithMany()
                .HasForeignKey(r => r.OrcamentoId)
                .OnDelete(DeleteBehavior.SetNull);

            // Relacionamento N:N - Orcamento × Peca
            modelBuilder.Entity<OrcamentoPeca>()
                .HasKey(op => new { op.OrcamentoId, op.PecaId });

            modelBuilder.Entity<OrcamentoPeca>()
                .HasOne(op => op.Orcamento)
                .WithMany()
                .HasForeignKey(op => op.OrcamentoId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<OrcamentoPeca>()
                .HasOne(op => op.Peca)
                .WithMany()
                .HasForeignKey(op => op.PecaId)
                .OnDelete(DeleteBehavior.Restrict);

            // Relacionamento N:N - Reparo × Peca (NOVO)
            modelBuilder.Entity<ReparoPeca>()
                .HasKey(rp => new { rp.ReparoId, rp.PecaId });

            modelBuilder.Entity<ReparoPeca>()
                .HasOne(rp => rp.Reparo)
                .WithMany()
                .HasForeignKey(rp => rp.ReparoId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ReparoPeca>()
                .HasOne(rp => rp.Peca)
                .WithMany()
                .HasForeignKey(rp => rp.PecaId)
                .OnDelete(DeleteBehavior.Restrict);

            // Relacionamento N:N - Reparo × Equipamento
            modelBuilder.Entity<ReparoEquipamento>()
                .HasKey(re => new { re.ReparoId, re.EquipamentoId });

            modelBuilder.Entity<ReparoEquipamento>()
                .HasOne(re => re.Reparo)
                .WithMany()
                .HasForeignKey(re => re.ReparoId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ReparoEquipamento>()
                .HasOne(re => re.Equipamento)
                .WithMany()
                .HasForeignKey(re => re.EquipamentoId)
                .OnDelete(DeleteBehavior.Restrict);

            // Relacionamento N:N - Fornecedor × Peca
            modelBuilder.Entity<FornecedorPeca>()
                .HasKey(fp => new { fp.FornecedorId, fp.PecaId });

            modelBuilder.Entity<FornecedorPeca>()
                .HasOne(fp => fp.Fornecedor)
                .WithMany()
                .HasForeignKey(fp => fp.FornecedorId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<FornecedorPeca>()
                .HasOne(fp => fp.Peca)
                .WithMany()
                .HasForeignKey(fp => fp.PecaId)
                .OnDelete(DeleteBehavior.Restrict);

            base.OnModelCreating(modelBuilder);
        }
    }
}