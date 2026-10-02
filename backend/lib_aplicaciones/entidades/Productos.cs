using System.ComponentModel.DataAnnotations.Schema;

namespace lib_aplicaciones.entidades
{
    public class Productos
    {
        public int Id { get; set; }
        public string? Codigo { get; set; }
        public string? Nombre { get; set; }
        public string? Descripcion { get; set; }
        public decimal Precio { get; set; }
        public bool Estado { get; set; }
        public int Categoria { get; set; }
        public int Marca { get; set; }
        public int Material { get; set; }

        [ForeignKey("Categoria")] public Categorias? _Categoria { get; set; }
        [ForeignKey("Marca")] public Marcas? _Marca { get; set; }
        [ForeignKey("Material")] public Materiales? _Material { get; set; }
        public List<Variantes>? Variantes { get; set; }
    }
}
