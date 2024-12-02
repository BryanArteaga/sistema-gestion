using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Sistema_inventario_suministro.Models;
using Sistema_inventario_suministro.Service;
using System;

[ApiController]
[Route("[controller]")]
public class ControllerMedicamentos : ControllerBase
{
    private readonly MedicamentoService _service;

    public ControllerMedicamentos(MedicamentoService service)
    {
        _service = service;
    }

    [Authorize]
    [HttpGet]
    public async Task<ActionResult<List<Medicamento>>> GetMedicamentos()
    {
        try
        {
            return await _service.ObtenerMedicamentos();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error en GetMedicamentos: {ex.Message}");
            return StatusCode(StatusCodes.Status500InternalServerError, $"Error en servidor: {ex.Message}");
        }
    }

    [Authorize]
    [HttpGet("{id}")]
    public async Task<ActionResult<Medicamento?>> GetMedicamentoById(int id)
    {
        try
        {
            return await _service.ObtenerMedicamentoPorId(id);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error en GetMedicamentoById: {ex.Message}");
            return StatusCode(StatusCodes.Status500InternalServerError, $"Error en servidor: {ex.Message}");
        }
    }

    [Authorize]
    [HttpPost]
    public async Task<ActionResult<Medicamento>> AddMedicamento(Medicamento medicamento)
    {
        try
        {
            var result = await _service.CrearMedicamento(medicamento);
            return CreatedAtAction(nameof(GetMedicamentoById), new { id = result.medicamento_id }, result);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error en AddMedicamento: {ex.Message}");
            return StatusCode(StatusCodes.Status500InternalServerError, $"Error en servidor: {ex.Message}");
        }
    }

    [Authorize]
    [HttpPut("{id}")]
    public async Task<ActionResult<Medicamento>> UpdateMedicamento(int id, Medicamento medicamento)
    {
        try
        {
            medicamento.medicamento_id = id;
            var result = await _service.EditarMedicamento(medicamento);
            return Ok(result);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error en UpdateMedicamento: {ex.Message}");
            return StatusCode(StatusCodes.Status500InternalServerError, $"Error en servidor: {ex.Message}");
        }
    }

    [Authorize]
    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteMedicamento(int id)
    {
        try
        {
            await _service.EliminarMedicamento(id);
            return NoContent();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error en DeleteMedicamento: {ex.Message}");
            return StatusCode(StatusCodes.Status500InternalServerError, $"Error en servidor: {ex.Message}");
        }
    }
}
