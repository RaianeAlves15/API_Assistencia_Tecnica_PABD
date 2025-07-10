using System.ComponentModel.DataAnnotations;

namespace API_assistencia_tecnica.Dtos
{
    public class EquipamentoDto
    {
        public int Id { get; set; }

        [Required] public string? NomeEquipamento { get; set; }
        [Required] public string? Fabricante { get; set; }
        [Required] public string? Modelo { get; set; }
        [Required] public string? NumeroDeSerie { get; set; }
        [Required] public string? CodigoDeFabricacao { get; set; }
        [Required] public int AnoDeFabricacao { get; set; }
        [Required] public string? Voltagem { get; set; }
        [Required] public string? Amperagem { get; set; }
    }
}
