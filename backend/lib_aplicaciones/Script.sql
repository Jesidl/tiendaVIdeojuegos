/*
CREATE DATABASE db_tienda_videojuegos;
GO
USE db_tienda_videojuegos;
GO

CREATE TABLE [Cargos] (
	[Id] INT NOT NULL PRIMARY KEY IDENTITY(1, 1),
	[Nombre] NVARCHAR(100) NOT NULL,
	[Descripcion] NVARCHAR(500) NOT NULL,
	[Salario] DECIMAL(10, 2) NOT NULL,
);

INSERT INTO [Cargos] ([Nombre], [Descripcion], [Salario])
VALUES ('Vendedor', 'Atencion y venta en tienda', 1800000.00);

CREATE TABLE [Sucursales] (
	[Id] INT NOT NULL PRIMARY KEY IDENTITY(1, 1),
	[Nombre] NVARCHAR(100) NOT NULL,
	[Direccion] NVARCHAR(200) NOT NULL,
	[Ciudad] NVARCHAR(100) NOT NULL,
	[Telefono] NVARCHAR(20) NOT NULL,
	[Estado] NVARCHAR(20) NOT NULL,
);

INSERT INTO [Sucursales] ([Nombre], [Direccion], [Ciudad], [Telefono], [Estado])
VALUES ('Sucursal Centro', 'Calle 50 # 45-20', 'Medellin', '6045551234', 'Activa');

CREATE TABLE [Clientes] (
	[Id] INT NOT NULL PRIMARY KEY IDENTITY(1, 1),
	[Nombre] NVARCHAR(200) NOT NULL,
	[Direccion] NVARCHAR(200) NOT NULL,
	[Cedula] NVARCHAR(50) NOT NULL UNIQUE,
	[Ciudad] NVARCHAR(100) NOT NULL,
);

INSERT INTO [Clientes] ([Nombre], [Direccion], [Cedula], [Ciudad])
VALUES ('Pepito Perez', 'Carrera 70 # 30-15', '1001', 'Medellin');

CREATE TABLE [Proveedores] (
	[Id] INT NOT NULL PRIMARY KEY IDENTITY(1, 1),
	[Nit] NVARCHAR(50) NOT NULL UNIQUE,
	[Nombre_Empresa] NVARCHAR(200) NOT NULL,
	[Telefono] NVARCHAR(20) NOT NULL,
	[Correo] NVARCHAR(100) NOT NULL,
);

INSERT INTO [Proveedores] ([Nit], [Nombre_Empresa], [Telefono], [Correo])
VALUES ('900123456-1', 'Distribuidora Gamer SAS', '6044440000', 'ventas@distgamer.com');

CREATE TABLE [Categorias] (
	[Id] INT NOT NULL PRIMARY KEY IDENTITY(1, 1),
	[Nombre] NVARCHAR(100) NOT NULL,
	[Descripcion] NVARCHAR(500) NOT NULL,
	[Estado] NVARCHAR(20) NOT NULL,
	[Fecha_Creacion] SMALLDATETIME NOT NULL,
);

INSERT INTO [Categorias] ([Nombre], [Descripcion], [Estado], [Fecha_Creacion])
VALUES ('Accion', 'Juegos de accion y aventura', 'Activa', GETDATE());

CREATE TABLE [Plataformas] (
	[Id] INT NOT NULL PRIMARY KEY IDENTITY(1, 1),
	[Nombre] NVARCHAR(100) NOT NULL,
	[Fabricante] NVARCHAR(100) NOT NULL,
	[Tipo] NVARCHAR(50) NOT NULL,
	[Estado] NVARCHAR(20) NOT NULL,
);

INSERT INTO [Plataformas] ([Nombre], [Fabricante], [Tipo], [Estado])
VALUES ('Nintendo Switch', 'Nintendo', 'Consola', 'Activa');

CREATE TABLE [Promociones] (
	[Id] INT NOT NULL PRIMARY KEY IDENTITY(1, 1),
	[Nombre] NVARCHAR(100) NOT NULL,
	[Descripcion] NVARCHAR(500) NOT NULL,
	[Porcentaje_Desc] DECIMAL(10, 2) NOT NULL,
	[Fecha_Inicio] SMALLDATETIME NOT NULL,
	[Fecha_Fin] SMALLDATETIME NOT NULL,
);

INSERT INTO [Promociones] ([Nombre], [Descripcion], [Porcentaje_Desc], [Fecha_Inicio], [Fecha_Fin])
VALUES ('Black Friday', 'Descuento de temporada', 20.00, GETDATE(), GETDATE());

CREATE TABLE [Desarrolladores] (
	[Id] INT NOT NULL PRIMARY KEY IDENTITY(1, 1),
	[Nombre] NVARCHAR(200) NOT NULL,
	[Nit] NVARCHAR(50) NOT NULL UNIQUE,
	[Pais] NVARCHAR(100) NOT NULL,
	[Sitio_Web] NVARCHAR(200) NOT NULL,
);

INSERT INTO [Desarrolladores] ([Nombre], [Nit], [Pais], [Sitio_Web])
VALUES ('Nintendo EPD', 'JP-0001', 'Japon', 'www.nintendo.com');

CREATE TABLE [Empleados] (
	[Id] INT NOT NULL PRIMARY KEY IDENTITY(1, 1),
	[Nombre] NVARCHAR(200) NOT NULL,
	[Cedula] NVARCHAR(50) NOT NULL UNIQUE,
	[Direccion] NVARCHAR(200) NOT NULL,
	[Sucursal] INT NOT NULL REFERENCES [Sucursales]([Id]),
	[Cargo] INT NOT NULL REFERENCES [Cargos]([Id]),
);

INSERT INTO [Empleados] ([Nombre], [Cedula], [Direccion], [Sucursal], [Cargo])
VALUES ('Juan Gomez', '2001', 'Calle 10 # 20-30', 1, 1);

CREATE TABLE [Videojuegos] (
	[Id] INT NOT NULL PRIMARY KEY IDENTITY(1, 1),
	[Categoria] INT NOT NULL REFERENCES [Categorias]([Id]),
	[Nombre] NVARCHAR(200) NOT NULL,
	[Precio] DECIMAL(10, 2) NOT NULL,
	[Estado] NVARCHAR(20) NOT NULL,
);

INSERT INTO [Videojuegos] ([Categoria], [Nombre], [Precio], [Estado])
VALUES (1, 'The Legend of Zelda', 250000.00, 'Disponible');

CREATE TABLE [VJ_Plataformas] (
	[Id] INT NOT NULL PRIMARY KEY IDENTITY(1, 1),
	[Videojuego] INT NOT NULL REFERENCES [Videojuegos]([Id]),
	[Plataforma] INT NOT NULL REFERENCES [Plataformas]([Id]),
	[Fecha_Lanzamiento] SMALLDATETIME NOT NULL,
	[Precio_Plataforma] DECIMAL(10, 2) NOT NULL,
);

INSERT INTO [VJ_Plataformas] ([Videojuego], [Plataforma], [Fecha_Lanzamiento], [Precio_Plataforma])
VALUES (1, 1, GETDATE(), 260000.00);

CREATE TABLE [Inventarios] (
	[Id] INT NOT NULL PRIMARY KEY IDENTITY(1, 1),
	[Cantidad] INT NOT NULL,
	[Fecha_Actualizacion] SMALLDATETIME NOT NULL,
	[Stock_Minimo] INT NOT NULL,
	[Sucursal] INT NOT NULL REFERENCES [Sucursales]([Id]),
	[Vj_Plataforma] INT NOT NULL REFERENCES [VJ_Plataformas]([Id]),
);

INSERT INTO [Inventarios] ([Cantidad], [Fecha_Actualizacion], [Stock_Minimo], [Sucursal], [Vj_Plataforma])
VALUES (50, GETDATE(), 5, 1, 1);

CREATE TABLE [Ventas] (
	[Id] INT NOT NULL PRIMARY KEY IDENTITY(1, 1),
	[Cliente] INT NOT NULL REFERENCES [Clientes]([Id]),
	[Empleado] INT NOT NULL REFERENCES [Empleados]([Id]),
	[Fecha_Venta] SMALLDATETIME NOT NULL,
	[Total] DECIMAL(10, 2) NOT NULL,
);

INSERT INTO [Ventas] ([Cliente], [Empleado], [Fecha_Venta], [Total])
VALUES (1, 1, GETDATE(), 260000.00);

CREATE TABLE [Detalle_Ventas] (
	[Id] INT NOT NULL PRIMARY KEY IDENTITY(1, 1),
	[Venta] INT NOT NULL REFERENCES [Ventas]([Id]),
	[Cantidad] INT NOT NULL,
	[Precio_Unitario] DECIMAL(10, 2) NOT NULL,
	[Subtotal] DECIMAL(10, 2) NOT NULL,
	[Vj_Plataforma] INT NOT NULL REFERENCES [VJ_Plataformas]([Id]),
);

INSERT INTO [Detalle_Ventas] ([Venta], [Cantidad], [Precio_Unitario], [Subtotal], [Vj_Plataforma])
VALUES (1, 1, 260000.00, 260000.00, 1);

CREATE TABLE [Metodo_Pagos] (
	[Id] INT NOT NULL PRIMARY KEY IDENTITY(1, 1),
	[Venta] INT NOT NULL REFERENCES [Ventas]([Id]),
	[Tipo_Pago] NVARCHAR(50) NOT NULL,
	[Valor] DECIMAL(10, 2) NOT NULL,
	[Descripcion] NVARCHAR(500) NOT NULL,
);

INSERT INTO [Metodo_Pagos] ([Venta], [Tipo_Pago], [Valor], [Descripcion])
VALUES (1, 'Efectivo', 260000.00, 'Pago completo en caja');

CREATE TABLE [Pedidos] (
	[Id] INT NOT NULL PRIMARY KEY IDENTITY(1, 1),
	[Proveedor] INT NOT NULL REFERENCES [Proveedores]([Id]),
	[Empleado] INT NOT NULL REFERENCES [Empleados]([Id]),
	[Fecha_Pedido] SMALLDATETIME NOT NULL,
	[Estado] NVARCHAR(20) NOT NULL,
	[Total] DECIMAL(10, 2) NOT NULL,
);

INSERT INTO [Pedidos] ([Proveedor], [Empleado], [Fecha_Pedido], [Estado], [Total])
VALUES (1, 1, GETDATE(), 'Recibido', 1800000.00);

CREATE TABLE [Detalle_Pedidos] (
	[Id] INT NOT NULL PRIMARY KEY IDENTITY(1, 1),
	[Pedido] INT NOT NULL REFERENCES [Pedidos]([Id]),
	[Cantidad] INT NOT NULL,
	[Precio_Compra] DECIMAL(10, 2) NOT NULL,
	[Subtotal] DECIMAL(10, 2) NOT NULL,
	[Vj_Plataforma] INT NOT NULL REFERENCES [VJ_Plataformas]([Id]),
);

INSERT INTO [Detalle_Pedidos] ([Pedido], [Cantidad], [Precio_Compra], [Subtotal], [Vj_Plataforma])
VALUES (1, 10, 180000.00, 1800000.00, 1);

CREATE TABLE [Resenas] (
	[Id] INT NOT NULL PRIMARY KEY IDENTITY(1, 1),
	[Cliente] INT NOT NULL REFERENCES [Clientes]([Id]),
	[Videojuego] INT NOT NULL REFERENCES [Videojuegos]([Id]),
	[Puntuacion] INT NOT NULL,
	[Comentario] NVARCHAR(1000) NOT NULL,
	[Fecha_Resena] SMALLDATETIME NOT NULL,
);

INSERT INTO [Resenas] ([Cliente], [Videojuego], [Puntuacion], [Comentario], [Fecha_Resena])
VALUES (1, 1, 5, 'Excelente juego', GETDATE());

CREATE TABLE [VJ_Promociones] (
	[Id] INT NOT NULL PRIMARY KEY IDENTITY(1, 1),
	[Vj_Plataforma] INT NOT NULL REFERENCES [VJ_Plataformas]([Id]),
	[Promocion] INT NOT NULL REFERENCES [Promociones]([Id]),
	[Precio_Promocion] DECIMAL(10, 2) NOT NULL,
);

INSERT INTO [VJ_Promociones] ([Vj_Plataforma], [Promocion], [Precio_Promocion])
VALUES (1, 1, 208000.00);

CREATE TABLE [Desarr_Videoj] (
	[Id] INT NOT NULL PRIMARY KEY IDENTITY(1, 1),
	[Videojuego] INT NOT NULL REFERENCES [Videojuegos]([Id]),
	[Desarrollador] INT NOT NULL REFERENCES [Desarrolladores]([Id]),
	[Fecha_Lanzamiento] SMALLDATETIME NOT NULL,
	[Clasif_Edad] NVARCHAR(20) NOT NULL,
);

INSERT INTO [Desarr_Videoj] ([Videojuego], [Desarrollador], [Fecha_Lanzamiento], [Clasif_Edad])
VALUES (1, 1, GETDATE(), 'E10+');
*/
