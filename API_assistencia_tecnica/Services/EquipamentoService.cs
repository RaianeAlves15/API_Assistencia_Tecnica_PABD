using API_assistencia_tecnica.DataContexts;
using API_assistencia_tecnica.Models;
using Microsoft.EntityFrameworkCore;

namespace API_assistencia_tecnica.Services
{
    public class EquipamentoService
    {
        private readonly AppDbContext _context;

        public EquipamentoService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Equipamento>> GetAllAsync()
        {
            return await _context.Equipamentos.ToListAsync();
        }

        public async Task<Equipamento?> GetByIdAsync(int id)
        {
            return await _context.Equipamentos.FirstOrDefaultAsync(e => e.IdEquipamento == id);
        }

        public async Task<Equipamento> CreateAsync(Equipamento equipamento)
        {
            _context.Equipamentos.Add(equipamento);
            await _context.SaveChangesAsync();
            return equipamento;
        }

        public async Task<Equipamento?> UpdateAsync(int id, Equipamento equipamento)
        {
            var existing = await _context.Equipamentos.FindAsync(id);
            if (existing == null) return null;

            existing.NomeEquipamento = equipamento.NomeEquipamento;
            existing.Fabricante = equipamento.Fabricante;
            existing.Modelo = equipamento.Modelo;
            existing.NumeroDeSerie = equipamento.NumeroDeSerie;
            existing.CodigoDeFabricacao = equipamento.CodigoDeFabricacao;
            existing.AnoDeFabricacao = equipamento.AnoDeFabricacao;
            existing.Voltagem = equipamento.Voltagem;
            existing.Amperagem = equipamento.Amperagem;

            _context.Equipamentos.Update(existing);
            await _context.SaveChangesAsync();
            return existing;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var item = await _context.Equipamentos.FindAsync(id);
            if (item == null) return false;

            _context.Equipamentos.Remove(item);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.Equipamentos.AnyAsync(e => e.IdEquipamento == id);
        }
    }
}