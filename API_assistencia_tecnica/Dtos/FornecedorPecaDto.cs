namespace API_assistencia_tecnica.Dtos
{
    public class FornecedorPecaDto
    {
        public int Id { get; set; }  // Adicionado
        public int FornecedorId { get; set; }
        public int PecaId { get; set; }
        public decimal PrecoUnitario { get; set; }
        public DateTime DataUltimaCompra { get; set; }
    }
}
