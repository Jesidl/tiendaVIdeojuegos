namespace lib_aplicaciones.entidades
{
    public class Sucursales
    {
        public int Id { get; set; }
        public string? Nombre { get; set; }
        public string? Direccion { get; set; }
        public string? Ciudad { get; set; }
        public string? Telefono { get; set; }
        public bool Estado { get; set; }

        public List<Empleados>? Empleados { get; set; }
        public List<Inventarios>? Inventarios { get; set; }
    }
}
