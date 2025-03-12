using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class PlatosController : ControllerBase
{
    private readonly GestionMenuDbContext _context;

    public PlatosController(GestionMenuDbContext context)
    {
        _context = context;
    }

    // 🚀 GET: api/platos - Obtener todos los platos
    [HttpGet]
    public async Task<IActionResult> GetPlatos()
    {
        var platos = await _context.Platos.Include(p => p.Categoria).ToListAsync();
        return Ok(platos);
    }

    // 🚀 GET: api/platos/{id} - Obtener un plato por ID
    [HttpGet("{id}")]
    public async Task<IActionResult> GetPlato(int id)
    {
        var plato = await _context.Platos.FindAsync(id);
        if (plato == null) return NotFound();
        return Ok(plato);
    }

    // 🚀 POST: api/platos - Crear un nuevo plato
    [HttpPost]
    public async Task<IActionResult> CreatePlato([FromBody] Plato plato)
    {
        _context.Platos.Add(plato);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetPlato), new { id = plato.PlatoId }, plato);
    }

    // 🚀 PUT: api/platos/{id} - Actualizar un plato existente
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdatePlato(int id, [FromBody] Plato plato)
    {
        if (id != plato.PlatoId) return BadRequest();

        _context.Entry(plato).State = EntityState.Modified;
        await _context.SaveChangesAsync();

        return NoContent();
    }

    // 🚀 DELETE: api/platos/{id} - Eliminar un plato por ID
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletePlato(int id)
    {
        var plato = await _context.Platos.FindAsync(id);
        if (plato == null) return NotFound();

        _context.Platos.Remove(plato);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}


