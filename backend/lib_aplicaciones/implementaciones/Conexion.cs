using lib_aplicaciones.entidades;
using lib_aplicaciones.interfaces;
using Microsoft.EntityFrameworkCore;

namespace lib_aplicaciones.implementaciones
{
    public class Conexion : DbContext, IConexion
    {
        public string? StringConexion { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(this.StringConexion!, p => { });
            optionsBuilder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
        }

        public DbSet<Cargos>? Cargos { get; set; }
        public DbSet<Sucursales>? Sucursales { get; set; }
        public DbSet<Clientes>? Clientes { get; set; }
        public DbSet<Proveedores>? Proveedores { get; set; }
        public DbSet<Categorias>? Categorias { get; set; }
        public DbSet<Marcas>? Marcas { get; set; }
        public DbSet<Materiales>? Materiales { get; set; }
        public DbSet<Tallas>? Tallas { get; set; }
        public DbSet<Colores>? Colores { get; set; }
        public DbSet<Metodos_Pago>? Metodos_Pago { get; set; }
        public DbSet<Empleados>? Empleados { get; set; }
        public DbSet<Medidas>? Medidas { get; set; }
        public DbSet<Productos>? Productos { get; set; }
        public DbSet<Variantes>? Variantes { get; set; }
        public DbSet<Inventarios>? Inventarios { get; set; }
        public DbSet<Ventas>? Ventas { get; set; }
        public DbSet<Detalles_Ventas>? Detalles_Ventas { get; set; }
        public DbSet<Pagos>? Pagos { get; set; }
        public DbSet<Pedidos>? Pedidos { get; set; }
        public DbSet<Detalles_Pedidos>? Detalles_Pedidos { get; set; }
    }
}
