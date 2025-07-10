using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API_assistencia_tecnica.Models
{
    public class ReparoEquipamento
    {
        [Key, Column(Order = 0)]
        public int ReparoId { get; set; }
        public Reparo Reparo { get; set; }

        [Key, Column(Order = 1)]
        public int EquipamentoId { get; set; }
        public Equipamento Equipamento { get; set; }
    }
}
