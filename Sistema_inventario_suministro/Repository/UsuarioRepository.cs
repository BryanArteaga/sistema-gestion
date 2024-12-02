using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Sistema_inventario_suministro.Models;
using Sistema_inventario_suministro.Data;

namespace Sistema_inventario_suministro.Repository
{
    public interface IUsuarioRepository
    {
        Task<Usuario?> GetUsuarioAsync(string nombre, string clave);
        Task<Usuario> CreateUsuarioAsync(Usuario usuario);
    }

    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly InventoryDbContext _context;

        public UsuarioRepository(InventoryDbContext context)
        {
            _context = context;
        }

        public async Task<Usuario?> GetUsuarioAsync(string nombre, string clave)
        {
            var usuario = await _context.Usuarios // Asegurarse de usar el DbSet correcto
                .FirstOrDefaultAsync(u => u.Nombre == nombre);

            if (usuario != null && BCrypt.Net.BCrypt.Verify(clave, usuario.Clave))
            {
                return usuario;
            }

            return null;
        }
        
        public async Task<Usuario> CreateUsuarioAsync(Usuario usuario)
        {
            usuario.Clave = BCrypt.Net.BCrypt.HashPassword(usuario.Clave);
            _context.Usuarios.Add(usuario);
            await _context.SaveChangesAsync();
            return usuario;
        }

    }
}
