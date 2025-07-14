using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API_assistencia_tecnica.Models
{
    public class OrcamentoPeca
    {
        public int OrcamentoId { get; set; }
        public Orcamento Orcamento { get; set; }

        public int PecaId { get; set; }
        public Peca Peca { get; set; }

        public int Quantidade { get; set; }
        public decimal PrecoUnitario { get; set; }
    }
}
