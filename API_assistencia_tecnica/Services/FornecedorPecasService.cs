using Microsoft.EntityFrameworkCore;
using API_assistencia_tecnica.DataContexts;
using API_assistencia_tecnica.Dtos;
using API_assistencia_tecnica.Models;
using AutoMapper;

namespace API_assistencia_tecnica.Services
{
    public class FornecedorPecasService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public FornecedorPecasService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<FornecedorPecaDto>> GetAllAsync()
        {
            var list = await _context.FornecedorPecas
                .Include(fp => fp.Fornecedor)
                .Include(fp => fp.Peca)
                .ToListAsync();

            return _mapper.Map<List<FornecedorPecaDto>>(list);
        }

        public async Task<FornecedorPecaDto> CreateAsync(FornecedorPecaDto dto)
        {
            var entity = _mapper.Map<FornecedorPeca>(dto);
            _context.FornecedorPecas.Add(entity);
            await _context.SaveChangesAsync();
            return _mapper.Map<FornecedorPecaDto>(entity);
        }

        public async Task<FornecedorPecaDto?> GetByIdAsync(int id)
        {
            var entity = await _context.FornecedorPecas
                .Include(fp => fp.Fornecedor)
                .Include(fp => fp.Peca)
                .FirstOrDefaultAsync(fp => fp.Id == id);

            return entity == null ? null : _mapper.Map<FornecedorPecaDto>(entity);
        }

        public async Task<FornecedorPecaDto?> UpdateAsync(int id, FornecedorPecaDto dto)
        {
            var entity = await _context.FornecedorPecas.FindAsync(id);
            if (entity == null) return null;

            _mapper.Map(dto, entity);
            await _context.SaveChangesAsync();

            return _mapper.Map<FornecedorPecaDto>(entity);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _context.FornecedorPecas.FindAsync(id);
            if (entity == null) return false;

            _context.FornecedorPecas.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
