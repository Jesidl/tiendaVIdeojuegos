namespace lib_aplicaciones.nucleo
{
    public class Datosgenerales
    {
        public static string ObtenerStringConexion()
        {
            return "server=localhost\\SQLEXPRESS;database=db_tienda_videojuegos;Integrated Security=True;TrustServerCertificate=true;";
        }
    }
}
