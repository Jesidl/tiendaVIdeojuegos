using lib_aplicaciones.implementaciones;
using lib_aplicaciones.interfaces;
using lib_aplicaciones.nucleo;
using Microsoft.EntityFrameworkCore;

try
{
    IConexion conexion = new Conexion();
    conexion.StringConexion = Datosgenerales.ObtenerStringConexion();
    var lista_cargos = conexion.Cargos!.ToList();
    var lista_sucursales = conexion.Sucursales!.ToList();
    var lista_empleados = conexion.Empleados!
        .Include(x => x._Sucursal)
        .Include(x => x._Cargo)
        .ToList();
}
catch (Exception ex)
{
    Console.WriteLine(ex.ToString());
}

Console.WriteLine("csl_presentacion");
