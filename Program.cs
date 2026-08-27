public class Empleados
{
    public int Id { get; set; }
    public string? Nombre { get; set; }
    public string? Cedula { get; set; }
    public string? Direccion { get; set; }
    public int Sucursal { get; set; }
    public int Cargo { get; set; }

    public Sucursales? _Sucursal { get; set; }
    public Cargos? _Cargo { get; set; }
    public List<Ventas> Ventas { get; set; } = new List<Ventas>();
    public List<Pedidos> Pedidos { get; set; } = new List<Pedidos>();
}

public class Cargos
{
    public int Id { get; set; }
    public string? Nombre_Cargo { get; set; }
    public string? Descripcion { get; set; }
    public decimal Salario { get; set; }

    public List<Empleados> Empleados { get; set; } = new List<Empleados>();
}

public class Clientes
{
    public int Id { get; set; }
    public string? Nombre { get; set; }
    public string? Direccion { get; set; }
    public string? Cedula { get; set; }
    public string? Ciudad { get; set; }

    public List<Ventas> Ventas { get; set; } = new List<Ventas>();
    public List<Resenas> Resenas { get; set; } = new List<Resenas>();
}

public class Ventas
{
    public int Id { get; set; }
    public int Cliente { get; set; }
    public int Empleado { get; set; }
    public DateTime Fecha_Venta { get; set; }
    public decimal Total { get; set; }

    public Clientes? _Cliente { get; set; }
    public Empleados? _Empleado { get; set; }
    public List<Detalle_Ventas> Detalle_Ventas { get; set; } = new List<Detalle_Ventas>();
    public List<Metodo_Pagos> Metodo_Pagos { get; set; } = new List<Metodo_Pagos>();
}

public class Detalle_Ventas
{
    public int Id { get; set; }
    public int Venta { get; set; }
    public decimal Cantidad { get; set; }
    public decimal Precio_Unitario { get; set; }
    public decimal Subtotal { get; set; }
    public int Videojuego { get; set; }

    public Ventas? _Venta { get; set; }
    public Videojuegos? _Videojuego { get; set; }
}

public class Metodo_Pagos
{
    public int Id { get; set; }
    public int Venta { get; set; }
    public string? Tipo_Pago { get; set; }
    public string? Descripcion { get; set; }

    public Ventas? _Venta { get; set; }
}

public class Inventarios
{
    public int Id { get; set; }
    public int Cantidad { get; set; }
    public DateTime Fecha_Actualizacion { get; set; }
    public int Stock_Minimo { get; set; }
    public int Sucursal { get; set; }

    public Sucursales? _Sucursal { get; set; }
    public List<Videojuegos> Videojuegos { get; set; } = new List<Videojuegos>();
}

public class Sucursales
{
    public int Id { get; set; }
    public string? Nombre { get; set; }
    public string? Direccion { get; set; }
    public string? Ciudad { get; set; }
    public string? Telefono { get; set; }
    public string? Estado { get; set; }

    public List<Empleados> Empleados { get; set; } = new List<Empleados>();
    public List<Inventarios> Inventarios { get; set; } = new List<Inventarios>();
}

public class Proveedores
{
    public int Id { get; set; }
    public string? Nit { get; set; }
    public string? Nombre_Empresa { get; set; }
    public string? Telefono { get; set; }
    public string? Correo { get; set; }

    public List<Pedidos> Pedidos { get; set; } = new List<Pedidos>();
}

public class Pedidos
{
    public int Id { get; set; }
    public int Proveedor { get; set; }
    public int Empleado { get; set; }
    public DateTime Fecha_Pedidos { get; set; }
    public string? Estado { get; set; }
    public decimal Total { get; set; }

    public Proveedores? _Proveedor { get; set; }
    public Empleados? _Empleado { get; set; }
    public List<Detalle_Pedidos> Detalle_Pedidos { get; set; } = new List<Detalle_Pedidos>();
}

public class Detalle_Pedidos
{
    public int Id { get; set; }
    public int Pedido { get; set; }
    public decimal Cantidad { get; set; }
    public decimal Precio_Compra { get; set; }
    public decimal Subtotal { get; set; }
    public int Videojuego { get; set; }

