using lib_aplicaciones.entidades;
using lib_aplicaciones.implementaciones;
using lib_aplicaciones.interfaces;
using lib_aplicaciones.nucleo;
using Microsoft.EntityFrameworkCore;

namespace pruebas_unitarias
{
    [TestClass]
    public class ColoresPruebas
    {
        private IConexion conexion;
        private Colores? entidad = null;

        public ColoresPruebas()
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
            this.entidad = new Colores()
            {
                Nombre = "Negro",
                Codigo_Hex = "#000000",
            };
            this.conexion.Colores!.Add(this.entidad!);
            this.conexion.SaveChanges();
        }

        public void Consultar()
        {
            var lista_colores = this.conexion.Colores!.ToList();
            if (lista_colores.Count <= 0)
                throw new Exception("Lista vacia");
        }

        private void Actualizar()
        {
            this.entidad!.Codigo_Hex = "#111111";

            var entry = this.conexion!.Entry<Colores>(this.entidad);
            entry.State = EntityState.Modified;
            this.conexion!.SaveChanges();
        }

        private void Borrar()
        {
            this.conexion.Colores!.Remove(this.entidad!);
            this.conexion.SaveChanges();
        }
    }
}
