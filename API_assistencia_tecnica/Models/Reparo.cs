namespace API_assistencia_tecnica.Models
{
    public class Reparo
    {
        public int IdLancamentoReparo { get; set; }

        // Dados do Cliente
        public required string NomeCliente { get; set; }
        public required string Cpf { get; set; }
        public required string Rg { get; set; }
        public required string Telefone { get; set; }
        public required string Rua { get; set; }
        public required string Bairro { get; set; }
        public required string Cidade { get; set; }
        public required string Cep { get; set; }

        // Dados do Equipamento
        public required string NomeEquipamento { get; set; }
        public required string Modelo { get; set; }
        public required string Fabricante { get; set; }
        public required int AnoFabricacao { get; set; }
        public required string Voltagem { get; set; }
        public required string Amperagem { get; set; }

        // Reparo
        public required string Pecas { get; set; } // Pode ser uma string ou uma lista, depende do uso
        public required string FormaDePagamento { get; set; }
        public required string PrazoDeEntrega { get; set; }
        public string? Observacao { get; set; }

        // Valores
        public required decimal ValorSemDesconto { get; set; }
        public required decimal ValorComDesconto { get; set; }
    }
}
