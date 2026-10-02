namespace lib_aplicaciones.entidades
{
    public class Marcas
    {
        public int Id { get; set; }
        public string? Nombre { get; set; }
        public string? Pais { get; set; }
        public bool Estado { get; set; }

        public List<Productos>? Productos { get; set; }
    }
}
