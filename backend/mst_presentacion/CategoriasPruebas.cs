using lib_aplicaciones.entidades;
using lib_aplicaciones.implementaciones;
using lib_aplicaciones.interfaces;
using lib_aplicaciones.nucleo;
using Microsoft.EntityFrameworkCore;

namespace pruebas_unitarias
{
    [TestClass]
    public class CategoriasPruebas
    {
        private IConexion conexion;
        private Categorias? entidad = null;

        public CategoriasPruebas()
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
            this.entidad = new Categorias()
            {
                Nombre = "Deportes",
                Descripcion = "Juegos de futbol, baloncesto y carreras",
                Estado = true,
                Fecha_Creacion = DateTime.Now,
            };
            this.conexion.Categorias!.Add(this.entidad!);
            this.conexion.SaveChanges();
        }

        public void Consultar()
        {
            var lista_categorias = this.conexion.Categorias!.ToList();
            if (lista_categorias.Count <= 0)
                throw new Exception("Lista vacia");
        }

        private void Actualizar()
        {
            this.entidad!.Estado = false;

            var entry = this.conexion!.Entry<Categorias>(this.entidad);
            entry.State = EntityState.Modified;
            this.conexion!.SaveChanges();
        }

        private void Borrar()
        {
            this.conexion.Categorias!.Remove(this.entidad!);
            this.conexion.SaveChanges();
        }
    }
}
