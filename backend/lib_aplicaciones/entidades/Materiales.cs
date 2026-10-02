namespace lib_aplicaciones.entidades
{
    public class Materiales
    {
        public int Id { get; set; }
        public string? Nombre { get; set; }
        public string? Descripcion { get; set; }

        public List<Productos>? Productos { get; set; }
    }
}
