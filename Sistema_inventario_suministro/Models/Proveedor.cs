using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Sistema_inventario_suministro.Models
{
    public class Proveedor
    {
        [Key]
        public int proveedor_id { get; set; }
        public string? nombre { get; set; }
        public string? contacto { get; set; }
        public string? telefono { get; set; }
        [JsonIgnore] 
        public ICollection<Medicamento>? Medicamentos { get; set; }
        [JsonIgnore]
        public ICollection<Pedido>? Pedidos { get; set; }
    }
}
