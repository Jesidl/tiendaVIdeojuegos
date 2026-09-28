namespace lib_aplicaciones.entidades
{
    public class Categorias
    {
        public int Id { get; set; }
        public string? Nombre { get; set; }
        public string? Descripcion { get; set; }
        public bool Estado { get; set; }
        public DateTime Fecha_Creacion { get; set; }
    }
}
