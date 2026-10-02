using System.ComponentModel.DataAnnotations.Schema;

namespace lib_aplicaciones.entidades
{
    public class Pedidos
    {
        public int Id { get; set; }
        public string? Codigo { get; set; }
        public int Proveedor { get; set; }
        public int Empleado { get; set; }
        public DateTime Fecha { get; set; }
        public string? Estado { get; set; }
        public decimal Total { get; set; }

        [ForeignKey("Proveedor")] public Proveedores? _Proveedor { get; set; }
        [ForeignKey("Empleado")] public Empleados? _Empleado { get; set; }
        public List<Detalles_Pedidos>? Detalles_Pedidos { get; set; }
    }
}
