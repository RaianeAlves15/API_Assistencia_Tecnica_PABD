using API_assistencia_tecnica.DataContexts;
using API_assistencia_tecnica.Models;
using Microsoft.EntityFrameworkCore;

namespace API_assistencia_tecnica.Services
{
    public class FornecedorService
    {
        private readonly AppDbContext _context;

        public FornecedorService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Fornecedor>> GetAllAsync()
        {
            return await _context.Fornecedores.ToListAsync();
        }

        public async Task<Fornecedor?> GetByIdAsync(int id)
        {
            return await _context.Fornecedores.FirstOrDefaultAsync(f => f.IdFornecedor == id);
        }

        public async Task<Fornecedor> CreateAsync(Fornecedor fornecedor)
        {
            _context.Fornecedores.Add(fornecedor);
            await _context.SaveChangesAsync();
            return fornecedor;
        }

        public async Task<Fornecedor?> UpdateAsync(int id, Fornecedor fornecedor)
        {
            var existing = await _context.Fornecedores.FindAsync(id);
            if (existing == null) return null;

            existing.NomeFornecedor = fornecedor.NomeFornecedor;
            existing.CnpjCpf = fornecedor.CnpjCpf;
            existing.InscricaoEstadual = fornecedor.InscricaoEstadual;
            existing.Email = fornecedor.Email;
            existing.Telefone = fornecedor.Telefone;
            existing.TelefoneCelular = fornecedor.TelefoneCelular;
            existing.NumeroDoImovel = fornecedor.NumeroDoImovel;
            existing.Cep = fornecedor.Cep;
            existing.Bairro = fornecedor.Bairro;
            existing.Cidade = fornecedor.Cidade;
            existing.Estado = fornecedor.Estado;
            existing.Pais = fornecedor.Pais;
            existing.Site = fornecedor.Site;
            existing.Representante = fornecedor.Representante;

            _context.Fornecedores.Update(existing);
            await _context.SaveChangesAsync();
            return existing;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var item = await _context.Fornecedores.FindAsync(id);
            if (item == null) return false;

            _context.Fornecedores.Remove(item);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.Fornecedores.AnyAsync(f => f.IdFornecedor == id);
        }
    }
}