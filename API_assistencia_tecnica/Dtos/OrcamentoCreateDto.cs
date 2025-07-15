using System.ComponentModel.DataAnnotations;

namespace API_assistencia_tecnica.Dtos
{
    public class OrcamentoCreateDto
    {
        [Required] public int ClienteId { get; set; }
        [Required] public int EquipamentoId { get; set; }
        [Required] public string FormaDePagamento { get; set; }
        [Required] public string PrazoDeEntrega { get; set; }
        public string? Observacao { get; set; }
        [Required] public decimal ValorSemDesconto { get; set; }
        [Required] public decimal ValorComDesconto { get; set; }
    }

    public class ReparoCreateDto
    {
        [Required] public int ClienteId { get; set; }
        [Required] public int EquipamentoId { get; set; }
        public int? OrcamentoId { get; set; }
        [Required] public string FormaDePagamento { get; set; }
        [Required] public string PrazoDeEntrega { get; set; }
        public string? Observacao { get; set; }
        public string? Diagnostico { get; set; }
        public string? SolucaoAplicada { get; set; }
        [Required] public decimal ValorSemDesconto { get; set; }
        [Required] public decimal ValorComDesconto { get; set; }
    }
}