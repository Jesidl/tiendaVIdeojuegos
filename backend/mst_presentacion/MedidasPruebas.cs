using lib_aplicaciones.entidades;
using lib_aplicaciones.implementaciones;
using lib_aplicaciones.interfaces;
using lib_aplicaciones.nucleo;
using Microsoft.EntityFrameworkCore;

namespace pruebas_unitarias
{
    [TestClass]
    public class MedidasPruebas
    {
        private IConexion conexion;
        private Medidas? entidad = null;

        public MedidasPruebas()
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
            this.entidad = new Medidas()
            {
                Cliente = 1,
                Cintura = 74.00m,
                Cadera = 100.00m,
                Busto = 92.00m,
                Fecha = DateTime.Now,
            };
            this.conexion.Medidas!.Add(this.entidad!);
            this.conexion.SaveChanges();
        }

        public void Consultar()
        {
            var lista_medidas = this.conexion.Medidas!.ToList();
            if (lista_medidas.Count <= 0)
                throw new Exception("Lista vacia");
        }

        private void Actualizar()
        {
            this.entidad!.Cintura = 72.00m;

            var entry = this.conexion!.Entry<Medidas>(this.entidad);
            entry.State = EntityState.Modified;
            this.conexion!.SaveChanges();
        }

        private void Borrar()
        {
            this.conexion.Medidas!.Remove(this.entidad!);
            this.conexion.SaveChanges();
        }
    }
}