    public Pedidos? _Pedido { get; set; }
    public Videojuegos? _Videojuego { get; set; }
}

public class Categorias
{
    public int Id { get; set; }
    public string? Nombre { get; set; }
    public string? Descripcion { get; set; }
    public string? Estado { get; set; }
    public DateTime Fecha_Creacion { get; set; }

    public List<Videojuegos> Videojuegos { get; set; } = new List<Videojuegos>();
}

public class Videojuegos
{
    public int Id { get; set; }
    public int Categoria { get; set; }
    public int Inventario { get; set; }
    public string? Nombre { get; set; }
    public decimal Precio { get; set; }
    public string? Estado { get; set; }

    public Categorias? _Categoria { get; set; }
    public Inventarios? _Inventario { get; set; }
    public List<Detalle_Ventas> Detalle_Ventas { get; set; } = new List<Detalle_Ventas>();
    public List<Detalle_Pedidos> Detalle_Pedidos { get; set; } = new List<Detalle_Pedidos>();
    public List<VJ_Plataformas> VJ_Plataformas { get; set; } = new List<VJ_Plataformas>();
    public List<Resenas> Resenas { get; set; } = new List<Resenas>();
    public List<Desarr_Videoj> Desarr_Videoj { get; set; } = new List<Desarr_Videoj>();
}

public class Plataformas
{
    public int Id { get; set; }
    public string? Nombre { get; set; }
    public string? Fabricante { get; set; }
    public string? Tipo { get; set; }
    public string? Estado { get; set; }

    public List<VJ_Plataformas> VJ_Plataformas { get; set; } = new List<VJ_Plataformas>();
}

public class VJ_Plataformas
{
    public int Id { get; set; }
    public int Videojuego { get; set; }
    public int Plataforma { get; set; }
    public DateTime Fecha_Lanzamiento { get; set; }
    public decimal Precio_Plataforma { get; set; }

    public Videojuegos? _Videojuego { get; set; }
    public Plataformas? _Plataforma { get; set; }
    public List<VJ_Promociones> VJ_Promociones { get; set; } = new List<VJ_Promociones>();
}

public class Resenas
{
    public int Id { get; set; }
    public int Cliente { get; set; }
    public int Videojuego { get; set; }
    public int Puntuacion { get; set; }
    public string? Comentario { get; set; }
    public DateTime Fecha_Resena { get; set; }

    public Clientes? _Cliente { get; set; }
    public Videojuegos? _Videojuego { get; set; }
}

public class Promociones
{
    public int Id { get; set; }
    public string? Nombre { get; set; }
    public string? Descripcion { get; set; }
    public decimal Porcentaje_Desc { get; set; }
    public DateTime Fecha_Inicio { get; set; }
    public DateTime Fecha_Fin { get; set; }

    public List<VJ_Promociones> VJ_Promociones { get; set; } = new List<VJ_Promociones>();
}

public class VJ_Promociones
{
    public int Id { get; set; }
    public int Vj_Plataforma { get; set; }
    public int Promocion { get; set; }
    public decimal Precio_Promocion { get; set; }

    public VJ_Plataformas? _Vj_Plataforma { get; set; }
    public Promociones? _Promocion { get; set; }
}

public class Desarrolladores
{
    public int Id { get; set; }
    public string? Nombre { get; set; }
    public string? Nit { get; set; }
    public string? Pais { get; set; }
    public string? Sitio_Web { get; set; }

    public List<Desarr_Videoj> Desarr_Videoj { get; set; } = new List<Desarr_Videoj>();
}

public class Desarr_Videoj
{
    public int Id { get; set; }
    public int Videojuego { get; set; }
    public int Desarrollador { get; set; }
    public DateTime Fecha_Lanzamiento { get; set; }
    public string? Clasif_Edad { get; set; }

    public Videojuegos? _Videojuego { get; set; }
    public Desarrolladores? _Desarrollador { get; set; }
}
