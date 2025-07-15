
using Microsoft.EntityFrameworkCore;
using API_assistencia_tecnica.Models;
using API_assistencia_tecnica.Dtos;
using API_assistencia_tecnica.DataContexts;
using AutoMapper;

namespace API_assistencia_tecnica.Services
{
    public class EquipamentoService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public EquipamentoService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<EquipamentoDto>> GetAllAsync()
        {
            var lista = await _context.Equipamentos.ToListAsync();
            return _mapper.Map<List<EquipamentoDto>>(lista);
        }

        public async Task<EquipamentoDto?> GetByIdAsync(int id)
        {
            var item = await _context.Equipamentos.FindAsync(id);
            return item == null ? null : _mapper.Map<EquipamentoDto>(item);
        }

        public async Task<EquipamentoDto> CreateAsync(EquipamentoDto dto)
        {
            var entidade = _mapper.Map<Equipamento>(dto);
            _context.Equipamentos.Add(entidade);
            await _context.SaveChangesAsync();
            return _mapper.Map<EquipamentoDto>(entidade);
        }

        public async Task<EquipamentoDto?> UpdateAsync(int id, EquipamentoDto dto)
        {
            var entidade = await _context.Equipamentos.FindAsync(id);
            if (entidade == null) return null;

            _mapper.Map(dto, entidade);
            await _context.SaveChangesAsync();
            return _mapper.Map<EquipamentoDto>(entidade);
        }

        // ✅ DELETE MELHORADO - Com verificação de dependências
        public async Task<bool> DeleteAsync(int id)
        {
            var equipamento = await _context.Equipamentos.FindAsync(id);
            if (equipamento == null) return false;

            // ✅ Verificar se existem orçamentos ou reparos relacionados
            var temOrcamentos = await _context.Orcamentos.AnyAsync(o => o.EquipamentoId == id);
            var temReparos = await _context.Reparos.AnyAsync(r => r.EquipamentoId == id);
            var temReparoEquipamentos = await _context.ReparoEquipamentos.AnyAsync(re => re.EquipamentoId == id);

            if (temOrcamentos || temReparos || temReparoEquipamentos)
            {
                var vinculos = new List<string>();
                if (temOrcamentos) vinculos.Add("orçamentos");
                if (temReparos) vinculos.Add("reparos");
                if (temReparoEquipamentos) vinculos.Add("reparos de equipamentos");

                throw new InvalidOperationException($"Não é possível excluir o equipamento. Existem {string.Join(", ", vinculos)} vinculados a este equipamento.");
            }

            _context.Equipamentos.Remove(equipamento);
            await _context.SaveChangesAsync();
            return true;
        }

        // ✅ NOVO MÉTODO - Verificar se pode deletar
        public async Task<(bool CanDelete, string Reason)> CanDeleteAsync(int id)
        {
            var equipamento = await _context.Equipamentos.FindAsync(id);
            if (equipamento == null) return (false, "Equipamento não encontrado.");

            var orcamentosCount = await _context.Orcamentos.CountAsync(o => o.EquipamentoId == id);
            var reparosCount = await _context.Reparos.CountAsync(r => r.EquipamentoId == id);
            var reparoEquipamentosCount = await _context.ReparoEquipamentos.CountAsync(re => re.EquipamentoId == id);

            var totalVinculos = orcamentosCount + reparosCount + reparoEquipamentosCount;

            if (totalVinculos > 0)
            {
                return (false, $"Equipamento possui {orcamentosCount} orçamento(s), {reparosCount} reparo(s) e {reparoEquipamentosCount} relação(ões) de reparo vinculados.");
            }

            return (true, "Pode ser excluído.");
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.Equipamentos.AnyAsync(e => e.Id == id);
        }
    }
}