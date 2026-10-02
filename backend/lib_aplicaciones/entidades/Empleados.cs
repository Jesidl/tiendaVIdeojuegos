using System.ComponentModel.DataAnnotations.Schema;

namespace lib_aplicaciones.entidades
{
    public class Empleados
    {
        public int Id { get; set; }
        public string? Cedula { get; set; }
        public string? Nombre { get; set; }
        public string? Telefono { get; set; }
        public int Sucursal { get; set; }
        public int Cargo { get; set; }

        [ForeignKey("Sucursal")] public Sucursales? _Sucursal { get; set; }
        [ForeignKey("Cargo")] public Cargos? _Cargo { get; set; }
        public List<Ventas>? Ventas { get; set; }
        public List<Pedidos>? Pedidos { get; set; }
    }
}
