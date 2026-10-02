using lib_aplicaciones.entidades;
using lib_aplicaciones.implementaciones;
using lib_aplicaciones.interfaces;
using lib_aplicaciones.nucleo;
using Microsoft.EntityFrameworkCore;

namespace pruebas_unitarias
{
    [TestClass]
    public class MaterialesPruebas
    {
        private IConexion conexion;
        private Materiales? entidad = null;

        public MaterialesPruebas()
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
            this.entidad = new Materiales()
            {
                Nombre = "Latex",
                Descripcion = "Caucho natural para efecto termico",
            };
            this.conexion.Materiales!.Add(this.entidad!);
            this.conexion.SaveChanges();
        }

        public void Consultar()
        {
            var lista_materiales = this.conexion.Materiales!.ToList();
            if (lista_materiales.Count <= 0)
                throw new Exception("Lista vacia");
        }

        private void Actualizar()
        {
            this.entidad!.Descripcion = "Caucho natural de alta compresion";

            var entry = this.conexion!.Entry<Materiales>(this.entidad);
            entry.State = EntityState.Modified;
            this.conexion!.SaveChanges();
        }

        private void Borrar()
        {
            this.conexion.Materiales!.Remove(this.entidad!);
            this.conexion.SaveChanges();
        }
    }
}
