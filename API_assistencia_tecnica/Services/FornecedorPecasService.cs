using Microsoft.EntityFrameworkCore;
using API_assistencia_tecnica.DataContexts;
using API_assistencia_tecnica.Dtos;
using API_assistencia_tecnica.Models;
using AutoMapper;

namespace API_assistencia_tecnica.Services
{
    public class FornecedorPecasService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public FornecedorPecasService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<FornecedorPecaDto>> GetAllAsync()
        {
            var list = await _context.FornecedorPecas
                .Include(fp => fp.Fornecedor)
                .Include(fp => fp.Peca)
                .ToListAsync();

            return _mapper.Map<List<FornecedorPecaDto>>(list);
        }

        public async Task<FornecedorPecaDto> CreateAsync(FornecedorPecaDto dto)
        {
            var entity = _mapper.Map<FornecedorPeca>(dto);
            _context.FornecedorPecas.Add(entity);
            await _context.SaveChangesAsync();
            return _mapper.Map<FornecedorPecaDto>(entity);
        }
    }
}
