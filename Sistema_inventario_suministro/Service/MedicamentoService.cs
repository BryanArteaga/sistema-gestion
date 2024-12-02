using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Sistema_inventario_suministro.Models;
using Sistema_inventario_suministro.Data;
using Sistema_inventario_suministro.Repository;

namespace Sistema_inventario_suministro.Service
{
    public class MedicamentoService
    {
        private readonly MedicamentoRepository _repository;
        private readonly InventoryDbContext _context;

        public MedicamentoService(MedicamentoRepository repository, InventoryDbContext context)
        {
            _repository = repository;
            _context = context;
        }

        public async Task<List<Medicamento>> ObtenerMedicamentos()
        {
            return await _repository.GetMedicamentos();
        }

        public async Task<Medicamento?> ObtenerMedicamentoPorId(int id)
        {
            return await _repository.GetMedicamentoById(id);
        }

        public async Task<Medicamento> CrearMedicamento(Medicamento medicamento)
        {
            try
            {
                var proveedor = await _context.Proveedores.FindAsync(medicamento.proveedor_id);
                if (proveedor == null)
                {
                    throw new Exception("Proveedor no encontrado.");
                }

                await _repository.AddMedicamento(medicamento);
                return medicamento;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en CrearMedicamento: {ex.Message}");
                throw;
            }
        }

        public async Task<Medicamento> EditarMedicamento(Medicamento medicamento)
        {
            try
            {
                await _repository.UpdateMedicamento(medicamento);
                return medicamento;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en EditarMedicamento: {ex.Message}");
                throw;
            }
        }

        public async Task<bool> EliminarMedicamento(int id)
        {
            try
            {
                var medicamento = await _repository.GetMedicamentoById(id);
                if (medicamento != null)
                {
                    await _repository.DeleteMedicamento(medicamento.medicamento_id);
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en EliminarMedicamento: {ex.Message}");
                throw;
            }
        }

} 
}