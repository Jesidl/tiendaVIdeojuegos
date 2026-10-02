using System.ComponentModel.DataAnnotations.Schema;

namespace lib_aplicaciones.entidades
{
    public class Pagos
    {
        public int Id { get; set; }
        public int Venta { get; set; }
        public int Metodo_Pago { get; set; }
        public decimal Valor { get; set; }
        public DateTime Fecha { get; set; }

        [ForeignKey("Venta")] public Ventas? _Venta { get; set; }
        [ForeignKey("Metodo_Pago")] public Metodos_Pago? _Metodo_Pago { get; set; }
    }
}
