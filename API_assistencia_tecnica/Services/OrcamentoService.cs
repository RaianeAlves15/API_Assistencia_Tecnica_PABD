using Microsoft.EntityFrameworkCore;
using API_assistencia_tecnica.Models;
using API_assistencia_tecnica.Dtos;
using API_assistencia_tecnica.DataContexts;
using AutoMapper;

namespace API_assistencia_tecnica.Services
{
    public class OrcamentoService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public OrcamentoService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<OrcamentoDto>> GetAllAsync()
        {
            var orcamentos = await _context.Orcamentos.ToListAsync();
            return _mapper.Map<List<OrcamentoDto>>(orcamentos);
        }

        public async Task<OrcamentoDto?> GetByIdAsync(int id)
        {
            var orcamento = await _context.Orcamentos.FindAsync(id);
            return orcamento == null ? null : _mapper.Map<OrcamentoDto>(orcamento);
        }

        public async Task<OrcamentoDto> CreateAsync(OrcamentoDto dto)
        {
            var orcamento = _mapper.Map<Orcamento>(dto);
            _context.Orcamentos.Add(orcamento);
            await _context.SaveChangesAsync();
            return _mapper.Map<OrcamentoDto>(orcamento);
        }

        public async Task<OrcamentoDto?> UpdateAsync(int id, OrcamentoDto dto)
        {
            var orcamento = await _context.Orcamentos.FindAsync(id);
            if (orcamento == null) return null;

            _mapper.Map(dto, orcamento);
            await _context.SaveChangesAsync();
            return _mapper.Map<OrcamentoDto>(orcamento);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var orcamento = await _context.Orcamentos.FindAsync(id);
            if (orcamento == null) return false;

            _context.Orcamentos.Remove(orcamento);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
