using System.ComponentModel.DataAnnotations;

namespace API_assistencia_tecnica.Dtos
{
    public class OrcamentoDto
    {
        public int Id { get; set; }

        // Relacionamentos
        [Required] public int ClienteId { get; set; }
        [Required] public int EquipamentoId { get; set; }

        // Dados específicos do orçamento
        [Required] public string? FormaDePagamento { get; set; }
        [Required] public string? PrazoDeEntrega { get; set; }
        public string? Observacao { get; set; }

        // Valores
        [Required] public decimal ValorSemDesconto { get; set; }
        [Required] public decimal ValorComDesconto { get; set; }

        // Controle
        public DateTime DataCriacao { get; set; }
        public DateTime? DataAprovacao { get; set; }
        public string Status { get; set; } = "Pendente";

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
