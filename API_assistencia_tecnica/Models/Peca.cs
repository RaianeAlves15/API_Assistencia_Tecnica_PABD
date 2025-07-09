using System.ComponentModel.DataAnnotations;

namespace API_assistencia_tecnica.Models
{
    public class Peca
    {
        [Key]
        public int Id { get; set; }

        public required string NomePeca { get; set; }

        public required string Fabricante { get; set; }

        public required string LocalDeFabricacao { get; set; }

        public required double PesoKg { get; set; }

        public required int Quantidade { get; set; }

        public required string NumeroDeSerie { get; set; }

        public required string CodigoDeProducao { get; set; }

        public required decimal Preco { get; set; }

        public required string Observacao { get; set; }

    }
}
