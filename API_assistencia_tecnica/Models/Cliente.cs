namespace API_assistencia_tecnica.Models
{
    public class Cliente
    {
        public int Id { get; set; }

        public required string NomeCliente { get; set; }

        public required string CpfCliente { get; set; }

        public required string RgCliente { get; set; }

        public required string TelefoneCliente { get; set; }

        public required string EmailCliente { get; set; }

        public required string RuaCliente { get; set; }

        public required string BairroCliente { get; set; }

        public required string CidadeCliente { get; set; }

    }
}
