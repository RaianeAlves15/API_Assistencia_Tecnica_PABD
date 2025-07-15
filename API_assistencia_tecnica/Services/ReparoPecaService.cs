using Microsoft.EntityFrameworkCore;
using API_assistencia_tecnica.DataContexts;
using API_assistencia_tecnica.Dtos;
using API_assistencia_tecnica.Models;
using AutoMapper;

namespace API_assistencia_tecnica.Services
{
    public class ReparoPecaService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public ReparoPecaService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<ReparoPecaDto>> GetAllAsync()
        {
            var list = await _context.ReparoPecas
                .Include(rp => rp.Reparo)
                .Include(rp => rp.Peca)
                .ToListAsync();

            return _mapper.Map<List<ReparoPecaDto>>(list);
        }

        public async Task<ReparoPecaDto?> GetByIdAsync(int reparoId, int pecaId)
        {
            var entity = await _context.ReparoPecas
                .Include(rp => rp.Reparo)
                .Include(rp => rp.Peca)
                .FirstOrDefaultAsync(rp => rp.ReparoId == reparoId && rp.PecaId == pecaId);

            return entity == null ? null : _mapper.Map<ReparoPecaDto>(entity);
        }

        public async Task<ReparoPecaDto> CreateAsync(ReparoPecaDto dto)
        {
            // ✅ Validar se reparo e peça existem
            var reparoExists = await _context.Reparos.AnyAsync(r => r.Id == dto.ReparoId);
            var pecaExists = await _context.Pecas.AnyAsync(p => p.Id == dto.PecaId);

            if (!reparoExists)
                throw new ArgumentException("Reparo não encontrado.");
            if (!pecaExists)
                throw new ArgumentException("Peça não encontrada.");

            var entity = _mapper.Map<ReparoPeca>(dto);
            _context.ReparoPecas.Add(entity);
            await _context.SaveChangesAsync();

            return await GetByIdAsync(entity.ReparoId, entity.PecaId) ?? throw new Exception("Erro ao criar relação.");
        }

        public async Task<ReparoPecaDto?> UpdateAsync(int reparoId, int pecaId, ReparoPecaDto dto)
        {
            var entity = await _context.ReparoPecas
                .FirstOrDefaultAsync(rp => rp.ReparoId == reparoId && rp.PecaId == pecaId);

            if (entity == null) return null;

            entity.Quantidade = dto.Quantidade;
            entity.PrecoUnitario = dto.PrecoUnitario;
            entity.PecaUtilizada = dto.PecaUtilizada;

            await _context.SaveChangesAsync();
            return await GetByIdAsync(reparoId, pecaId);
        }

        // ✅ DELETE sem dependências (é uma tabela de relacionamento)
        public async Task<bool> DeleteAsync(int reparoId, int pecaId)
        {
            var entity = await _context.ReparoPecas
                .FirstOrDefaultAsync(rp => rp.ReparoId == reparoId && rp.PecaId == pecaId);

            if (entity == null) return false;

            _context.ReparoPecas.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<ReparoPecaDto>> GetPecasByReparoIdAsync(int reparoId)
        {
            var list = await _context.ReparoPecas
                .Include(rp => rp.Reparo)
                .Include(rp => rp.Peca)
                .Where(rp => rp.ReparoId == reparoId)
                .ToListAsync();

            return _mapper.Map<List<ReparoPecaDto>>(list);
        }

        public async Task<bool> ExistsAsync(int reparoId, int pecaId)
        {
            return await _context.ReparoPecas
                .AnyAsync(rp => rp.ReparoId == reparoId && rp.PecaId == pecaId);
        }

        public async Task<ReparoPecaDto?> MarcarPecaUtilizadaAsync(int reparoId, int pecaId, bool utilizada = true)
        {
            var entity = await _context.ReparoPecas
                .FirstOrDefaultAsync(rp => rp.ReparoId == reparoId && rp.PecaId == pecaId);

            if (entity == null) return null;

            entity.PecaUtilizada = utilizada;
            await _context.SaveChangesAsync();

            return await GetByIdAsync(reparoId, pecaId);
        }
    }
}
