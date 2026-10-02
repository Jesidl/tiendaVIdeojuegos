using System.ComponentModel.DataAnnotations.Schema;

namespace lib_aplicaciones.entidades
{
    public class Ventas
    {
        public int Id { get; set; }
        public string? Codigo { get; set; }
        public int Cliente { get; set; }
        public int Empleado { get; set; }
        public DateTime Fecha { get; set; }
        public decimal Total { get; set; }

        [ForeignKey("Cliente")] public Clientes? _Cliente { get; set; }
        [ForeignKey("Empleado")] public Empleados? _Empleado { get; set; }
        public List<Detalles_Ventas>? Detalles_Ventas { get; set; }
        public List<Pagos>? Pagos { get; set; }
    }
}
