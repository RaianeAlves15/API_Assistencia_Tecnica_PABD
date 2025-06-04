using API_assistencia_tecnica.DataContexts;
using API_assistencia_tecnica.Models;
using Microsoft.EntityFrameworkCore;

namespace API_assistencia_tecnica.Services
{
    public class OrcamentoService
    {
        private readonly AppDbContext _context;

        public OrcamentoService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Orcamento>> GetAllAsync()
        {
            return await _context.Orcamentos.ToListAsync();
        }

        public async Task<Orcamento?> GetByIdAsync(int id)
        {
            return await _context.Orcamentos.FirstOrDefaultAsync(o => o.IdOrcamento == id);
        }

        public async Task<Orcamento> CreateAsync(Orcamento orcamento)
        {
            _context.Orcamentos.Add(orcamento);
            await _context.SaveChangesAsync();
            return orcamento;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var item = await _context.Orcamentos.FindAsync(id);
            if (item == null) return false;

            _context.Orcamentos.Remove(item);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.Orcamentos.AnyAsync(o => o.IdOrcamento == id);
        }
    }
}