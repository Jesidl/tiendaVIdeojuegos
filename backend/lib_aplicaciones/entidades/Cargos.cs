namespace lib_aplicaciones.entidades
{
    public class Cargos
    {
        public int Id { get; set; }
        public string? Nombre { get; set; }
        public string? Descripcion { get; set; }
        public decimal Salario { get; set; }

        public List<Empleados>? Empleados { get; set; }
    }
}
