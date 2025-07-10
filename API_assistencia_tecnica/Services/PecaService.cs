using Microsoft.EntityFrameworkCore;
using API_assistencia_tecnica.Models;
using API_assistencia_tecnica.Dtos;
using API_assistencia_tecnica.DataContexts;
using AutoMapper;

namespace API_assistencia_tecnica.Services
{
    public class PecaService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public PecaService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<PecaDto>> GetAllAsync()
        {
            var pecas = await _context.Pecas.ToListAsync();
            return _mapper.Map<List<PecaDto>>(pecas);
        }

        public async Task<PecaDto?> GetByIdAsync(int id)
        {
            var peca = await _context.Pecas.FindAsync(id);
            return peca == null ? null : _mapper.Map<PecaDto>(peca);
        }

        public async Task<PecaDto> CreateAsync(PecaDto dto)
        {
            var peca = _mapper.Map<Peca>(dto);
            _context.Pecas.Add(peca);
            await _context.SaveChangesAsync();
            return _mapper.Map<PecaDto>(peca);
        }

        public async Task<PecaDto?> UpdateAsync(int id, PecaDto dto)
        {
            var peca = await _context.Pecas.FindAsync(id);
            if (peca == null) return null;

            _mapper.Map(dto, peca);
            await _context.SaveChangesAsync();
            return _mapper.Map<PecaDto>(peca);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var peca = await _context.Pecas.FindAsync(id);
            if (peca == null) return false;

            _context.Pecas.Remove(peca);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
