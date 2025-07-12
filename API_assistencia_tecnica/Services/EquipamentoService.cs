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
            var lista = await _context.Equipamentos.ToListAsync();
            return _mapper.Map<List<EquipamentoDto>>(lista);
        }

        public async Task<EquipamentoDto?> GetByIdAsync(int id)
        {
            var item = await _context.Equipamentos.FindAsync(id);
            return item == null ? null : _mapper.Map<EquipamentoDto>(item);
        }

        public async Task<EquipamentoDto> CreateAsync(EquipamentoDto dto)
        {
            var entidade = _mapper.Map<Equipamento>(dto);
            _context.Equipamentos.Add(entidade);
            await _context.SaveChangesAsync();
            return _mapper.Map<EquipamentoDto>(entidade);
        }

        public async Task<EquipamentoDto?> UpdateAsync(int id, EquipamentoDto dto)
        {
            var entidade = await _context.Equipamentos.FindAsync(id);
            if (entidade == null) return null;

            _mapper.Map(dto, entidade);
            await _context.SaveChangesAsync();
            return _mapper.Map<EquipamentoDto>(entidade);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entidade = await _context.Equipamentos.FindAsync(id);
            if (entidade == null) return false;

            _context.Equipamentos.Remove(entidade);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}