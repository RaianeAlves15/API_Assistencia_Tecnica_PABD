using API_assistencia_tecnica.Dtos;
using API_assistencia_tecnica.Models;
using AutoMapper;

public class MapperProfile : Profile
{
    public MapperProfile()
    {
        // Cliente
        CreateMap<ClienteDto, Cliente>();
        CreateMap<Cliente, ClienteDto>();

        // Equipamento
        CreateMap<EquipamentoDto, Equipamento>();
        CreateMap<Equipamento, EquipamentoDto>();

        // Fornecedor
        CreateMap<FornecedorDto, Fornecedor>();
        CreateMap<Fornecedor, FornecedorDto>();

        // Peça
        CreateMap<PecaDto, Peca>();
        CreateMap<Peca, PecaDto>();

        // Orçamento
        CreateMap<OrcamentoDto, Orcamento>();
        CreateMap<Orcamento, OrcamentoDto>();

        // Reparo
        CreateMap<ReparoDto, Reparo>();
        CreateMap<Reparo, ReparoDto>();

        // Relacionamentos N:N - Simplificados
        CreateMap<OrcamentoPecaDto, OrcamentoPeca>();
        CreateMap<OrcamentoPeca, OrcamentoPecaDto>();

        CreateMap<ReparoEquipamentoDto, ReparoEquipamento>();
        CreateMap<ReparoEquipamento, ReparoEquipamentoDto>();

        CreateMap<FornecedorPecaDto, FornecedorPeca>();
        CreateMap<FornecedorPeca, FornecedorPecaDto>();
    }
}