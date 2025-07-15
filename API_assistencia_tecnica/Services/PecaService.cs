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

        // ✅ DELETE MELHORADO - Com verificação de dependências
        public async Task<bool> DeleteAsync(int id)
        {
            var peca = await _context.Pecas.FindAsync(id);
            if (peca == null) return false;

            // ✅ Verificar se existem relacionamentos
            var temOrcamentoPecas = await _context.OrcamentoPecas.AnyAsync(op => op.PecaId == id);
            var temReparoPecas = await _context.ReparoPecas.AnyAsync(rp => rp.PecaId == id);
            var temFornecedorPecas = await _context.FornecedorPecas.AnyAsync(fp => fp.PecaId == id);

            if (temOrcamentoPecas || temReparoPecas || temFornecedorPecas)
            {
                var vinculos = new List<string>();
                if (temOrcamentoPecas) vinculos.Add("orçamentos");
                if (temReparoPecas) vinculos.Add("reparos");
                if (temFornecedorPecas) vinculos.Add("fornecedores");

                throw new InvalidOperationException($"Não é possível excluir a peça. Está vinculada a {string.Join(", ", vinculos)}.");
            }

            _context.Pecas.Remove(peca);
            await _context.SaveChangesAsync();
            return true;
        }

        // ✅ NOVO MÉTODO - Verificar se pode deletar
        public async Task<(bool CanDelete, string Reason)> CanDeleteAsync(int id)
        {
            var peca = await _context.Pecas.FindAsync(id);
            if (peca == null) return (false, "Peça não encontrada.");

            var orcamentoPecasCount = await _context.OrcamentoPecas.CountAsync(op => op.PecaId == id);
            var reparoPecasCount = await _context.ReparoPecas.CountAsync(rp => rp.PecaId == id);
            var fornecedorPecasCount = await _context.FornecedorPecas.CountAsync(fp => fp.PecaId == id);

            var totalVinculos = orcamentoPecasCount + reparoPecasCount + fornecedorPecasCount;

            if (totalVinculos > 0)
            {
                return (false, $"Peça possui {orcamentoPecasCount} orçamento(s), {reparoPecasCount} reparo(s) e {fornecedorPecasCount} fornecedor(es) vinculados.");
            }

            return (true, "Pode ser excluída.");
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.Pecas.AnyAsync(p => p.Id == id);
        }
    }
}