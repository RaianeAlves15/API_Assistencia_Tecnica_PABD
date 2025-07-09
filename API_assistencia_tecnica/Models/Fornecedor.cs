using System.ComponentModel.DataAnnotations;

namespace API_assistencia_tecnica.Models
{
    public class Fornecedor
    {
        [Key]
        public int IdFornecedor { get; set; }

        public required string NomeFornecedor { get; set; }

        public required string CnpjCpf { get; set; }

        public required string InscricaoEstadual { get; set; }

        public required string Email { get; set; }

        public required string Telefone { get; set; }

        public required string TelefoneCelular { get; set; }

        public required string NumeroDoImovel { get; set; }

        public required string Cep { get; set; }

        public required string Bairro { get; set; }

        public required string Cidade { get; set; }

        public required string Estado { get; set; }

        public required string Pais { get; set; }

        public string? Site { get; set; }

        public required string Representante { get; set; }

    }
}
