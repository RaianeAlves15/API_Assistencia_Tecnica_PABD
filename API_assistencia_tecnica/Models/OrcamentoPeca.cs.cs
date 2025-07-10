using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API_assistencia_tecnica.Models
{
    public class OrcamentoPeca
    {
        // ID auxiliar para facilitar uso em métodos que requerem identificador simples
        public int Id { get; set; }

        [Key, Column(Order = 0)]
        public int OrcamentoId { get; set; }
        public Orcamento Orcamento { get; set; }

        [Key, Column(Order = 1)]
        public int PecaId { get; set; }
        public Peca Peca { get; set; }

        public int Quantidade { get; set; }
        public decimal PrecoUnitario { get; set; }
    }
}
