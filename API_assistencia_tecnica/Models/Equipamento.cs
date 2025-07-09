using System.ComponentModel.DataAnnotations;

namespace API_assistencia_tecnica.Models
{
    public class Equipamento
    {
        [Key]
        public int IdEquipamento { get; set; }

        public required string NomeEquipamento { get; set; }

        public required string Fabricante { get; set; }

        public required string Modelo { get; set; }

        public required string NumeroDeSerie { get; set; }

        public required string CodigoDeFabricacao { get; set; }

        public required int AnoDeFabricacao { get; set; }

        public required string Voltagem { get; set; }

        public required string Amperagem { get; set; }

    }
}
