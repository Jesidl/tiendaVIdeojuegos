using lib_aplicaciones.entidades;
using lib_aplicaciones.implementaciones;
using lib_aplicaciones.interfaces;
using lib_aplicaciones.nucleo;
using Microsoft.EntityFrameworkCore;

namespace pruebas_unitarias
{
    [TestClass]
    public class Detalles_PedidosPruebas
    {
        private IConexion conexion;
        private Detalles_Pedidos? entidad = null;

        public Detalles_PedidosPruebas()
        {
            this.conexion = new Conexion();
            this.conexion.StringConexion = Datosgenerales.ObtenerStringConexion();
        }

        [TestMethod]
        public void Execute()
        {
            Insertar();
            Consultar();
            Actualizar();
            Borrar();
        }

        public void Insertar()
        {
            this.entidad = new Detalles_Pedidos()
            {
                Pedido = 1,
                Variante = 1,
                Cantidad = 5,
                Precio_Compra = 180000.00m,
                Subtotal = 900000.00m,
            };
            this.conexion.Detalles_Pedidos!.Add(this.entidad!);
            this.conexion.SaveChanges();
        }

        public void Consultar()
        {
            var lista_detalles_pedidos = this.conexion.Detalles_Pedidos!.ToList();
            if (lista_detalles_pedidos.Count <= 0)
                throw new Exception("Lista vacia");
        }

        private void Actualizar()
        {
            this.entidad!.Cantidad = 6;

            var entry = this.conexion!.Entry<Detalles_Pedidos>(this.entidad);
            entry.State = EntityState.Modified;
            this.conexion!.SaveChanges();
        }

        private void Borrar()
        {
            this.conexion.Detalles_Pedidos!.Remove(this.entidad!);
            this.conexion.SaveChanges();
        }
    }
}
