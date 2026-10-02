namespace lib_aplicaciones.entidades
{
    public class Colores
    {
        public int Id { get; set; }
        public string? Nombre { get; set; }
        public string? Codigo_Hex { get; set; }

        public List<Variantes>? Variantes { get; set; }
    }
}
