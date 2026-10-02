using lib_aplicaciones.entidades;
using lib_aplicaciones.implementaciones;
using lib_aplicaciones.interfaces;
using lib_aplicaciones.nucleo;
using Microsoft.EntityFrameworkCore;

namespace pruebas_unitarias
{
    [TestClass]
    public class SucursalesPruebas
    {
        private IConexion conexion;
        private Sucursales? entidad = null;

        public SucursalesPruebas()
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
            this.entidad = new Sucursales()
            {
                Nombre = "Sucursal Envigado",
                Direccion = "Carrera 43A # 38 Sur-15",
                Ciudad = "Envigado",
                Telefono = "6043334455",
                Estado = true,
            };
            this.conexion.Sucursales!.Add(this.entidad!);
            this.conexion.SaveChanges();
        }

        public void Consultar()
        {
            var lista_sucursales = this.conexion.Sucursales!.ToList();
            if (lista_sucursales.Count <= 0)
                throw new Exception("Lista vacia");
        }

        private void Actualizar()
        {
            this.entidad!.Estado = false;

            var entry = this.conexion!.Entry<Sucursales>(this.entidad);
            entry.State = EntityState.Modified;
            this.conexion!.SaveChanges();
        }

        private void Borrar()
        {
            this.conexion.Sucursales!.Remove(this.entidad!);
            this.conexion.SaveChanges();
        }
    }
}
