using Microsoft.EntityFrameworkCore;
using API_assistencia_tecnica.Models;
using API_assistencia_tecnica.Dtos;
using API_assistencia_tecnica.DataContexts; // 🔧 IMPORTANTE!

namespace API_assistencia_tecnica.Services
{
    public class ReparoService
    {
        private readonly AppDbContext _context;

        public ReparoService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Reparo>> GetAllAsync()
        {
            return await _context.Reparos.ToListAsync();
        }

        public async Task<Reparo?> GetByIdAsync(int id)
        {
            return await _context.Reparos.FirstOrDefaultAsync(r => r.IdLancamentoReparo == id);
        }

        public async Task<Reparo> CreateAsync(ReparoDto dto)
        {
            var reparo = new Reparo
            {
                NomeCliente = dto.NomeCliente,
                Cpf = dto.Cpf,
                Rg = dto.Rg,
                Telefone = dto.Telefone,
                Rua = dto.Rua,
                Bairro = dto.Bairro,
                Cidade = dto.Cidade,
                Cep = dto.Cep,
                NomeEquipamento = dto.NomeEquipamento,
                Modelo = dto.Modelo,
                Fabricante = dto.Fabricante,
                AnoFabricacao = dto.AnoFabricacao,
                Voltagem = dto.Voltagem,
                Amperagem = dto.Amperagem,
                Pecas = dto.Pecas,
                FormaDePagamento = dto.FormaDePagamento,
                PrazoDeEntrega = dto.PrazoDeEntrega,
                Observacao = dto.Observacao,
                ValorSemDesconto = dto.ValorSemDesconto,
                ValorComDesconto = dto.ValorComDesconto
            };

            _context.Reparos.Add(reparo);
            await _context.SaveChangesAsync();
            return reparo;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var item = await _context.Reparos.FindAsync(id);
            if (item == null) return false;

            _context.Reparos.Remove(item);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.Reparos.AnyAsync(r => r.IdLancamentoReparo == id);
        }
    }
}
