using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API_assistencia_tecnica.Models
{
    public class FornecedorPeca
    {
        // ID auxiliar para facilitar uso em métodos que requerem identificador simples
        public int Id { get; set; }

        [Key, Column(Order = 0)]
        public int FornecedorId { get; set; }
        public Fornecedor Fornecedor { get; set; }

        [Key, Column(Order = 1)]
        public int PecaId { get; set; }
        public Peca Peca { get; set; }

        public decimal PrecoUnitario { get; set; }
        public DateTime DataUltimaCompra { get; set; }
    }
}

