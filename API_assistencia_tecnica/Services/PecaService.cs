using API_assistencia_tecnica.DataContexts;
using API_assistencia_tecnica.Models;
using Microsoft.EntityFrameworkCore;

namespace API_assistencia_tecnica.Services
{
    public class PecaService
    {
        private readonly AppDbContext _context;

        public PecaService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Peca>> GetAllAsync()
        {
            return await _context.Pecas.ToListAsync();
        }

        public async Task<Peca?> GetByIdAsync(int id)
        {
            return await _context.Pecas.FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<Peca> CreateAsync(Peca peca)
        {
            _context.Pecas.Add(peca);
            await _context.SaveChangesAsync();
            return peca;
        }

        public async Task<Peca?> UpdateAsync(int id, Peca peca)
        {
            var existing = await _context.Pecas.FindAsync(id);
            if (existing == null) return null;

            existing.NomePeca = peca.NomePeca;
            existing.Fabricante = peca.Fabricante;
            existing.LocalDeFabricacao = peca.LocalDeFabricacao;
            existing.PesoKg = peca.PesoKg;
            existing.Quantidade = peca.Quantidade;
            existing.NumeroDeSerie = peca.NumeroDeSerie;
            existing.CodigoDeProducao = peca.CodigoDeProducao;
            existing.Preco = peca.Preco;
            existing.Observacao = peca.Observacao;

            _context.Pecas.Update(existing);
            await _context.SaveChangesAsync();
            return existing;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var item = await _context.Pecas.FindAsync(id);
            if (item == null) return false;

            _context.Pecas.Remove(item);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.Pecas.AnyAsync(p => p.Id == id);
        }
    }
}