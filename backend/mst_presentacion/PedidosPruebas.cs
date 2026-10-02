using lib_aplicaciones.entidades;
using lib_aplicaciones.implementaciones;
using lib_aplicaciones.interfaces;
using lib_aplicaciones.nucleo;
using Microsoft.EntityFrameworkCore;

namespace pruebas_unitarias
{
    [TestClass]
    public class PedidosPruebas
    {
        private IConexion conexion;
        private Pedidos? entidad = null;

        public PedidosPruebas()
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
            this.entidad = new Pedidos()
            {
                Codigo = "P-PRB",
                Proveedor = 1,
                Empleado = 1,
                Fecha = DateTime.Now,
                Estado = "Pendiente",
                Total = 900000.00m,
            };
            this.conexion.Pedidos!.Add(this.entidad!);
            this.conexion.SaveChanges();
        }

        public void Consultar()
        {
            var lista_pedidos = this.conexion.Pedidos!.ToList();
            if (lista_pedidos.Count <= 0)
                throw new Exception("Lista vacia");
        }

        private void Actualizar()
        {
            this.entidad!.Estado = "Recibido";

            var entry = this.conexion!.Entry<Pedidos>(this.entidad);
            entry.State = EntityState.Modified;
            this.conexion!.SaveChanges();
        }

        private void Borrar()
        {
            this.conexion.Pedidos!.Remove(this.entidad!);
            this.conexion.SaveChanges();
        }
    }
}
