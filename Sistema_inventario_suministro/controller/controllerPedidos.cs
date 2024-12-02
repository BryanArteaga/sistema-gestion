using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization; // Asegúrate de agregar esto
using Sistema_inventario_suministro.Models;
using Sistema_inventario_suministro.Service;

namespace Sistema_inventario_suministro.Controller
{
    [Route("[controller]")]
    [ApiController]
    public class ControllerPedidos : ControllerBase
    {
        private readonly PedidoService _service;

        public ControllerPedidos(PedidoService service)
        {
            _service = service;
        }

        [Authorize]
        [HttpGet]
        public async Task<ActionResult<List<Pedido>>> GetPedidos()
        {
            return await _service.GetPedidos();
        }

        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<Pedido>> GetPedidoById(int id)
        {
            return await _service.GetPedidoById(id);
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult<Pedido>> CrearPedido(Pedido pedido)
        {
            var result = await _service.CrearPedido(pedido);
            return CreatedAtAction(nameof(GetPedidoById), new { id = result.pedido_id }, result);
        }

        [Authorize]
        [HttpPut("{id}")]
        public async Task<ActionResult<Pedido>> EditarPedido(int id, Pedido pedido)
        {
            pedido.pedido_id = id;
            var result = await _service.EditarPedido(pedido);
            return Ok(result);
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<ActionResult> EliminarPedido(int id)
        {
            await _service.EliminarPedido(id);
            return NoContent();
        }
    }
}
