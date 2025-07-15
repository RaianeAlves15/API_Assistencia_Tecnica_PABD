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
            var reparos = await _context.Reparos
                .Include(r => r.Cliente)
                .Include(r => r.Equipamento)
                .Include(r => r.Orcamento)
                .ToListAsync();

            return _mapper.Map<List<ReparoDto>>(reparos);
        }

        public async Task<ReparoDto?> GetByIdAsync(int id)
        {
            var reparo = await _context.Reparos
                .Include(r => r.Cliente)
                .Include(r => r.Equipamento)
                .Include(r => r.Orcamento)
                .FirstOrDefaultAsync(r => r.Id == id);

            return reparo == null ? null : _mapper.Map<ReparoDto>(reparo);
        }

        public async Task<ReparoDto> CreateAsync(ReparoCreateDto dto)
        {
            // Validações
            var clienteExists = await _context.Clientes.AnyAsync(c => c.Id == dto.ClienteId);
            var equipamentoExists = await _context.Equipamentos.AnyAsync(e => e.Id == dto.EquipamentoId);

            if (!clienteExists)
                throw new ArgumentException("Cliente não encontrado.");
            if (!equipamentoExists)
                throw new ArgumentException("Equipamento não encontrado.");

            // Se foi informado um orçamento, validar
            if (dto.OrcamentoId.HasValue)
            {
                var orcamento = await _context.Orcamentos.FindAsync(dto.OrcamentoId.Value);
                if (orcamento == null)
                    throw new ArgumentException("Orçamento não encontrado.");
                if (orcamento.Status != "Aprovado")
                    throw new ArgumentException("Orçamento deve estar aprovado para iniciar reparo.");
            }

            var reparo = _mapper.Map<Reparo>(dto);
            reparo.DataInicio = DateTime.Now;

            _context.Reparos.Add(reparo);
            await _context.SaveChangesAsync();

            return await GetByIdAsync(reparo.Id) ?? throw new Exception("Erro ao criar reparo.");
        }

        public async Task<ReparoDto?> UpdateAsync(int id, ReparoCreateDto dto)
        {
            var reparo = await _context.Reparos.FindAsync(id);
            if (reparo == null) return null;

            // Validações similares ao Create
            var clienteExists = await _context.Clientes.AnyAsync(c => c.Id == dto.ClienteId);
            var equipamentoExists = await _context.Equipamentos.AnyAsync(e => e.Id == dto.EquipamentoId);

            if (!clienteExists)
                throw new ArgumentException("Cliente não encontrado.");
            if (!equipamentoExists)
                throw new ArgumentException("Equipamento não encontrado.");

            _mapper.Map(dto, reparo);
            await _context.SaveChangesAsync();

            return await GetByIdAsync(id);
        }

        // ✅ DELETE MELHORADO - Com verificação de dependências
        public async Task<bool> DeleteAsync(int id)
        {
            var reparo = await _context.Reparos.FindAsync(id);
            if (reparo == null) return false;

            // ✅ Verificar dependências
            var temReparoPecas = await _context.ReparoPecas.AnyAsync(rp => rp.ReparoId == id);
            var temReparoEquipamentos = await _context.ReparoEquipamentos.AnyAsync(re => re.ReparoId == id);

            if (temReparoPecas || temReparoEquipamentos)
            {
                var vinculos = new List<string>();
                if (temReparoPecas) vinculos.Add("peças vinculadas");
                if (temReparoEquipamentos) vinculos.Add("equipamentos vinculados");

                throw new InvalidOperationException($"Não é possível excluir o reparo. Existem {string.Join(" e ", vinculos)}.");
            }

            _context.Reparos.Remove(reparo);
            await _context.SaveChangesAsync();
            return true;
        }

        // ✅ MÉTODO - Verificar se pode deletar
        public async Task<(bool CanDelete, string Reason)> CanDeleteAsync(int id)
        {
            var reparo = await _context.Reparos.FindAsync(id);
            if (reparo == null) return (false, "Reparo não encontrado.");

            var reparoPecasCount = await _context.ReparoPecas.CountAsync(rp => rp.ReparoId == id);
            var reparoEquipamentosCount = await _context.ReparoEquipamentos.CountAsync(re => re.ReparoId == id);

            if (reparoPecasCount > 0 || reparoEquipamentosCount > 0)
            {
                return (false, $"Reparo possui {reparoPecasCount} peça(s) e {reparoEquipamentosCount} equipamento(s) vinculados.");
            }

            return (true, "Pode ser excluído.");
        }

        // Métodos específicos do reparo
        public async Task<ReparoDto?> ConcluirReparoAsync(int id)
        {
            var reparo = await _context.Reparos.FindAsync(id);
            if (reparo == null) return null;

            reparo.Status = "Concluido";
            reparo.DataConclusao = DateTime.Now;
            await _context.SaveChangesAsync();

            return await GetByIdAsync(id);
        }

        public async Task<ReparoDto?> EntregarReparoAsync(int id)
        {
            var reparo = await _context.Reparos.FindAsync(id);
            if (reparo == null) return null;

            reparo.Status = "Entregue";
            reparo.DataEntrega = DateTime.Now;
            await _context.SaveChangesAsync();

            return await GetByIdAsync(id);
        }

        public async Task<List<ReparoDto>> GetByClienteIdAsync(int clienteId)
        {
            var reparos = await _context.Reparos
                .Include(r => r.Cliente)
                .Include(r => r.Equipamento)
                .Include(r => r.Orcamento)
                .Where(r => r.ClienteId == clienteId)
                .ToListAsync();

            return _mapper.Map<List<ReparoDto>>(reparos);
        }

        public async Task<ReparoDto> CreateFromOrcamentoAsync(int orcamentoId)
        {
            var orcamento = await _context.Orcamentos
                .Include(o => o.Cliente)
                .Include(o => o.Equipamento)
                .FirstOrDefaultAsync(o => o.Id == orcamentoId);

            if (orcamento == null)
                throw new ArgumentException("Orçamento não encontrado.");
            if (orcamento.Status != "Aprovado")
                throw new ArgumentException("Orçamento deve estar aprovado.");

            var reparo = new Reparo
            {
                ClienteId = orcamento.ClienteId,
                EquipamentoId = orcamento.EquipamentoId,
                OrcamentoId = orcamento.Id,
                FormaDePagamento = orcamento.FormaDePagamento,
                PrazoDeEntrega = orcamento.PrazoDeEntrega,
                Observacao = orcamento.Observacao,
                ValorSemDesconto = orcamento.ValorSemDesconto,
                ValorComDesconto = orcamento.ValorComDesconto,
                DataInicio = DateTime.Now,
                Status = "Iniciado"
            };

            _context.Reparos.Add(reparo);
            await _context.SaveChangesAsync();

            // Copiar peças do orçamento para o reparo
            var orcamentoPecas = await _context.OrcamentoPecas
                .Where(op => op.OrcamentoId == orcamentoId)
                .ToListAsync();

            foreach (var op in orcamentoPecas)
            {
                var reparoPeca = new ReparoPeca
                {
                    ReparoId = reparo.Id,
                    PecaId = op.PecaId,
                    Quantidade = op.Quantidade,
                    PrecoUnitario = op.PrecoUnitario,
                    PecaUtilizada = false
                };
                _context.ReparoPecas.Add(reparoPeca);
            }

            await _context.SaveChangesAsync();

            return await GetByIdAsync(reparo.Id) ?? throw new Exception("Erro ao criar reparo.");
        }
    }
}