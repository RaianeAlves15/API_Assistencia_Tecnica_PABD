using API_assistencia_tecnica.DataContexts;
using API_assistencia_tecnica.Models;
using Microsoft.EntityFrameworkCore;

namespace API_assistencia_tecnica.Services
{
    public class ReparoService
    {
        private readonly AppDbContext _context;

        public ReparoService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Reparo>> GetAllAsync()
        {
            return await _context.Reparos.ToListAsync();
        }

        public async Task<Reparo?> GetByIdAsync(int id)
        {
            return await _context.Reparos.FirstOrDefaultAsync(r => r.IdLancamentoReparo == id);
        }

        public async Task<Reparo> CreateAsync(Reparo reparo)
        {
            _context.Reparos.Add(reparo);
            await _context.SaveChangesAsync();
            return reparo;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var item = await _context.Reparos.FindAsync(id);
            if (item == null) return false;

            _context.Reparos.Remove(item);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.Reparos.AnyAsync(r => r.IdLancamentoReparo == id);
        }
    }
}