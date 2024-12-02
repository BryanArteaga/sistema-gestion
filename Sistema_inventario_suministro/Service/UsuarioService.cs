using System;
using System.Threading.Tasks;
using Sistema_inventario_suministro.Models;
using Sistema_inventario_suministro.Repository;

namespace Sistema_inventario_suministro.Service
{
    public interface IUsuarioService
    {
        Task<Usuario> RegistrarAsync(Usuario usuario);
        Task<string> IniciarSesionAsync(string nombre, string clave);
    }

    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioRepository _repository;
        private readonly ITokenService _tokenService;

        public UsuarioService(IUsuarioRepository repository, ITokenService tokenService)
        {
            _repository = repository;
            _tokenService = tokenService;
        }

        public async Task<Usuario> RegistrarAsync(Usuario usuario)
        {
            if (usuario.Estado != Estado.Activo && usuario.Estado != Estado.Inactivo)
            {
                throw new ArgumentException("Estado inválido. Debe ser 'Activo' o 'Inactivo'.");
            }

            return await _repository.CreateUsuarioAsync(usuario);
        }

        public async Task<string> IniciarSesionAsync(string nombre, string clave)
        {
            try
            {
                var usuario = await _repository.GetUsuarioAsync(nombre, clave);
                if (usuario != null && usuario.Estado == Estado.Activo)
                {
                    return _tokenService.GenerateToken(usuario);
                }

                throw new UnauthorizedAccessException("Credenciales incorrectas o usuario inactivo.");
            }
            catch (Exception ex)
            {
                // Loguear el error
                Console.WriteLine($"Error iniciando sesión: {ex.Message}");
                throw;
            }
        }
    }
}
