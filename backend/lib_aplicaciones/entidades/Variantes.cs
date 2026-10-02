using System.ComponentModel.DataAnnotations.Schema;

namespace lib_aplicaciones.entidades
{
    public class Variantes
    {
        public int Id { get; set; }
        public string? Sku { get; set; }
        public int Producto { get; set; }
        public int Talla { get; set; }
        public int Color { get; set; }
        public decimal Precio_Adicional { get; set; }

        [ForeignKey("Producto")] public Productos? _Producto { get; set; }
        [ForeignKey("Talla")] public Tallas? _Talla { get; set; }
        [ForeignKey("Color")] public Colores? _Color { get; set; }
        public List<Inventarios>? Inventarios { get; set; }
        public List<Detalles_Ventas>? Detalles_Ventas { get; set; }
        public List<Detalles_Pedidos>? Detalles_Pedidos { get; set; }
    }
}
