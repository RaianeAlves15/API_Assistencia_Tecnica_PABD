using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API_assistencia_tecnica.Models
{
    public class ReparoEquipamento
    {
        public int ReparoId { get; set; }
        public Reparo Reparo { get; set; }

        public int EquipamentoId { get; set; }
        public Equipamento Equipamento { get; set; }
    }
}
