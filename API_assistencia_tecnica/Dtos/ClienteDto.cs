using System.ComponentModel.DataAnnotations;

namespace API_assistencia_tecnica.Dtos
{
    public class ClienteDto
    {
        public int Id { get; set; }

        [Required] public string? NomeCliente { get; set; }
        [Required] public string? CpfCliente { get; set; }
        [Required] public string? RgCliente { get; set; }
        [Required] public string? TelefoneCliente { get; set; }
        [Required, EmailAddress] public string? EmailCliente { get; set; }
        [Required] public string? RuaCliente { get; set; }
        [Required] public string? BairroCliente { get; set; }
        [Required] public string? CidadeCliente { get; set; }
    }
}
