using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Sistema_inventario_suministro.Models
{
    public class Medicamento
    {
        [Key]
        public int medicamento_id { get; set; }
        public string? nombre { get; set; }
        public string? descripcion { get; set; }
        public int stock_actual { get; set; }
        public int stock_minimo { get; set; }
        public int proveedor_id { get; set; }
        [JsonIgnore]    
        public Proveedor? Proveedor { get; set; }
    }
}
