using System;
using System.IdentityModel.Tokens.Jwt;
using Sistema_inventario_suministro.Models;
using Microsoft.Extensions.Configuration;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace Sistema_inventario_suministro.Service
{
    public interface ITokenService
    {
        string GenerateToken(Usuario usuario);
    }

    public class TokenService : ITokenService
    {
        private readonly string _secretKey;

        public TokenService(IConfiguration configuration)
        {
            _secretKey = configuration["Jwt:Key"] ?? throw new ArgumentNullException("SecretKey no puede ser nulo.");
        }

        public string GenerateToken(Usuario usuario)
        {
            if (usuario == null || string.IsNullOrEmpty(usuario.Nombre) || string.IsNullOrEmpty(usuario.Id.ToString()))
            {
                throw new ArgumentNullException("Datos de usuario no pueden ser nulos.");
            }

            var claims = new[]
            {
                new Claim(ClaimTypes.Name, usuario.Nombre),
                new Claim(ClaimTypes.NameIdentifier, usuario.Id.ToString()),
                new Claim("Estado", usuario.Estado.ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_secretKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: "your_issuer",
                audience: "your_audience",
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(30),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
