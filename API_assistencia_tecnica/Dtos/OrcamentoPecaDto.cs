namespace API_assistencia_tecnica.Dtos
{
    public class OrcamentoPecaDto
    {
        public int Id { get; set; }  // Adicionado
        public int OrcamentoId { get; set; }
        public int PecaId { get; set; }
        public int Quantidade { get; set; }
        public decimal PrecoUnitario { get; set; }
    }
}
