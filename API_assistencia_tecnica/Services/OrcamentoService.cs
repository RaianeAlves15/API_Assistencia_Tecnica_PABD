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
            var orcamentos = await _context.Orcamentos
                .Include(o => o.Cliente)
                .Include(o => o.Equipamento)
                .ToListAsync();

            return _mapper.Map<List<OrcamentoDto>>(orcamentos);
        }

        public async Task<OrcamentoDto?> GetByIdAsync(int id)
        {
            var orcamento = await _context.Orcamentos
                .Include(o => o.Cliente)
                .Include(o => o.Equipamento)
                .FirstOrDefaultAsync(o => o.Id == id);

            return orcamento == null ? null : _mapper.Map<OrcamentoDto>(orcamento);
        }

        public async Task<OrcamentoDto> CreateAsync(OrcamentoCreateDto dto)
        {
            // Validar se cliente e equipamento existem
            var clienteExists = await _context.Clientes.AnyAsync(c => c.Id == dto.ClienteId);
            var equipamentoExists = await _context.Equipamentos.AnyAsync(e => e.Id == dto.EquipamentoId);

            if (!clienteExists)
                throw new ArgumentException("Cliente não encontrado.");
            if (!equipamentoExists)
                throw new ArgumentException("Equipamento não encontrado.");

            var orcamento = _mapper.Map<Orcamento>(dto);
            orcamento.DataCriacao = DateTime.Now;

            _context.Orcamentos.Add(orcamento);
            await _context.SaveChangesAsync();

            return await GetByIdAsync(orcamento.Id) ?? throw new Exception("Erro ao criar orçamento.");
        }

        public async Task<OrcamentoDto?> UpdateAsync(int id, OrcamentoCreateDto dto)
        {
            var orcamento = await _context.Orcamentos.FindAsync(id);
            if (orcamento == null) return null;

            // Validar se cliente e equipamento existem
            var clienteExists = await _context.Clientes.AnyAsync(c => c.Id == dto.ClienteId);
            var equipamentoExists = await _context.Equipamentos.AnyAsync(e => e.Id == dto.EquipamentoId);

            if (!clienteExists)
                throw new ArgumentException("Cliente não encontrado.");
            if (!equipamentoExists)
                throw new ArgumentException("Equipamento não encontrado.");

            _mapper.Map(dto, orcamento);
            await _context.SaveChangesAsync();

            return await GetByIdAsync(id);
        }

        // ✅ DELETE MELHORADO - Com verificação de dependências
        public async Task<bool> DeleteAsync(int id)
        {
            var orcamento = await _context.Orcamentos.FindAsync(id);
            if (orcamento == null) return false;

            // ✅ Verificar dependências
            var temOrcamentoPecas = await _context.OrcamentoPecas.AnyAsync(op => op.OrcamentoId == id);
            var temReparos = await _context.Reparos.AnyAsync(r => r.OrcamentoId == id);

            if (temOrcamentoPecas || temReparos)
            {
                var vinculos = new List<string>();
                if (temOrcamentoPecas) vinculos.Add("peças vinculadas");
                if (temReparos) vinculos.Add("reparos baseados neste orçamento");

                throw new InvalidOperationException($"Não é possível excluir o orçamento. Existem {string.Join(" e ", vinculos)}.");
            }

            _context.Orcamentos.Remove(orcamento);
            await _context.SaveChangesAsync();
            return true;
        }

        // ✅ MÉTODO - Verificar se pode deletar
        public async Task<(bool CanDelete, string Reason)> CanDeleteAsync(int id)
        {
            var orcamento = await _context.Orcamentos.FindAsync(id);
            if (orcamento == null) return (false, "Orçamento não encontrado.");

            var orcamentoPecasCount = await _context.OrcamentoPecas.CountAsync(op => op.OrcamentoId == id);
            var reparosCount = await _context.Reparos.CountAsync(r => r.OrcamentoId == id);

            if (orcamentoPecasCount > 0 || reparosCount > 0)
            {
                return (false, $"Orçamento possui {orcamentoPecasCount} peça(s) e {reparosCount} reparo(s) vinculados.");
            }

            return (true, "Pode ser excluído.");
        }

        // Métodos específicos do orçamento
        public async Task<OrcamentoDto?> AprovarOrcamentoAsync(int id)
        {
            var orcamento = await _context.Orcamentos.FindAsync(id);
            if (orcamento == null) return null;

            orcamento.Status = "Aprovado";
            orcamento.DataAprovacao = DateTime.Now;
            await _context.SaveChangesAsync();

            return await GetByIdAsync(id);
        }

        public async Task<List<OrcamentoDto>> GetByClienteIdAsync(int clienteId)
        {
            var orcamentos = await _context.Orcamentos
                .Include(o => o.Cliente)
                .Include(o => o.Equipamento)
                .Where(o => o.ClienteId == clienteId)
                .ToListAsync();

            return _mapper.Map<List<OrcamentoDto>>(orcamentos);
        }
    }
}