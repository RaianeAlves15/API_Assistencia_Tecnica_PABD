using System.ComponentModel.DataAnnotations;

namespace API_assistencia_tecnica.Models
{
    public class Reparo
    {
        [Key]
        public int Id { get; set; } // ✅ NÃO IdLancamentoReparo

        // ✅ Relacionamentos (NOVO)
        public int ClienteId { get; set; }
        public Cliente Cliente { get; set; }

        public int EquipamentoId { get; set; }
        public Equipamento Equipamento { get; set; }

        public int? OrcamentoId { get; set; } // ✅ Opcional
        public Orcamento? Orcamento { get; set; }

        // ✅ Dados específicos do reparo
        public required string FormaDePagamento { get; set; }
        public required string PrazoDeEntrega { get; set; }
        public string? Observacao { get; set; }
        public string? Diagnostico { get; set; }
        public string? SolucaoAplicada { get; set; }
        public required decimal ValorSemDesconto { get; set; }
        public required decimal ValorComDesconto { get; set; }

        // ✅ Controle (NOVO)
        public DateTime DataInicio { get; set; } = DateTime.Now;
        public DateTime? DataConclusao { get; set; }
        public DateTime? DataEntrega { get; set; }
        public string Status { get; set; } = "Iniciado";

        // ❌ NÃO deve ter: NomeCliente, Cpf, Rg, NomeEquipamento, etc.
    }
}
