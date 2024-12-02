using System;
using System.ComponentModel.DataAnnotations;

namespace Sistema_inventario_suministro.Models
{
    public class Usuario
    {
        public int Id { get; set; }

        [Required]
        public string? Nombre { get; set; }

        [Required]
        public string? Clave { get; set; }

        [Required]
        [EnumDataType(typeof(Estado))]
        public Estado Estado { get; set; }
    }

    public enum Estado
    {
        Activo,
        Inactivo
    }

}
