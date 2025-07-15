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

        // Orçamento - ATUALIZADO
        CreateMap<OrcamentoCreateDto, Orcamento>();
        CreateMap<Orcamento, OrcamentoDto>()
            .ForMember(dest => dest.NomeCliente, opt => opt.MapFrom(src => src.Cliente != null ? src.Cliente.NomeCliente : null))
            .ForMember(dest => dest.CpfCliente, opt => opt.MapFrom(src => src.Cliente != null ? src.Cliente.CpfCliente : null))
            .ForMember(dest => dest.TelefoneCliente, opt => opt.MapFrom(src => src.Cliente != null ? src.Cliente.TelefoneCliente : null))
            .ForMember(dest => dest.NomeEquipamento, opt => opt.MapFrom(src => src.Equipamento != null ? src.Equipamento.NomeEquipamento : null))
            .ForMember(dest => dest.ModeloEquipamento, opt => opt.MapFrom(src => src.Equipamento != null ? src.Equipamento.Modelo : null))
            .ForMember(dest => dest.FabricanteEquipamento, opt => opt.MapFrom(src => src.Equipamento != null ? src.Equipamento.Fabricante : null));

        // Reparo - ATUALIZADO
        CreateMap<ReparoCreateDto, Reparo>();
        CreateMap<Reparo, ReparoDto>()
            .ForMember(dest => dest.NomeCliente, opt => opt.MapFrom(src => src.Cliente != null ? src.Cliente.NomeCliente : null))
            .ForMember(dest => dest.CpfCliente, opt => opt.MapFrom(src => src.Cliente != null ? src.Cliente.CpfCliente : null))
            .ForMember(dest => dest.TelefoneCliente, opt => opt.MapFrom(src => src.Cliente != null ? src.Cliente.TelefoneCliente : null))
            .ForMember(dest => dest.NomeEquipamento, opt => opt.MapFrom(src => src.Equipamento != null ? src.Equipamento.NomeEquipamento : null))
            .ForMember(dest => dest.ModeloEquipamento, opt => opt.MapFrom(src => src.Equipamento != null ? src.Equipamento.Modelo : null))
            .ForMember(dest => dest.FabricanteEquipamento, opt => opt.MapFrom(src => src.Equipamento != null ? src.Equipamento.Fabricante : null));

        // Relacionamentos N:N
        CreateMap<OrcamentoPecaDto, OrcamentoPeca>();
        CreateMap<OrcamentoPeca, OrcamentoPecaDto>();

        CreateMap<ReparoEquipamentoDto, ReparoEquipamento>();
        CreateMap<ReparoEquipamento, ReparoEquipamentoDto>();

        CreateMap<FornecedorPecaDto, FornecedorPeca>();
        CreateMap<FornecedorPeca, FornecedorPecaDto>();

        // ReparoPeca - NOVO
        CreateMap<ReparoPecaDto, ReparoPeca>();
        CreateMap<ReparoPeca, ReparoPecaDto>()
            .ForMember(dest => dest.NomePeca, opt => opt.MapFrom(src => src.Peca != null ? src.Peca.NomePeca : null))
            .ForMember(dest => dest.FabricantePeca, opt => opt.MapFrom(src => src.Peca != null ? src.Peca.Fabricante : null));
    }
}