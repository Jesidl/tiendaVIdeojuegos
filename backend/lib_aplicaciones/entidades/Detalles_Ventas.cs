using System.ComponentModel.DataAnnotations.Schema;

namespace lib_aplicaciones.entidades
{
    public class Detalles_Ventas
    {
        public int Id { get; set; }
        public int Venta { get; set; }
        public int Variante { get; set; }
        public int Cantidad { get; set; }
        public decimal Precio_Unitario { get; set; }
        public decimal Subtotal { get; set; }

        [ForeignKey("Venta")] public Ventas? _Venta { get; set; }
        [ForeignKey("Variante")] public Variantes? _Variante { get; set; }
    }
}
