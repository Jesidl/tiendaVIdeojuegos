using lib_aplicaciones.entidades;
using lib_aplicaciones.implementaciones;
using lib_aplicaciones.interfaces;
using lib_aplicaciones.nucleo;
using Microsoft.EntityFrameworkCore;

namespace pruebas_unitarias
{
    [TestClass]
    public class TallasPruebas
    {
        private IConexion conexion;
        private Tallas? entidad = null;

        public TallasPruebas()
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
            this.entidad = new Tallas()
            {
                Nombre = "XXL",
                Descripcion = "Cintura de 94 a 102 cm",
            };
            this.conexion.Tallas!.Add(this.entidad!);
            this.conexion.SaveChanges();
        }

        public void Consultar()
        {
            var lista_tallas = this.conexion.Tallas!.ToList();
            if (lista_tallas.Count <= 0)
                throw new Exception("Lista vacia");
        }

        private void Actualizar()
        {
            this.entidad!.Descripcion = "Cintura de 94 a 104 cm";

            var entry = this.conexion!.Entry<Tallas>(this.entidad);
            entry.State = EntityState.Modified;
            this.conexion!.SaveChanges();
        }

        private void Borrar()
        {
            this.conexion.Tallas!.Remove(this.entidad!);
            this.conexion.SaveChanges();
        }
    }
}
