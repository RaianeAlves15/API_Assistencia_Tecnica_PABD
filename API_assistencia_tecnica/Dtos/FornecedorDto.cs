using System.ComponentModel.DataAnnotations;

namespace API_assistencia_tecnica.Dtos
{
    public class FornecedorDto
    {
        [Required] public string NomeFornecedor { get; set; }
        [Required] public string CnpjCpf { get; set; }
        [Required] public string InscricaoEstadual { get; set; }
        [Required, EmailAddress] public string Email { get; set; }
        [Required] public string Telefone { get; set; }
        [Required] public string TelefoneCelular { get; set; }
        [Required] public string NumeroDoImovel { get; set; }
        [Required] public string Cep { get; set; }
        [Required] public string Bairro { get; set; }
        [Required] public string Cidade { get; set; }
        [Required] public string Estado { get; set; }
        [Required] public string Pais { get; set; }
        public string? Site { get; set; }
        [Required] public string Representante { get; set; }
    }
}
