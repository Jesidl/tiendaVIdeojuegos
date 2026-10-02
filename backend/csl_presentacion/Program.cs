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
    var lista_clientes = conexion.Clientes!.ToList();
    var lista_proveedores = conexion.Proveedores!.ToList();
    var lista_categorias = conexion.Categorias!.ToList();
    var lista_marcas = conexion.Marcas!.ToList();
    var lista_materiales = conexion.Materiales!.ToList();
    var lista_tallas = conexion.Tallas!.ToList();
    var lista_colores = conexion.Colores!.ToList();
    var lista_metodos_pago = conexion.Metodos_Pago!.ToList();
    var lista_empleados = conexion.Empleados!
        .Include(x => x._Sucursal)
        .Include(x => x._Cargo)
        .ToList();
    var lista_medidas = conexion.Medidas!
        .Include(x => x._Cliente)
        .ToList();
    var lista_productos = conexion.Productos!
        .Include(x => x._Categoria)
        .Include(x => x._Marca)
        .Include(x => x._Material)
        .ToList();
    var lista_variantes = conexion.Variantes!
        .Include(x => x._Producto)
        .Include(x => x._Talla)
        .Include(x => x._Color)
        .ToList();
    var lista_inventarios = conexion.Inventarios!
        .Include(x => x._Variante)
        .Include(x => x._Sucursal)
        .ToList();
    var lista_ventas = conexion.Ventas!
        .Include(x => x._Cliente)
        .Include(x => x._Empleado)
        .ToList();
    var lista_detalles_ventas = conexion.Detalles_Ventas!
        .Include(x => x._Venta)
        .Include(x => x._Variante)
        .ToList();
    var lista_pagos = conexion.Pagos!
        .Include(x => x._Venta)
        .Include(x => x._Metodo_Pago)
        .ToList();
    var lista_pedidos = conexion.Pedidos!
        .Include(x => x._Proveedor)
        .Include(x => x._Empleado)
        .ToList();
    var lista_detalles_pedidos = conexion.Detalles_Pedidos!
        .Include(x => x._Pedido)
        .Include(x => x._Variante)
        .ToList();
}
catch (Exception ex)
{
    Console.WriteLine(ex.ToString());
}

Console.WriteLine("csl_presentacion");
