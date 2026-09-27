using System.ComponentModel.DataAnnotations.Schema;

namespace lib_aplicaciones.entidades
{
    public class Empleados
    {
        public int Id { get; set; }
        public string? Nombre { get; set; }
        public string? Cedula { get; set; }
        public string? Direccion { get; set; }
        public int Sucursal { get; set; }
        public int Cargo { get; set; }

        [ForeignKey("Sucursal")] public Sucursales? _Sucursal { get; set; }
        [ForeignKey("Cargo")] public Cargos? _Cargo { get; set; }
    }
}
