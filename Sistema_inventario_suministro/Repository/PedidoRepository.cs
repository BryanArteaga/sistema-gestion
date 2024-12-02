using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Sistema_inventario_suministro.Models;
using Sistema_inventario_suministro.Data;

namespace Sistema_inventario_suministro.Repository
{
    public class PedidoRepository
    {
        private readonly InventoryDbContext _context;

        public PedidoRepository(InventoryDbContext context)
        {
            _context = context;
        }

        public async Task<List<Pedido>> GetPedidos()
        {
            return await _context.Pedidos.Include(p => p.Proveedor).ToListAsync();
        }

        public async Task<Pedido?> GetPedidoById(int id)
        {
            return await _context.Pedidos.FirstOrDefaultAsync(p => p.pedido_id == id);
        }

        public async Task AddPedido(Pedido pedido)
        {
            try
            {
                var proveedorExistente = await _context.Proveedores.FindAsync(pedido.proveedor_id);
                if (proveedorExistente == null)
                {
                    throw new InvalidOperationException($"El proveedor con ID {pedido.proveedor_id} no existe.");
                }

                await _context.Pedidos.AddAsync(pedido);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en AddPedido: {ex.Message}");
                throw;
            }
        }

        public async Task UpdatePedido(Pedido pedido)
        {
            try
            {
                var pedidoExistente = await _context.Pedidos.FindAsync(pedido.pedido_id);
                if (pedidoExistente == null)
                {
                    throw new InvalidOperationException($"El pedido con ID {pedido.pedido_id} no existe.");
                }
                pedidoExistente.fecha_pedido = pedido.fecha_pedido;
                pedidoExistente.fecha_entrega = pedido.fecha_entrega;
                pedidoExistente.estado = pedido.estado;
                _context.Pedidos.Update(pedidoExistente);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en UpdatePedido: {ex.Message}");
                throw;
            }
        }

        public async Task DeletePedido(int pedido_Id)
        {
            try
            {
                var pedido = await _context.Pedidos.FindAsync(pedido_Id);
                if (pedido != null)
                {
                    _context.Pedidos.Remove(pedido);
                    await _context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en DeletePedido: {ex.Message}");
                throw;
            }
        }
    }
}