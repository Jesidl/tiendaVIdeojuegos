using lib_aplicaciones.entidades;
using lib_aplicaciones.implementaciones;
using lib_aplicaciones.interfaces;
using lib_aplicaciones.nucleo;
using Microsoft.EntityFrameworkCore;

namespace pruebas_unitarias
{
    [TestClass]
    public class PlataformasPruebas
    {
        private IConexion conexion;
        private Plataformas? entidad = null;

        public PlataformasPruebas()
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
            this.entidad = new Plataformas()
            {
                Nombre = "PlayStation 5",
                Fabricante = "Sony",
                Tipo = "Consola",
                Estado = true,
            };
            this.conexion.Plataformas!.Add(this.entidad!);
            this.conexion.SaveChanges();
        }

        public void Consultar()
        {
            var lista_plataformas = this.conexion.Plataformas!.ToList();
            if (lista_plataformas.Count <= 0)
                throw new Exception("Lista vacia");
        }

        private void Actualizar()
        {
            this.entidad!.Tipo = "Consola de sobremesa";

            var entry = this.conexion!.Entry<Plataformas>(this.entidad);
            entry.State = EntityState.Modified;
            this.conexion!.SaveChanges();
        }

        private void Borrar()
        {
            this.conexion.Plataformas!.Remove(this.entidad!);
            this.conexion.SaveChanges();
        }
    }
}
