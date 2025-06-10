using Microsoft.EntityFrameworkCore;
using API_assistencia_tecnica.Models;
using API_assistencia_tecnica.Dtos;
using API_assistencia_tecnica.DataContexts;

namespace API_assistencia_tecnica.Services
{
    public class OrcamentoService
    {
        private readonly AppDbContext _context;

        public OrcamentoService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Orcamento>> GetAllAsync()
        {
            return await _context.Orcamentos.ToListAsync();
        }

        public async Task<Orcamento?> GetByIdAsync(int id)
        {
            return await _context.Orcamentos.FirstOrDefaultAsync(o => o.IdOrcamento == id);
        }

        public async Task<Orcamento> CreateAsync(OrcamentoDto dto)
        {
            var orcamento = new Orcamento
            {
                // Cliente
                NomeCliente = dto.NomeCliente,
                Cpf = dto.Cpf,
                Rg = dto.Rg,
                Telefone = dto.Telefone,
                Rua = dto.Rua,
                Bairro = dto.Bairro,
                Cidade = dto.Cidade,
                Cep = dto.Cep,

                // Equipamento
                NomeEquipamento = dto.NomeEquipamento,
                Modelo = dto.Modelo,
                Fabricante = dto.Fabricante,
                AnoFabricacao = dto.AnoFabricacao,
                Voltagem = dto.Voltagem,
                Amperagem = dto.Amperagem,

                // Reparo
                Pecas = dto.Pecas,
                FormaDePagamento = dto.FormaDePagamento,
                PrazoDeEntrega = dto.PrazoDeEntrega,
                Observacao = dto.Observacao,

                // Valores
                ValorSemDesconto = dto.ValorSemDesconto,
                ValorComDesconto = dto.ValorComDesconto
            };

            _context.Orcamentos.Add(orcamento);
            await _context.SaveChangesAsync();
            return orcamento;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var item = await _context.Orcamentos.FindAsync(id);
            if (item == null) return false;

            _context.Orcamentos.Remove(item);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.Orcamentos.AnyAsync(o => o.IdOrcamento == id);
        }
    }
}
