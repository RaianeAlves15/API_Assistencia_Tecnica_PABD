using Microsoft.EntityFrameworkCore;
using API_assistencia_tecnica.Models;
using API_assistencia_tecnica.Dtos;
using API_assistencia_tecnica.DataContexts;
using AutoMapper;

namespace API_assistencia_tecnica.Services
{
    public class ClienteService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public ClienteService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<ClienteDto>> GetAllAsync()
        {
            var clientes = await _context.Clientes.ToListAsync();
            return _mapper.Map<List<ClienteDto>>(clientes);
        }

        public async Task<ClienteDto?> GetByIdAsync(int id)
        {
            var cliente = await _context.Clientes.FindAsync(id);
            return cliente == null ? null : _mapper.Map<ClienteDto>(cliente);
        }

        public async Task<ClienteDto> CreateAsync(ClienteDto dto)
        {
            var cliente = _mapper.Map<Cliente>(dto);
            _context.Clientes.Add(cliente);
            await _context.SaveChangesAsync();
            return _mapper.Map<ClienteDto>(cliente);
        }

        public async Task<ClienteDto?> UpdateAsync(int id, ClienteDto dto)
        {
            var cliente = await _context.Clientes.FindAsync(id);
            if (cliente == null) return null;

            _mapper.Map(dto, cliente);
            await _context.SaveChangesAsync();
            return _mapper.Map<ClienteDto>(cliente);
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
