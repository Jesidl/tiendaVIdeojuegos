/*
CREATE DATABASE db_tienda_fajas;
GO
USE db_tienda_fajas;
GO

CREATE TABLE [Cargos] (
	[Id] INT NOT NULL PRIMARY KEY IDENTITY(1, 1),
	[Nombre] NVARCHAR(100) NOT NULL,
	[Descripcion] NVARCHAR(500) NOT NULL,
	[Salario] DECIMAL(10, 2) NOT NULL,
);

INSERT INTO [Cargos] ([Nombre], [Descripcion], [Salario])
VALUES ('Asesora comercial', 'Atencion y venta de fajas', 1600000.00);

CREATE TABLE [Sucursales] (
	[Id] INT NOT NULL PRIMARY KEY IDENTITY(1, 1),
	[Nombre] NVARCHAR(100) NOT NULL,
	[Direccion] NVARCHAR(200) NOT NULL,
	[Ciudad] NVARCHAR(100) NOT NULL,
	[Telefono] NVARCHAR(20) NOT NULL,
	[Estado] BIT NOT NULL,
);

INSERT INTO [Sucursales] ([Nombre], [Direccion], [Ciudad], [Telefono], [Estado])
VALUES ('Sucursal Centro', 'Calle 52 # 49-30', 'Medellin', '6045123456', 1);

CREATE TABLE [Clientes] (
	[Id] INT NOT NULL PRIMARY KEY IDENTITY(1, 1),
	[Cedula] NVARCHAR(50) NOT NULL UNIQUE,
	[Nombre] NVARCHAR(200) NOT NULL,
	[Telefono] NVARCHAR(20) NOT NULL,
	[Correo] NVARCHAR(100) NOT NULL,
	[Direccion] NVARCHAR(200) NOT NULL,
);

INSERT INTO [Clientes] ([Cedula], [Nombre], [Telefono], [Correo], [Direccion])
VALUES ('1001', 'Maria Lopez', '3001234567', 'maria@correo.com', 'Calle 10 # 20-30');

CREATE TABLE [Proveedores] (
	[Id] INT NOT NULL PRIMARY KEY IDENTITY(1, 1),
	[Nit] NVARCHAR(50) NOT NULL UNIQUE,
	[Nombre_Empresa] NVARCHAR(200) NOT NULL,
	[Telefono] NVARCHAR(20) NOT NULL,
	[Correo] NVARCHAR(100) NOT NULL,
);

INSERT INTO [Proveedores] ([Nit], [Nombre_Empresa], [Telefono], [Correo])
VALUES ('900123456-1', 'Confecciones Moldeate SAS', '6044440000', 'ventas@moldeate.com');

CREATE TABLE [Categorias] (
	[Id] INT NOT NULL PRIMARY KEY IDENTITY(1, 1),
	[Nombre] NVARCHAR(100) NOT NULL,
	[Descripcion] NVARCHAR(500) NOT NULL,
	[Estado] BIT NOT NULL,
);

INSERT INTO [Categorias] ([Nombre], [Descripcion], [Estado])
VALUES ('Postquirurgicas', 'Fajas para recuperacion despues de cirugia', 1);

CREATE TABLE [Marcas] (
	[Id] INT NOT NULL PRIMARY KEY IDENTITY(1, 1),
	[Nombre] NVARCHAR(100) NOT NULL,
	[Pais] NVARCHAR(100) NOT NULL,
	[Estado] BIT NOT NULL,
);

INSERT INTO [Marcas] ([Nombre], [Pais], [Estado])
VALUES ('Fajas Salome', 'Colombia', 1);

CREATE TABLE [Materiales] (
	[Id] INT NOT NULL PRIMARY KEY IDENTITY(1, 1),
	[Nombre] NVARCHAR(100) NOT NULL,
	[Descripcion] NVARCHAR(500) NOT NULL,
);

INSERT INTO [Materiales] ([Nombre], [Descripcion])
VALUES ('Powernet', 'Tela de alta compresion y transpirable');

CREATE TABLE [Tallas] (
	[Id] INT NOT NULL PRIMARY KEY IDENTITY(1, 1),
	[Nombre] NVARCHAR(10) NOT NULL,
	[Descripcion] NVARCHAR(200) NOT NULL,
);

INSERT INTO [Tallas] ([Nombre], [Descripcion])
VALUES ('M', 'Cintura de 70 a 76 cm');

CREATE TABLE [Colores] (
	[Id] INT NOT NULL PRIMARY KEY IDENTITY(1, 1),
	[Nombre] NVARCHAR(50) NOT NULL,
	[Codigo_Hex] NVARCHAR(10) NOT NULL,
);

INSERT INTO [Colores] ([Nombre], [Codigo_Hex])
VALUES ('Beige', '#F5F5DC');

CREATE TABLE [Metodos_Pago] (
	[Id] INT NOT NULL PRIMARY KEY IDENTITY(1, 1),
	[Nombre] NVARCHAR(50) NOT NULL,
	[Descripcion] NVARCHAR(500) NOT NULL,
	[Estado] BIT NOT NULL,
);

INSERT INTO [Metodos_Pago] ([Nombre], [Descripcion], [Estado])
VALUES ('Efectivo', 'Pago en caja', 1);

CREATE TABLE [Empleados] (
	[Id] INT NOT NULL PRIMARY KEY IDENTITY(1, 1),
	[Cedula] NVARCHAR(50) NOT NULL UNIQUE,
	[Nombre] NVARCHAR(200) NOT NULL,
	[Telefono] NVARCHAR(20) NOT NULL,
	[Sucursal] INT NOT NULL REFERENCES [Sucursales]([Id]),
	[Cargo] INT NOT NULL REFERENCES [Cargos]([Id]),
);

INSERT INTO [Empleados] ([Cedula], [Nombre], [Telefono], [Sucursal], [Cargo])
VALUES ('2001', 'Laura Gomez', '3109876543', 1, 1);

CREATE TABLE [Medidas] (
	[Id] INT NOT NULL PRIMARY KEY IDENTITY(1, 1),
	[Cliente] INT NOT NULL REFERENCES [Clientes]([Id]),
	[Cintura] DECIMAL(10, 2) NOT NULL,
	[Cadera] DECIMAL(10, 2) NOT NULL,
	[Busto] DECIMAL(10, 2) NOT NULL,
	[Fecha] SMALLDATETIME NOT NULL,
);

INSERT INTO [Medidas] ([Cliente], [Cintura], [Cadera], [Busto], [Fecha])
VALUES (1, 72.50, 98.00, 90.00, GETDATE());

CREATE TABLE [Productos] (
	[Id] INT NOT NULL PRIMARY KEY IDENTITY(1, 1),
	[Codigo] NVARCHAR(50) NOT NULL UNIQUE,
	[Nombre] NVARCHAR(200) NOT NULL,
	[Descripcion] NVARCHAR(500) NOT NULL,
	[Precio] DECIMAL(10, 2) NOT NULL,
	[Estado] BIT NOT NULL,
	[Categoria] INT NOT NULL REFERENCES [Categorias]([Id]),
	[Marca] INT NOT NULL REFERENCES [Marcas]([Id]),
	[Material] INT NOT NULL REFERENCES [Materiales]([Id]),
);

INSERT INTO [Productos] ([Codigo], [Nombre], [Descripcion], [Precio], [Estado], [Categoria], [Marca], [Material])
VALUES ('FAJ-001', 'Faja reductora postquirurgica', 'Faja de alta compresion con broches', 280000.00, 1, 1, 1, 1);

CREATE TABLE [Variantes] (
	[Id] INT NOT NULL PRIMARY KEY IDENTITY(1, 1),
	[Sku] NVARCHAR(50) NOT NULL UNIQUE,
	[Producto] INT NOT NULL REFERENCES [Productos]([Id]),
	[Talla] INT NOT NULL REFERENCES [Tallas]([Id]),
	[Color] INT NOT NULL REFERENCES [Colores]([Id]),
	[Precio_Adicional] DECIMAL(10, 2) NOT NULL,
);

INSERT INTO [Variantes] ([Sku], [Producto], [Talla], [Color], [Precio_Adicional])
VALUES ('FAJ-001-M-BEI', 1, 1, 1, 0.00);

CREATE TABLE [Inventarios] (
	[Id] INT NOT NULL PRIMARY KEY IDENTITY(1, 1),
	[Variante] INT NOT NULL REFERENCES [Variantes]([Id]),
	[Sucursal] INT NOT NULL REFERENCES [Sucursales]([Id]),
	[Cantidad] INT NOT NULL,
	[Stock_Minimo] INT NOT NULL,
	[Fecha_Actualizacion] SMALLDATETIME NOT NULL,
);

INSERT INTO [Inventarios] ([Variante], [Sucursal], [Cantidad], [Stock_Minimo], [Fecha_Actualizacion])
VALUES (1, 1, 30, 5, GETDATE());

CREATE TABLE [Ventas] (
	[Id] INT NOT NULL PRIMARY KEY IDENTITY(1, 1),
	[Codigo] NVARCHAR(50) NOT NULL UNIQUE,
	[Cliente] INT NOT NULL REFERENCES [Clientes]([Id]),
	[Empleado] INT NOT NULL REFERENCES [Empleados]([Id]),
	[Fecha] SMALLDATETIME NOT NULL,
	[Total] DECIMAL(10, 2) NOT NULL,
);

INSERT INTO [Ventas] ([Codigo], [Cliente], [Empleado], [Fecha], [Total])
VALUES ('V-0001', 1, 1, GETDATE(), 280000.00);

CREATE TABLE [Detalles_Ventas] (
	[Id] INT NOT NULL PRIMARY KEY IDENTITY(1, 1),
	[Venta] INT NOT NULL REFERENCES [Ventas]([Id]),
	[Variante] INT NOT NULL REFERENCES [Variantes]([Id]),
	[Cantidad] INT NOT NULL,
	[Precio_Unitario] DECIMAL(10, 2) NOT NULL,
	[Subtotal] DECIMAL(10, 2) NOT NULL,
);

INSERT INTO [Detalles_Ventas] ([Venta], [Variante], [Cantidad], [Precio_Unitario], [Subtotal])
VALUES (1, 1, 1, 280000.00, 280000.00);

CREATE TABLE [Pagos] (
	[Id] INT NOT NULL PRIMARY KEY IDENTITY(1, 1),
	[Venta] INT NOT NULL REFERENCES [Ventas]([Id]),
	[Metodo_Pago] INT NOT NULL REFERENCES [Metodos_Pago]([Id]),
	[Valor] DECIMAL(10, 2) NOT NULL,
	[Fecha] SMALLDATETIME NOT NULL,
);

INSERT INTO [Pagos] ([Venta], [Metodo_Pago], [Valor], [Fecha])
VALUES (1, 1, 280000.00, GETDATE());

CREATE TABLE [Pedidos] (
	[Id] INT NOT NULL PRIMARY KEY IDENTITY(1, 1),
	[Codigo] NVARCHAR(50) NOT NULL UNIQUE,
	[Proveedor] INT NOT NULL REFERENCES [Proveedores]([Id]),
	[Empleado] INT NOT NULL REFERENCES [Empleados]([Id]),
	[Fecha] SMALLDATETIME NOT NULL,
	[Estado] NVARCHAR(20) NOT NULL,
	[Total] DECIMAL(10, 2) NOT NULL,
);

INSERT INTO [Pedidos] ([Codigo], [Proveedor], [Empleado], [Fecha], [Estado], [Total])
VALUES ('P-0001', 1, 1, GETDATE(), 'Recibido', 1800000.00);

CREATE TABLE [Detalles_Pedidos] (
	[Id] INT NOT NULL PRIMARY KEY IDENTITY(1, 1),
	[Pedido] INT NOT NULL REFERENCES [Pedidos]([Id]),
	[Variante] INT NOT NULL REFERENCES [Variantes]([Id]),
	[Cantidad] INT NOT NULL,
	[Precio_Compra] DECIMAL(10, 2) NOT NULL,
	[Subtotal] DECIMAL(10, 2) NOT NULL,
);

INSERT INTO [Detalles_Pedidos] ([Pedido], [Variante], [Cantidad], [Precio_Compra], [Subtotal])
VALUES (1, 1, 10, 180000.00, 1800000.00);
*/
