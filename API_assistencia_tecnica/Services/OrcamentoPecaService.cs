﻿using Microsoft.EntityFrameworkCore;
using API_assistencia_tecnica.DataContexts;
using API_assistencia_tecnica.Dtos;
using API_assistencia_tecnica.Models;
using AutoMapper;

namespace API_assistencia_tecnica.Services
{
    public class OrcamentoPecaService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public OrcamentoPecaService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<OrcamentoPecaDto>> GetAllAsync()
        {
            var list = await _context.OrcamentoPecas
                .Include(op => op.Orcamento)
                .Include(op => op.Peca)
                .ToListAsync();

            return _mapper.Map<List<OrcamentoPecaDto>>(list);
        }

        public async Task<OrcamentoPecaDto?> GetByIdAsync(int orcamentoId, int pecaId)
        {
            var entity = await _context.OrcamentoPecas
                .Include(op => op.Orcamento)
                .Include(op => op.Peca)
                .FirstOrDefaultAsync(op => op.OrcamentoId == orcamentoId && op.PecaId == pecaId);

            return entity == null ? null : _mapper.Map<OrcamentoPecaDto>(entity);
        }

        public async Task<OrcamentoPecaDto> CreateAsync(OrcamentoPecaDto dto)
        {
            // ✅ Validar se orçamento e peça existem
            var orcamentoExists = await _context.Orcamentos.AnyAsync(o => o.Id == dto.OrcamentoId);
            var pecaExists = await _context.Pecas.AnyAsync(p => p.Id == dto.PecaId);

            if (!orcamentoExists)
                throw new ArgumentException("Orçamento não encontrado.");
            if (!pecaExists)
                throw new ArgumentException("Peça não encontrada.");

            var entity = _mapper.Map<OrcamentoPeca>(dto);
            _context.OrcamentoPecas.Add(entity);
            await _context.SaveChangesAsync();

            return await GetByIdAsync(entity.OrcamentoId, entity.PecaId) ?? throw new Exception("Erro ao criar relação.");
        }

        public async Task<OrcamentoPecaDto?> UpdateAsync(int orcamentoId, int pecaId, OrcamentoPecaDto dto)
        {
            var entity = await _context.OrcamentoPecas
                .FirstOrDefaultAsync(op => op.OrcamentoId == orcamentoId && op.PecaId == pecaId);

            if (entity == null) return null;

            // Mapear apenas os campos que podem ser atualizados
            entity.Quantidade = dto.Quantidade;
            entity.PrecoUnitario = dto.PrecoUnitario;

            await _context.SaveChangesAsync();
            return await GetByIdAsync(orcamentoId, pecaId);
        }

        // ✅ DELETE sem dependências (é uma tabela de relacionamento)
        public async Task<bool> DeleteAsync(int orcamentoId, int pecaId)
        {
            var entity = await _context.OrcamentoPecas
                .FirstOrDefaultAsync(op => op.OrcamentoId == orcamentoId && op.PecaId == pecaId);

            if (entity == null) return false;

            _context.OrcamentoPecas.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<OrcamentoPecaDto>> GetPecasByOrcamentoIdAsync(int orcamentoId)
        {
            var list = await _context.OrcamentoPecas
                .Include(op => op.Orcamento)
                .Include(op => op.Peca)
                .Where(op => op.OrcamentoId == orcamentoId)
                .ToListAsync();

            return _mapper.Map<List<OrcamentoPecaDto>>(list);
        }

        public async Task<List<OrcamentoPecaDto>> GetOrcamentosByPecaIdAsync(int pecaId)
        {
            var list = await _context.OrcamentoPecas
                .Include(op => op.Orcamento)
                .Include(op => op.Peca)
                .Where(op => op.PecaId == pecaId)
                .ToListAsync();

            return _mapper.Map<List<OrcamentoPecaDto>>(list);
        }

        public async Task<bool> ExistsAsync(int orcamentoId, int pecaId)
        {
            return await _context.OrcamentoPecas
                .AnyAsync(op => op.OrcamentoId == orcamentoId && op.PecaId == pecaId);
        }
    }
}