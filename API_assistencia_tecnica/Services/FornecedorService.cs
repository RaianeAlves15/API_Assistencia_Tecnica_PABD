// ========================================
// FORNECEDORSERVICE.CS - CORRIGIDO
// ========================================

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

        // ✅ DELETE MELHORADO - Com verificação de dependências
        public async Task<bool> DeleteAsync(int id)
        {
            var fornecedor = await _context.Fornecedores.FindAsync(id);
            if (fornecedor == null) return false;

            // ✅ Verificar se existem peças relacionadas
            var temFornecedorPecas = await _context.FornecedorPecas.AnyAsync(fp => fp.FornecedorId == id);

            if (temFornecedorPecas)
            {
                throw new InvalidOperationException("Não é possível excluir o fornecedor. Existem peças vinculadas a este fornecedor.");
            }

            _context.Fornecedores.Remove(fornecedor);
            await _context.SaveChangesAsync();
            return true;
        }

        // ✅ NOVO MÉTODO - Verificar se pode deletar
        public async Task<(bool CanDelete, string Reason)> CanDeleteAsync(int id)
        {
            var fornecedor = await _context.Fornecedores.FindAsync(id);
            if (fornecedor == null) return (false, "Fornecedor não encontrado.");

            var fornecedorPecasCount = await _context.FornecedorPecas.CountAsync(fp => fp.FornecedorId == id);

            if (fornecedorPecasCount > 0)
            {
                return (false, $"Fornecedor possui {fornecedorPecasCount} peça(s) vinculada(s).");
            }

            return (true, "Pode ser excluído.");
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.Fornecedores.AnyAsync(f => f.Id == id);
        }
    }
}