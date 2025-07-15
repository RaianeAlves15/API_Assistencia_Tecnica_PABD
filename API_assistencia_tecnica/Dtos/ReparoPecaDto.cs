namespace API_assistencia_tecnica.Dtos
{
    public class ReparoPecaDto
    {
        public int ReparoId { get; set; }
        public int PecaId { get; set; }
        public int Quantidade { get; set; }
        public decimal PrecoUnitario { get; set; }
        public bool PecaUtilizada { get; set; } = false;

        // Dados da peça (somente leitura para exibição)
        public string? NomePeca { get; set; }
        public string? FabricantePeca { get; set; }
    }
}
