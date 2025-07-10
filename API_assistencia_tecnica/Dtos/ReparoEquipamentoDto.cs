namespace API_assistencia_tecnica.Dtos
{
    public class ReparoEquipamentoDto
    {
        public int Id { get; set; }  // Adicionado
        public int ReparoId { get; set; }
        public int EquipamentoId { get; set; }
    }
}
