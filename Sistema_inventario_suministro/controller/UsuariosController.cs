using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sistema_inventario_suministro.Models;
using Sistema_inventario_suministro.Service;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Sistema_inventario_suministro.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuariosController : ControllerBase
    {
        private readonly IUsuarioService _service;

        public UsuariosController(IUsuarioService service)
        {
            _service = service;
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> RegistrarAsync([FromBody] Usuario usuario)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var createdUser = await _service.RegistrarAsync(usuario);
                return Ok(new { id = createdUser.Id, message = "Usuario registrado exitosamente." });
            }
            catch (DbUpdateException ex)
            {
                var innerException = ex.InnerException?.Message ?? ex.Message;
                // Loguear el error
                Console.WriteLine($"Error registrando usuario: {innerException}");
                return StatusCode(StatusCodes.Status500InternalServerError, $"Se produjo un error en el servidor: {innerException}");
            }
            catch (Exception ex)
            {
                // Loguear otros errores
                Console.WriteLine($"Error registrando usuario: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, $"Se produjo un error en el servidor: {ex.Message}");
            }
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> IniciarSesionAsync([FromBody] LoginModel model)
        {
            if (string.IsNullOrEmpty(model?.Nombre) || string.IsNullOrEmpty(model?.Clave))
            {
                return BadRequest("Nombre y clave no pueden ser nulos o vacíos.");
            }

            try
            {
                var token = await _service.IniciarSesionAsync(model.Nombre, model.Clave);
                return Ok(new { token });
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(ex.Message);
            }
            catch (Exception ex)
            {
                // Loguear el error
                Console.WriteLine($"Error iniciando sesión: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, $"Se produjo un error en el servidor: {ex.Message}");
            }
        }
        public class LoginModel
        {
            public string? Nombre { get; set; }
            public string? Clave { get; set; }
        }
    }
}
