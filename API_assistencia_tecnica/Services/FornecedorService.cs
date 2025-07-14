using Microsoft.EntityFrameworkCore;
using API_assistencia_tecnica.Models;
using API_assistencia_tecnica.Dtos;
using API_assistencia_tecnica.DataContexts;
using AutoMapper;

namespace API_assistencia_tecnica.Services
{
    public class FornecedorService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public FornecedorService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<FornecedorDto>> GetAllAsync()
        {
            var fornecedores = await _context.Fornecedores.ToListAsync();
            return _mapper.Map<List<FornecedorDto>>(fornecedores);
        }

        public async Task<FornecedorDto?> GetByIdAsync(int id)
        {
            var fornecedor = await _context.Fornecedores.FindAsync(id);
            return fornecedor == null ? null : _mapper.Map<FornecedorDto>(fornecedor);
        }

        public async Task<FornecedorDto> CreateAsync(FornecedorDto dto)
        {
            var fornecedor = _mapper.Map<Fornecedor>(dto);
            _context.Fornecedores.Add(fornecedor);
            await _context.SaveChangesAsync();
            return _mapper.Map<FornecedorDto>(fornecedor);
        }

        public async Task<FornecedorDto?> UpdateAsync(int id, FornecedorDto dto)
        {
            var fornecedor = await _context.Fornecedores.FindAsync(id);
            if (fornecedor == null) return null;

            _mapper.Map(dto, fornecedor);
            await _context.SaveChangesAsync();
            return _mapper.Map<FornecedorDto>(fornecedor);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var fornecedor = await _context.Fornecedores.FindAsync(id);
            if (fornecedor == null) return false;

            _context.Fornecedores.Remove(fornecedor);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.Fornecedores.AnyAsync(f => f.Id == id);
        }
    }
}