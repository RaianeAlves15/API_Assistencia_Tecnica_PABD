using Microsoft.EntityFrameworkCore;
using API_assistencia_tecnica.DataContexts;
using API_assistencia_tecnica.Dtos;
using API_assistencia_tecnica.Models;
using AutoMapper;

namespace API_assistencia_tecnica.Services
{
    public class OrcamentoPecaService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public OrcamentoPecaService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<OrcamentoPecaDto>> GetAllAsync()
        {
            var list = await _context.OrcamentoPecas
                .Include(op => op.Orcamento)
                .Include(op => op.Peca)
                .ToListAsync();

            return _mapper.Map<List<OrcamentoPecaDto>>(list);
        }

        public async Task<OrcamentoPecaDto> CreateAsync(OrcamentoPecaDto dto)
        {
            var entity = _mapper.Map<OrcamentoPeca>(dto);
            _context.OrcamentoPecas.Add(entity);
            await _context.SaveChangesAsync();
            return _mapper.Map<OrcamentoPecaDto>(entity);
        }

        public async Task<OrcamentoPecaDto?> GetByIdAsync(int id)
        {
            var entity = await _context.OrcamentoPecas
                .Include(op => op.Orcamento)
                .Include(op => op.Peca)
                .FirstOrDefaultAsync(op => op.Id == id);

            return entity == null ? null : _mapper.Map<OrcamentoPecaDto>(entity);
        }

        public async Task<OrcamentoPecaDto?> UpdateAsync(int id, OrcamentoPecaDto dto)
        {
            var entity = await _context.OrcamentoPecas.FindAsync(id);
            if (entity == null) return null;

            _mapper.Map(dto, entity);
            await _context.SaveChangesAsync();

            return _mapper.Map<OrcamentoPecaDto>(entity);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _context.OrcamentoPecas.FindAsync(id);
            if (entity == null) return false;

            _context.OrcamentoPecas.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}

