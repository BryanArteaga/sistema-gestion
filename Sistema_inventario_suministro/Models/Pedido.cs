using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Sistema_inventario_suministro.Models
{
    public class Pedido
    {
        [Key]
        public int pedido_id { get; set; }
        public int proveedor_id { get; set; }
        public DateTime fecha_pedido { get; set; }
        public DateTime fecha_entrega { get; set; }
        public string? estado { get; set; }
        
        [JsonIgnore]
        public Proveedor? Proveedor { get; set; }
    }
}
