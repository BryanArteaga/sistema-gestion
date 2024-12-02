using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Sistema_inventario_suministro.Models;
using Sistema_inventario_suministro.Data;
using Sistema_inventario_suministro.Repository;

namespace Sistema_inventario_suministro.Service
{
    public class PedidoService
    {
        private readonly PedidoRepository _repository;
        private readonly InventoryDbContext _context;

        public PedidoService(PedidoRepository repository, InventoryDbContext context)
        {
            _repository = repository;
            _context = context;
        }

        public async Task<List<Pedido>> GetPedidos()
        {
            return await _repository.GetPedidos();
        }

        public async Task<Pedido?> GetPedidoById(int id)
        {
            return await _repository.GetPedidoById(id);
        }

        public async Task<Pedido> CrearPedido(Pedido pedido)
        {
            try
            {
                var proveedorExistente = await _context.Proveedores.FindAsync(pedido.proveedor_id);
                if (proveedorExistente == null)
                {
                    throw new InvalidOperationException($"El proveedor con ID {pedido.proveedor_id} no existe.");
                }

                await _repository.AddPedido(pedido);
                return pedido;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en CrearPedido: {ex.Message}");
                throw;
            }
        }

        public async Task<Pedido> EditarPedido(Pedido pedido)
        {
            try
            {
                await _repository.UpdatePedido(pedido);
                return pedido;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en EditarPedido: {ex.Message}");
                throw;
            }
        }

        public async Task<bool> EliminarPedido(int id)
        {
            try
            {
                var pedido = await _repository.GetPedidoById(id);
                if (pedido != null)
                {
                    await _repository.DeletePedido(pedido.pedido_id);
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en EliminarPedido: {ex.Message}");
                throw;
            }
        }
    }
}