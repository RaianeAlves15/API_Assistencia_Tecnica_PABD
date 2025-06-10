using Microsoft.EntityFrameworkCore;
using API_assistencia_tecnica.Models;
using API_assistencia_tecnica.Dtos;
using API_assistencia_tecnica.DataContexts;


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

        public async Task<Fornecedor> CreateAsync(FornecedorDto dto)
        {
            var fornecedor = new Fornecedor
            {
                NomeFornecedor = dto.NomeFornecedor,
                CnpjCpf = dto.CnpjCpf,
                InscricaoEstadual = dto.InscricaoEstadual,
                Email = dto.Email,
                Telefone = dto.Telefone,
                TelefoneCelular = dto.TelefoneCelular,
                NumeroDoImovel = dto.NumeroDoImovel,
                Cep = dto.Cep,
                Bairro = dto.Bairro,
                Cidade = dto.Cidade,
                Estado = dto.Estado,
                Pais = dto.Pais,
                Site = dto.Site,
                Representante = dto.Representante
            };

            _context.Fornecedores.Add(fornecedor);
            await _context.SaveChangesAsync();
            return fornecedor;
        }

        public async Task<Fornecedor?> UpdateAsync(int id, FornecedorDto dto)
        {
            var existing = await _context.Fornecedores.FindAsync(id);
            if (existing == null) return null;

            existing.NomeFornecedor = dto.NomeFornecedor;
            existing.CnpjCpf = dto.CnpjCpf;
            existing.InscricaoEstadual = dto.InscricaoEstadual;
            existing.Email = dto.Email;
            existing.Telefone = dto.Telefone;
            existing.TelefoneCelular = dto.TelefoneCelular;
            existing.NumeroDoImovel = dto.NumeroDoImovel;
            existing.Cep = dto.Cep;
            existing.Bairro = dto.Bairro;
            existing.Cidade = dto.Cidade;
            existing.Estado = dto.Estado;
            existing.Pais = dto.Pais;
            existing.Site = dto.Site;
            existing.Representante = dto.Representante;

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
