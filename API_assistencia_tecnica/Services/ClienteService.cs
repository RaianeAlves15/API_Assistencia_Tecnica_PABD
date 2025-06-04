using API_assistencia_tecnica.DataContexts;
using API_assistencia_tecnica.Models;
using Microsoft.EntityFrameworkCore; 

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

       

        public async Task<Cliente> CreateAsync(Cliente cliente)
        {
            _context.Clientes.Add(cliente);
            await _context.SaveChangesAsync();
            return cliente;
        }

        public async Task<Cliente?> UpdateAsync(int id, Cliente cliente)
        {
            var existing = await _context.Clientes.FindAsync(id);
            if (existing == null) return null;

            existing.NomeCliente = cliente.NomeCliente;
            existing.CpfCliente = cliente.CpfCliente;
            existing.RgCliente = cliente.RgCliente;
            existing.TelefoneCliente = cliente.TelefoneCliente;
            existing.EmailCliente = cliente.EmailCliente;
            existing.RuaCliente = cliente.RuaCliente;
            existing.BairroCliente = cliente.BairroCliente;
            existing.CidadeCliente = cliente.CidadeCliente;

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