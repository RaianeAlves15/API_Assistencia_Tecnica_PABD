using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API_assistencia_tecnica.Models
{
    public class FornecedorPeca
    {
        public int FornecedorId { get; set; }
        public Fornecedor Fornecedor { get; set; }

        public int PecaId { get; set; }
        public Peca Peca { get; set; }

        public decimal PrecoUnitario { get; set; }
        public DateTime DataUltimaCompra { get; set; }
    }
}

