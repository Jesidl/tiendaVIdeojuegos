namespace lib_aplicaciones.entidades
{
    public class Clientes
    {
        public int Id { get; set; }
        public string? Cedula { get; set; }
        public string? Nombre { get; set; }
        public string? Telefono { get; set; }
        public string? Correo { get; set; }
        public string? Direccion { get; set; }

        public List<Medidas>? Medidas { get; set; }
        public List<Ventas>? Ventas { get; set; }
    }
}
