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

        public async Task<FornecedorPecaDto?> GetByIdAsync(int fornecedorId, int pecaId)
        {
            var entity = await _context.FornecedorPecas
                .Include(fp => fp.Fornecedor)
                .Include(fp => fp.Peca)
                .FirstOrDefaultAsync(fp => fp.FornecedorId == fornecedorId && fp.PecaId == pecaId);

            return entity == null ? null : _mapper.Map<FornecedorPecaDto>(entity);
        }

        public async Task<FornecedorPecaDto> CreateAsync(FornecedorPecaDto dto)
        {
            // ✅ Validar se fornecedor e peça existem
            var fornecedorExists = await _context.Fornecedores.AnyAsync(f => f.Id == dto.FornecedorId);
            var pecaExists = await _context.Pecas.AnyAsync(p => p.Id == dto.PecaId);

            if (!fornecedorExists)
                throw new ArgumentException("Fornecedor não encontrado.");
            if (!pecaExists)
                throw new ArgumentException("Peça não encontrada.");

            var entity = _mapper.Map<FornecedorPeca>(dto);
            _context.FornecedorPecas.Add(entity);
            await _context.SaveChangesAsync();

            return await GetByIdAsync(entity.FornecedorId, entity.PecaId) ?? throw new Exception("Erro ao criar relação.");
        }

        public async Task<FornecedorPecaDto?> UpdateAsync(int fornecedorId, int pecaId, FornecedorPecaDto dto)
        {
            var entity = await _context.FornecedorPecas
                .FirstOrDefaultAsync(fp => fp.FornecedorId == fornecedorId && fp.PecaId == pecaId);

            if (entity == null) return null;

            // Mapear apenas os campos que podem ser atualizados
            entity.PrecoUnitario = dto.PrecoUnitario;
            entity.DataUltimaCompra = dto.DataUltimaCompra;

            await _context.SaveChangesAsync();
            return await GetByIdAsync(fornecedorId, pecaId);
        }

        // ✅ DELETE sem dependências (é uma tabela de relacionamento)
        public async Task<bool> DeleteAsync(int fornecedorId, int pecaId)
        {
            var entity = await _context.FornecedorPecas
                .FirstOrDefaultAsync(fp => fp.FornecedorId == fornecedorId && fp.PecaId == pecaId);

            if (entity == null) return false;

            _context.FornecedorPecas.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<FornecedorPecaDto>> GetPecasByFornecedorIdAsync(int fornecedorId)
        {
            var list = await _context.FornecedorPecas
                .Include(fp => fp.Fornecedor)
                .Include(fp => fp.Peca)
                .Where(fp => fp.FornecedorId == fornecedorId)
                .ToListAsync();

            return _mapper.Map<List<FornecedorPecaDto>>(list);
        }

        public async Task<List<FornecedorPecaDto>> GetFornecedoresByPecaIdAsync(int pecaId)
        {
            var list = await _context.FornecedorPecas
                .Include(fp => fp.Fornecedor)
                .Include(fp => fp.Peca)
                .Where(fp => fp.PecaId == pecaId)
                .ToListAsync();

            return _mapper.Map<List<FornecedorPecaDto>>(list);
        }

        public async Task<bool> ExistsAsync(int fornecedorId, int pecaId)
        {
            return await _context.FornecedorPecas
                .AnyAsync(fp => fp.FornecedorId == fornecedorId && fp.PecaId == pecaId);
        }
    }
}
