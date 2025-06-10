using Microsoft.EntityFrameworkCore;
using API_assistencia_tecnica.Models;
using API_assistencia_tecnica.Dtos;
using API_assistencia_tecnica.DataContexts;


namespace API_assistencia_tecnica.Services
{
    public class ClienteService
    {
        private readonly AppDbContext _context;

        public ClienteService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Cliente>> GetAllAsync()
        {
            return await _context.Clientes.ToListAsync();
        }

        public async Task<Cliente?> GetByIdAsync(int id)
        {
            return await _context.Clientes.FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<Cliente> CreateAsync(ClienteDto dto)
        {
            var cliente = new Cliente
            {
                NomeCliente = dto.NomeCliente,
                CpfCliente = dto.CpfCliente,
                RgCliente = dto.RgCliente,
                TelefoneCliente = dto.TelefoneCliente,
                EmailCliente = dto.EmailCliente,
                RuaCliente = dto.RuaCliente,
                BairroCliente = dto.BairroCliente,
                CidadeCliente = dto.CidadeCliente
            };

            _context.Clientes.Add(cliente);
            await _context.SaveChangesAsync();
            return cliente;
        }

        public async Task<Cliente?> UpdateAsync(int id, ClienteDto dto)
        {
            var existing = await _context.Clientes.FindAsync(id);
            if (existing == null) return null;

            existing.NomeCliente = dto.NomeCliente;
            existing.CpfCliente = dto.CpfCliente;
            existing.RgCliente = dto.RgCliente;
            existing.TelefoneCliente = dto.TelefoneCliente;
            existing.EmailCliente = dto.EmailCliente;
            existing.RuaCliente = dto.RuaCliente;
            existing.BairroCliente = dto.BairroCliente;
            existing.CidadeCliente = dto.CidadeCliente;

            _context.Clientes.Update(existing);
            await _context.SaveChangesAsync();
            return existing;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var cliente = await _context.Clientes.FindAsync(id);
            if (cliente == null) return false;

            _context.Clientes.Remove(cliente);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.Clientes.AnyAsync(c => c.Id == id);
        }
    }
}
