using Microsoft.EntityFrameworkCore;
using API_assistencia_tecnica.Models;
using API_assistencia_tecnica.Dtos;
using API_assistencia_tecnica.DataContexts;

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

        public async Task<Equipamento> CreateAsync(EquipamentoDto dto)
        {
            var equipamento = new Equipamento
            {
                NomeEquipamento = dto.NomeEquipamento,
                Fabricante = dto.Fabricante,
                Modelo = dto.Modelo,
                NumeroDeSerie = dto.NumeroDeSerie,
                CodigoDeFabricacao = dto.CodigoDeFabricacao,
                AnoDeFabricacao = dto.AnoDeFabricacao,
                Voltagem = dto.Voltagem,
                Amperagem = dto.Amperagem
            };

            _context.Equipamentos.Add(equipamento);
            await _context.SaveChangesAsync();
            return equipamento;
        }

        public async Task<Equipamento?> UpdateAsync(int id, EquipamentoDto dto)
        {
            var existing = await _context.Equipamentos.FindAsync(id);
            if (existing == null) return null;

            existing.NomeEquipamento = dto.NomeEquipamento;
            existing.Fabricante = dto.Fabricante;
            existing.Modelo = dto.Modelo;
            existing.NumeroDeSerie = dto.NumeroDeSerie;
            existing.CodigoDeFabricacao = dto.CodigoDeFabricacao;
            existing.AnoDeFabricacao = dto.AnoDeFabricacao;
            existing.Voltagem = dto.Voltagem;
            existing.Amperagem = dto.Amperagem;

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