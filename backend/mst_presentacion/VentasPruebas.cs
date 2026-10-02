using lib_aplicaciones.entidades;
using lib_aplicaciones.implementaciones;
using lib_aplicaciones.interfaces;
using lib_aplicaciones.nucleo;
using Microsoft.EntityFrameworkCore;

namespace pruebas_unitarias
{
    [TestClass]
    public class VentasPruebas
    {
        private IConexion conexion;
        private Ventas? entidad = null;

        public VentasPruebas()
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
            this.entidad = new Ventas()
            {
                Codigo = "V-PRB",
                Cliente = 1,
                Empleado = 1,
                Fecha = DateTime.Now,
                Total = 150000.00m,
            };
            this.conexion.Ventas!.Add(this.entidad!);
            this.conexion.SaveChanges();
        }

        public void Consultar()
        {
            var lista_ventas = this.conexion.Ventas!.ToList();
            if (lista_ventas.Count <= 0)
                throw new Exception("Lista vacia");
        }

        private void Actualizar()
        {
            this.entidad!.Total = 160000.00m;

            var entry = this.conexion!.Entry<Ventas>(this.entidad);
            entry.State = EntityState.Modified;
            this.conexion!.SaveChanges();
        }

        private void Borrar()
        {
            this.conexion.Ventas!.Remove(this.entidad!);
            this.conexion.SaveChanges();
        }
    }
}
