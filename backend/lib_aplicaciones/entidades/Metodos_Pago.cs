namespace lib_aplicaciones.entidades
{
    public class Metodos_Pago
    {
        public int Id { get; set; }
        public string? Nombre { get; set; }
        public string? Descripcion { get; set; }
        public bool Estado { get; set; }

        public List<Pagos>? Pagos { get; set; }
    }
}
