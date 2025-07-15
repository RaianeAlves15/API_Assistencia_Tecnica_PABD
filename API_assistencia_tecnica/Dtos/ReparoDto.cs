using System.ComponentModel.DataAnnotations;

namespace API_assistencia_tecnica.Dtos
{
    public class ReparoDto
    {
        public int Id { get; set; }

        // Relacionamentos
        [Required] public int ClienteId { get; set; }
        [Required] public int EquipamentoId { get; set; }
        public int? OrcamentoId { get; set; } // Opcional, caso venha de um orçamento

        // Dados específicos do reparo
        [Required] public string? FormaDePagamento { get; set; }
        [Required] public string? PrazoDeEntrega { get; set; }
        public string? Observacao { get; set; }
        public string? Diagnostico { get; set; }
        public string? SolucaoAplicada { get; set; }

        // Valores
        [Required] public decimal ValorSemDesconto { get; set; }
        [Required] public decimal ValorComDesconto { get; set; }

        // Controle de datas
        public DateTime DataInicio { get; set; }
        public DateTime? DataConclusao { get; set; }
        public DateTime? DataEntrega { get; set; }
        public string Status { get; set; } = "Iniciado";

        // Dados do cliente (somente leitura para exibição)
        public string? NomeCliente { get; set; }
        public string? CpfCliente { get; set; }
        public string? TelefoneCliente { get; set; }

        // Dados do equipamento (somente leitura para exibição)
        public string? NomeEquipamento { get; set; }
        public string? ModeloEquipamento { get; set; }
        public string? FabricanteEquipamento { get; set; }
    }
}

