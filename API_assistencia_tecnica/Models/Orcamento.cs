using System.ComponentModel.DataAnnotations;

namespace API_assistencia_tecnica.Models
{
    public class Orcamento
    {
        [Key]
        public int Id { get; set; } // 

        public int ClienteId { get; set; }
        public Cliente Cliente { get; set; }

        public int EquipamentoId { get; set; }
        public Equipamento Equipamento { get; set; }

   
        public required string FormaDePagamento { get; set; }
        public required string PrazoDeEntrega { get; set; }
        public string? Observacao { get; set; }
        public required decimal ValorSemDesconto { get; set; }
        public required decimal ValorComDesconto { get; set; }

    
        public DateTime DataCriacao { get; set; } = DateTime.Now;
        public DateTime? DataAprovacao { get; set; }
        public string Status { get; set; } = "Pendente";

 
    }
}
