namespace lib_aplicaciones.entidades
{
    public class Tallas
    {
        public int Id { get; set; }
        public string? Nombre { get; set; }
        public string? Descripcion { get; set; }

        public List<Variantes>? Variantes { get; set; }
    }
}
