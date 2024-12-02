using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Sistema_inventario_suministro.Models;
using Sistema_inventario_suministro.Data;

namespace Sistema_inventario_suministro.Repository
{
    public class MedicamentoRepository
    {
        private readonly InventoryDbContext _context;

        public MedicamentoRepository(InventoryDbContext context)
        {
            _context = context;
        }

        public async Task<List<Medicamento>> GetMedicamentos()
        {
            return await _context.Medicamentos.Include(m => m.Proveedor).ToListAsync();
        }

        public async Task<Medicamento?> GetMedicamentoById(int id)
        {
            return await _context.Medicamentos.FirstOrDefaultAsync(m => m.medicamento_id == id);
        }

        public async Task AddMedicamento(Medicamento medicamento)
        {
            try
            {
                var proveedorExistente = await _context.Proveedores.FindAsync(medicamento.proveedor_id);
                if (proveedorExistente == null)
                {
                    throw new InvalidOperationException($"El proveedor con ID {medicamento.proveedor_id} no existe.");
                }

                await _context.Medicamentos.AddAsync(medicamento);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en AddMedicamento: {ex.Message}");
                throw;
            }
        }

        public async Task UpdateMedicamento(Medicamento medicamento)
        {
            try
            {
                var medicamentoExistente = await _context.Medicamentos.FindAsync(medicamento.medicamento_id);
                if (medicamentoExistente == null)
                {
                    throw new InvalidOperationException($"El medicamento con ID {medicamento.medicamento_id} no existe.");
                }
                medicamentoExistente.nombre = medicamento.nombre;
                medicamentoExistente.descripcion = medicamento.descripcion;
                medicamentoExistente.stock_actual = medicamento.stock_actual;
                medicamentoExistente.stock_minimo = medicamento.stock_minimo;
                _context.Medicamentos.Update(medicamentoExistente);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en UpdateMedicamento: {ex.Message}");
                throw;
            }
        }

        public async Task DeleteMedicamento(int medicamento_Id)
        {
            try
            {
                var medicamento = await _context.Medicamentos.FindAsync(medicamento_Id);
                if (medicamento != null)
                {
                    _context.Medicamentos.Remove(medicamento);
                    await _context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en DeleteMedicamento: {ex.Message}");
                throw;
            }
        }
    }
}