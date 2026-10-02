using System.ComponentModel.DataAnnotations.Schema;

namespace lib_aplicaciones.entidades
{
    public class Medidas
    {
        public int Id { get; set; }
        public int Cliente { get; set; }
        public decimal Cintura { get; set; }
        public decimal Cadera { get; set; }
        public decimal Busto { get; set; }
        public DateTime Fecha { get; set; }

        [ForeignKey("Cliente")] public Clientes? _Cliente { get; set; }
    }
}
