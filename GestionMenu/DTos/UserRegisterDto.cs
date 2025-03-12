namespace GestionMenu.DTos
{
    public class UserRegisterDto
    {
        public required string Email { get; set; } = string.Empty;
        public required string Password { get; set; } = string.Empty;
        public required string NombreCompleto { get; set; } = string.Empty;
    }
}

