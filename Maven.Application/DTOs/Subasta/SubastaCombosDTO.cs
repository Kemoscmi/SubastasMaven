namespace Maven.Application.DTOs.Subasta
{
    public record ComboItemDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
    }

    public record SubastaCombosDTO
    {
        public List<ComboItemDTO> Joyas { get; set; } = new();
        public List<ComboItemDTO> Vendedores { get; set; } = new();
        public List<ComboItemDTO> EstadosSubasta { get; set; } = new();
    }
}