using Microsoft.EntityFrameworkCore;
using API_assistencia_tecnica.Models;
using API_assistencia_tecnica.Dtos;
using API_assistencia_tecnica.DataContexts;
using AutoMapper;

namespace API_assistencia_tecnica.Services
{
    public class ReparoService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public ReparoService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<ReparoDto>> GetAllAsync()
        {
            var reparos = await _context.Reparos.ToListAsync();
            return _mapper.Map<List<ReparoDto>>(reparos);
        }

        public async Task<ReparoDto?> GetByIdAsync(int id)
        {
            var reparo = await _context.Reparos.FindAsync(id);
            return reparo == null ? null : _mapper.Map<ReparoDto>(reparo);
        }

        public async Task<ReparoDto> CreateAsync(ReparoDto dto)
        {
            var reparo = _mapper.Map<Reparo>(dto);
            _context.Reparos.Add(reparo);
            await _context.SaveChangesAsync();
            return _mapper.Map<ReparoDto>(reparo);
        }

        public async Task<ReparoDto?> UpdateAsync(int id, ReparoDto dto)
        {
            var reparo = await _context.Reparos.FindAsync(id);
            if (reparo == null) return null;

            _mapper.Map(dto, reparo);
            await _context.SaveChangesAsync();
            return _mapper.Map<ReparoDto>(reparo);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var reparo = await _context.Reparos.FindAsync(id);
            if (reparo == null) return false;

            _context.Reparos.Remove(reparo);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
