using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using GestionMenu.DTos; // Asegúrate de usar el namespace correcto

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly UserManager<Usuario> _userManager;
    private readonly IConfiguration _configuration;

    public AuthController(UserManager<Usuario> userManager, IConfiguration configuration)
    {
        _userManager = userManager;
        _configuration = configuration;
    }

    // ✅ Endpoint para Registro
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] UserRegisterDto dto)
    {
        var usuario = new Usuario
        {
            UserName = dto.Email,
            Email = dto.Email,
            NombreCompleto = dto.NombreCompleto
        };

        var result = await _userManager.CreateAsync(usuario, dto.Password);
        if (result.Succeeded)
        {
            return Ok("Usuario registrado correctamente");
        }

        return BadRequest(result.Errors);
    }

    // ✅ Endpoint para Login
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] UserLoginDto dto)
    {
        var usuario = await _userManager.FindByEmailAsync(dto.Email);

        if (usuario != null && await _userManager.CheckPasswordAsync(usuario, dto.Password))
        {
            var token = GenerateJwtToken(usuario);
            return Ok(new { token });
        }

        return Unauthorized("Credenciales inválidas");
    }

    // ✅ Método para Generar el Token JWT
    private string GenerateJwtToken(Usuario usuario)
    {
        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, usuario.Email!),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(ClaimTypes.NameIdentifier, usuario.Id)
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: _configuration["Jwt:Issuer"],
            audience: _configuration["Jwt:Audience"],
            claims: claims,
            expires: DateTime.Now.AddHours(1),
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}

