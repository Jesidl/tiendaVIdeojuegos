using lib_aplicaciones.entidades;
using lib_aplicaciones.implementaciones;
using lib_aplicaciones.interfaces;
using lib_aplicaciones.nucleo;
using Microsoft.EntityFrameworkCore;

namespace pruebas_unitarias
{
    [TestClass]
    public class Detalles_VentasPruebas
    {
        private IConexion conexion;
        private Detalles_Ventas? entidad = null;

        public Detalles_VentasPruebas()
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
            this.entidad = new Detalles_Ventas()
            {
                Venta = 1,
                Variante = 1,
                Cantidad = 2,
                Precio_Unitario = 280000.00m,
                Subtotal = 560000.00m,
            };
            this.conexion.Detalles_Ventas!.Add(this.entidad!);
            this.conexion.SaveChanges();
        }

        public void Consultar()
        {
            var lista_detalles_ventas = this.conexion.Detalles_Ventas!.ToList();
            if (lista_detalles_ventas.Count <= 0)
                throw new Exception("Lista vacia");
        }

        private void Actualizar()
        {
            this.entidad!.Cantidad = 3;

            var entry = this.conexion!.Entry<Detalles_Ventas>(this.entidad);
            entry.State = EntityState.Modified;
            this.conexion!.SaveChanges();
        }

        private void Borrar()
        {
            this.conexion.Detalles_Ventas!.Remove(this.entidad!);
            this.conexion.SaveChanges();
        }
    }
}
