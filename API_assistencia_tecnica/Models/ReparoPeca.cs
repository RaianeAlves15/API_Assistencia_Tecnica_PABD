using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API_assistencia_tecnica.Models

{
    public class ReparoPeca
    {
        public int ReparoId { get; set; }
        public Reparo Reparo { get; set; }

        public int PecaId { get; set; }
        public Peca Peca { get; set; }

        public int Quantidade { get; set; }
        public decimal PrecoUnitario { get; set; }
        public bool PecaUtilizada { get; set; } = false; // Para controlar se a peça foi realmente utilizada
    }
}
