namespace lib_aplicaciones.entidades
{
    public class Proveedores
    {
        public int Id { get; set; }
        public string? Nit { get; set; }
        public string? Nombre_Empresa { get; set; }
        public string? Telefono { get; set; }
        public string? Correo { get; set; }

        public List<Pedidos>? Pedidos { get; set; }
    }
}
