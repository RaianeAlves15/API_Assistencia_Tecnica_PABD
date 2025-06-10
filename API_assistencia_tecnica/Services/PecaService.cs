using Microsoft.EntityFrameworkCore;
using API_assistencia_tecnica.Models;
using API_assistencia_tecnica.Dtos;
using API_assistencia_tecnica.DataContexts;

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

        public async Task<Peca> CreateAsync(PecaDto dto)
        {
            var peca = new Peca
            {
                NomePeca = dto.NomePeca,
                Fabricante = dto.Fabricante,
                LocalDeFabricacao = dto.LocalDeFabricacao,
                PesoKg = dto.PesoKg,
                Quantidade = dto.Quantidade,
                NumeroDeSerie = dto.NumeroDeSerie,
                CodigoDeProducao = dto.CodigoDeProducao,
                Preco = dto.Preco,
                Observacao = dto.Observacao
            };

            _context.Pecas.Add(peca);
            await _context.SaveChangesAsync();
            return peca;
        }

        public async Task<Peca?> UpdateAsync(int id, PecaDto dto)
        {
            var existing = await _context.Pecas.FindAsync(id);
            if (existing == null) return null;

            existing.NomePeca = dto.NomePeca;
            existing.Fabricante = dto.Fabricante;
            existing.LocalDeFabricacao = dto.LocalDeFabricacao;
            existing.PesoKg = dto.PesoKg;
            existing.Quantidade = dto.Quantidade;
            existing.NumeroDeSerie = dto.NumeroDeSerie;
            existing.CodigoDeProducao = dto.CodigoDeProducao;
            existing.Preco = dto.Preco;
            existing.Observacao = dto.Observacao;

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
