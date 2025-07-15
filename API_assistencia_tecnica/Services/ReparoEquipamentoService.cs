using Microsoft.EntityFrameworkCore;
using API_assistencia_tecnica.DataContexts;
using API_assistencia_tecnica.Dtos;
using API_assistencia_tecnica.Models;
using AutoMapper;

namespace API_assistencia_tecnica.Services
{
    public class ReparoEquipamentoService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public ReparoEquipamentoService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<ReparoEquipamentoDto>> GetAllAsync()
        {
            var list = await _context.ReparoEquipamentos
                .Include(re => re.Reparo)
                .Include(re => re.Equipamento)
                .ToListAsync();
            return _mapper.Map<List<ReparoEquipamentoDto>>(list);
        }

        public async Task<ReparoEquipamentoDto?> GetByIdAsync(int reparoId, int equipamentoId)
        {
            var entity = await _context.ReparoEquipamentos
                .Include(re => re.Reparo)
                .Include(re => re.Equipamento)
                .FirstOrDefaultAsync(x => x.ReparoId == reparoId && x.EquipamentoId == equipamentoId);
            return entity == null ? null : _mapper.Map<ReparoEquipamentoDto>(entity);
        }

        public async Task<ReparoEquipamentoDto> CreateAsync(ReparoEquipamentoDto dto)
        {
            // ✅ Validar se reparo e equipamento existem
            var reparoExists = await _context.Reparos.AnyAsync(r => r.Id == dto.ReparoId);
            var equipamentoExists = await _context.Equipamentos.AnyAsync(e => e.Id == dto.EquipamentoId);

            if (!reparoExists)
                throw new ArgumentException("Reparo não encontrado.");
            if (!equipamentoExists)
                throw new ArgumentException("Equipamento não encontrado.");

            var entity = _mapper.Map<ReparoEquipamento>(dto);
            _context.ReparoEquipamentos.Add(entity);
            await _context.SaveChangesAsync();

            return await GetByIdAsync(entity.ReparoId, entity.EquipamentoId) ?? throw new Exception("Erro ao criar relação.");
        }

        public async Task<ReparoEquipamentoDto?> UpdateAsync(int reparoId, int equipamentoId, ReparoEquipamentoDto dto)
        {
            var entity = await _context.ReparoEquipamentos
                .FirstOrDefaultAsync(x => x.ReparoId == reparoId && x.EquipamentoId == equipamentoId);

            if (entity == null) return null;

            _mapper.Map(dto, entity);
            await _context.SaveChangesAsync();

            return await GetByIdAsync(reparoId, equipamentoId);
        }

        // ✅ DELETE sem dependências (é uma tabela de relacionamento)
        public async Task<bool> DeleteAsync(int reparoId, int equipamentoId)
        {
            var entity = await _context.ReparoEquipamentos
                .FirstOrDefaultAsync(x => x.ReparoId == reparoId && x.EquipamentoId == equipamentoId);

            if (entity == null) return false;

            _context.ReparoEquipamentos.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
