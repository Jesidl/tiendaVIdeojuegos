using lib_aplicaciones.entidades;
using lib_aplicaciones.implementaciones;
using lib_aplicaciones.interfaces;
using lib_aplicaciones.nucleo;
using Microsoft.EntityFrameworkCore;

namespace pruebas_unitarias
{
    [TestClass]
    public class VariantesPruebas
    {
        private IConexion conexion;
        private Variantes? entidad = null;

        public VariantesPruebas()
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
            this.entidad = new Variantes()
            {
                Sku = "FAJ-PRB-SKU",
                Producto = 1,
                Talla = 1,
                Color = 1,
                Precio_Adicional = 10000.00m,
            };
            this.conexion.Variantes!.Add(this.entidad!);
            this.conexion.SaveChanges();
        }

        public void Consultar()
        {
            var lista_variantes = this.conexion.Variantes!.ToList();
            if (lista_variantes.Count <= 0)
                throw new Exception("Lista vacia");
        }

        private void Actualizar()
        {
            this.entidad!.Precio_Adicional = 15000.00m;

            var entry = this.conexion!.Entry<Variantes>(this.entidad);
            entry.State = EntityState.Modified;
            this.conexion!.SaveChanges();
        }

        private void Borrar()
        {
            this.conexion.Variantes!.Remove(this.entidad!);
            this.conexion.SaveChanges();
        }
    }
}
