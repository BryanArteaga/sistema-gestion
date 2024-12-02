using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization; // Asegúrate de agregar esto
using Sistema_inventario_suministro.Models;
using Sistema_inventario_suministro.Service;

namespace Sistema_inventario_suministro.Controller
{
    [Route("[controller]")]
    [ApiController]
    public class ControllerProveedor : ControllerBase
    {
        private readonly ProveedorService _service;

        public ControllerProveedor(ProveedorService service)
        {
            _service = service;
        }

        [Authorize]
        [HttpGet]
        public async Task<ActionResult<List<Proveedor>>> GetProveedores()
        {
            return await _service.ObtenerProveedores();
        }

        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<Proveedor?>> GetProveedorById(int id)
        {
            return await _service.ObtenerProveedorPorId(id);
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult<Proveedor>> CrearProveedor(Proveedor proveedor)
        {
            var result = await _service.CrearProveedor(proveedor);
            return CreatedAtAction(nameof(GetProveedorById), new { id = result.proveedor_id }, result);
        }

        [Authorize]
        [HttpPut("{id}")]
        public async Task<ActionResult<Proveedor>> EditarProveedor(int id, Proveedor proveedor)
        {
            proveedor.proveedor_id = id;
            var result = await _service.EditarProveedor(proveedor);
            return Ok(result);
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<ActionResult> EliminarProveedor(int id)
        {
            await _service.EliminarProveedor(id);
            return NoContent();
        }
    }
}
