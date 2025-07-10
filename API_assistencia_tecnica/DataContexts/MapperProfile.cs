using AutoMapper;
using API_assistencia_tecnica.Dtos;
using API_assistencia_tecnica.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace API_assistencia_tecnica.DataContexts
{
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

            // Relacionamentos N:N
            CreateMap<OrcamentoPecaDto, OrcamentoPeca>();
            CreateMap<OrcamentoPeca, OrcamentoPecaDto>();

            CreateMap<ReparoEquipamentoDto, ReparoEquipamento>();
            CreateMap<ReparoEquipamento, ReparoEquipamentoDto>();

            CreateMap<FornecedorPecaDto, FornecedorPeca>();
            CreateMap<FornecedorPeca, FornecedorPecaDto>();
        }
    }
}



// adicionar no program builder.Services.AddAutoMapper(typeof(MapperProfile));
