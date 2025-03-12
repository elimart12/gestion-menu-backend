public class Plato
{
    public int PlatoId { get; set; }
    public required string Nombre { get; set; }
    public required string Descripcion { get; set; }
    public decimal Precio { get; set; }
    public string? ImagenUrl { get; set; }
    public bool Disponible { get; set; }
    public int? CategoriaId { get; set; }
    public Categoria? Categoria { get; set; }
}
