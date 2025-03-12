public class Categoria
{
    public int CategoriaId { get; set; }
    public required string Nombre { get; set; }
    public ICollection<Plato> Platos { get; set; } = new List<Plato>();
}

