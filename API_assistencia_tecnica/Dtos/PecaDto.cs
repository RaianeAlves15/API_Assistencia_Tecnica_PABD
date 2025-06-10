using System.ComponentModel.DataAnnotations;

namespace API_assistencia_tecnica.Dtos
{
    public class PecaDto
    {
        [Required] public string NomePeca { get; set; }
        [Required] public string Fabricante { get; set; }
        [Required] public string LocalDeFabricacao { get; set; }
        [Required] public double PesoKg { get; set; }
        [Required] public int Quantidade { get; set; }
        [Required] public string NumeroDeSerie { get; set; }
        [Required] public string CodigoDeProducao { get; set; }
        [Required] public decimal Preco { get; set; }
        [Required] public string Observacao { get; set; }
    }
}
