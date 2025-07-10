using Microsoft.EntityFrameworkCore;
using API_assistencia_tecnica.Models;
using API_assistencia_tecnica.Dtos;
using API_assistencia_tecnica.DataContexts;
using AutoMapper;

namespace API_assistencia_tecnica.Services
{
    public class EquipamentoService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public EquipamentoService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<EquipamentoDto>> GetAllAsync()
        {
            var equipamentos = await _context.Equipamentos.ToListAsync();
            return _mapper.Map<List<EquipamentoDto>>(equipamentos);
        }

        public async Task<EquipamentoDto?> GetByIdAsync(int id)
        {
            var equipamento = await _context.Equipamentos.FindAsync(id);
            return equipamento == null ? null : _mapper.Map<EquipamentoDto>(equipamento);
        }

        public async Task<EquipamentoDto> CreateAsync(EquipamentoDto dto)
        {
            var equipamento = _mapper.Map<Equipamento>(dto);
            _context.Equipamentos.Add(equipamento);
            await _context.SaveChangesAsync();
            return _mapper.Map<EquipamentoDto>(equipamento);
        }

        public async Task<EquipamentoDto?> UpdateAsync(int id, EquipamentoDto dto)
        {
            var equipamento = await _context.Equipamentos.FindAsync(id);
            if (equipamento == null) return null;

            _mapper.Map(dto, equipamento);
            await _context.SaveChangesAsync();
            return _mapper.Map<EquipamentoDto>(equipamento);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var equipamento = await _context.Equipamentos.FindAsync(id);
            if (equipamento == null) return false;

            _context.Equipamentos.Remove(equipamento);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
