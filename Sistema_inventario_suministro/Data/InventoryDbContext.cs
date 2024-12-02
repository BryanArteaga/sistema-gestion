using Microsoft.EntityFrameworkCore;
using Sistema_inventario_suministro.Models;

namespace Sistema_inventario_suministro.Data
{
    public class InventoryDbContext : DbContext
    {
        public InventoryDbContext(DbContextOptions<InventoryDbContext> options) : base(options)
        {
        }

        public DbSet<Proveedor>? Proveedores { get; set; }
        public DbSet<Medicamento>? Medicamentos { get; set; }
        public DbSet<Pedido>? Pedidos { get; set; }
        public DbSet<Usuario>? Usuarios { get; set; } // Esto debe coincidir con la tabla en la BD

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Proveedor>()
                .HasMany(p => p.Medicamentos)
                .WithOne(m => m.Proveedor)
                .HasForeignKey(m => m.proveedor_id);

            modelBuilder.Entity<Pedido>()
                .HasOne(p => p.Proveedor)
                .WithMany(p => p.Pedidos)
                .HasForeignKey(p => p.proveedor_id);
            
            modelBuilder.Entity<Usuario>().ToTable("usuarios")
                .Property(u => u.Estado)
                .HasConversion(
                    v => v.ToString(), // Convertimos el ENUM a string
                    v => (Estado)Enum.Parse(typeof(Estado), v, true)); // Convertimos de string a ENUM
        }
    }
}
