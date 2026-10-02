using lib_aplicaciones.entidades;
using lib_aplicaciones.implementaciones;
using lib_aplicaciones.interfaces;
using lib_aplicaciones.nucleo;
using Microsoft.EntityFrameworkCore;

namespace pruebas_unitarias
{
    [TestClass]
    public class Metodos_PagoPruebas
    {
        private IConexion conexion;
        private Metodos_Pago? entidad = null;

        public Metodos_PagoPruebas()
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
            this.entidad = new Metodos_Pago()
            {
                Nombre = "Nequi",
                Descripcion = "Transferencia por billetera digital",
                Estado = true,
            };
            this.conexion.Metodos_Pago!.Add(this.entidad!);
            this.conexion.SaveChanges();
        }

        public void Consultar()
        {
            var lista_metodos_pago = this.conexion.Metodos_Pago!.ToList();
            if (lista_metodos_pago.Count <= 0)
                throw new Exception("Lista vacia");
        }

        private void Actualizar()
        {
            this.entidad!.Estado = false;

            var entry = this.conexion!.Entry<Metodos_Pago>(this.entidad);
            entry.State = EntityState.Modified;
            this.conexion!.SaveChanges();
        }

        private void Borrar()
        {
            this.conexion.Metodos_Pago!.Remove(this.entidad!);
            this.conexion.SaveChanges();
        }
    }
}
