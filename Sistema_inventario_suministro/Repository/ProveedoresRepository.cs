using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Sistema_inventario_suministro.Models;
using Sistema_inventario_suministro.Data;

namespace Sistema_inventario_suministro.Repository
{
    public class ProveedoresRepository
    {
        private readonly InventoryDbContext _context;

        public ProveedoresRepository(InventoryDbContext context)
        {
            _context = context;
        }

        public async Task<List<Proveedor>> GetProveedores()
        {
            return await _context.Proveedores.Include(p => p.Medicamentos).ToListAsync();
        }

        public async Task<Proveedor?> GetProveedorById(int id)
        {
            return await _context.Proveedores.FirstOrDefaultAsync(p => p.proveedor_id == id);
        }

        public async Task<Proveedor> AddProveedor(Proveedor proveedor)
        {
            try
            {
                var proveedorExistente = await _context.Proveedores.FindAsync(proveedor.proveedor_id);
                if (proveedorExistente != null)
                {
                    throw new InvalidOperationException($"El proveedor con ID {proveedor.proveedor_id} ya existe.");
                }
                await _context.Proveedores.AddAsync(proveedor);
                await _context.SaveChangesAsync();
                return proveedor;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en AddProveedor: {ex.Message}");
                throw;
            }
        }

        public async Task UpdateProveedor(Proveedor proveedor)
        {
            try
            {
                var proveedorExistente = await _context.Proveedores.FindAsync(proveedor.proveedor_id);
                if (proveedorExistente == null)
                {
                    throw new InvalidOperationException($"El proveedor con ID {proveedor.proveedor_id} no existe.");
                }
                proveedorExistente.nombre = proveedor.nombre;
                proveedorExistente.contacto = proveedor.contacto;
                proveedorExistente.telefono = proveedor.telefono;
                _context.Proveedores.Update(proveedorExistente);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en UpdateProveedor: {ex.Message}");
                throw;
            }
        }

        public async Task DeleteProveedor(int proveedor_Id)
        {
            try
            {
                var proveedor = await _context.Proveedores.FindAsync(proveedor_Id);
                if (proveedor != null)
                {
                    _context.Proveedores.Remove(proveedor);
                    await _context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en DeleteProveedor: {ex.Message}");
                throw;
            }
        }
    }

}