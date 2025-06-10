using System.ComponentModel.DataAnnotations;

namespace API_assistencia_tecnica.Dtos
{
    public class OrcamentoDto
    {
        // Dados do Cliente
        [Required] public string NomeCliente { get; set; }
        [Required] public string Cpf { get; set; }
        [Required] public string Rg { get; set; }
        [Required] public string Telefone { get; set; }
        [Required] public string Rua { get; set; }
        [Required] public string Bairro { get; set; }
        [Required] public string Cidade { get; set; }
        [Required] public string Cep { get; set; }

        // Dados do Equipamento
        [Required] public string NomeEquipamento { get; set; }
        [Required] public string Modelo { get; set; }
        [Required] public string Fabricante { get; set; }
        [Required] public int AnoFabricacao { get; set; }
        [Required] public string Voltagem { get; set; }
        [Required] public string Amperagem { get; set; }

        // Reparo
        [Required] public string Pecas { get; set; } // string separada por vírgula ou nomes
        [Required] public string FormaDePagamento { get; set; }
        [Required] public string PrazoDeEntrega { get; set; }
        public string? Observacao { get; set; }

        // Valores
        [Required] public decimal ValorSemDesconto { get; set; }
        [Required] public decimal ValorComDesconto { get; set; }
    }
}
