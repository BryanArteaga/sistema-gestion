using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Sistema_inventario_suministro.Models;
using Sistema_inventario_suministro.Data;
using Sistema_inventario_suministro.Repository;

namespace Sistema_inventario_suministro.Service
{
    public class ProveedorService
    {
        private readonly ProveedoresRepository _repository;
        private readonly InventoryDbContext _context;

        public ProveedorService(ProveedoresRepository repository, InventoryDbContext context)
        {
            _repository = repository;
            _context = context;
        }

        public async Task<List<Proveedor>> ObtenerProveedores()
        {
            return await _repository.GetProveedores();
        }

        public async Task<Proveedor?> ObtenerProveedorPorId(int id)
        {
            return await _repository.GetProveedorById(id);
        }

        public async Task<Proveedor> CrearProveedor(Proveedor proveedor)
        {
            try
            {
                return await _repository.AddProveedor(proveedor);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en CrearProveedor: {ex.Message}");
                throw;
            }
        }

        public async Task<Proveedor> EditarProveedor(Proveedor proveedor)
        {
            try
            {
                await _repository.UpdateProveedor(proveedor);
                return proveedor;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en EditarProveedor: {ex.Message}");
                throw;
            }
        }

        public async Task<bool> EliminarProveedor(int id)
        {
            try
            {
                var proveedor = await _repository.GetProveedorById(id);
                if (proveedor != null)
                {
                    await _repository.DeleteProveedor(proveedor.proveedor_id);
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en EliminarProveedor: {ex.Message}");
                throw;
            }
        }
    }
}