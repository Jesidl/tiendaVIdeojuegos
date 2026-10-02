# Tienda de Fajas

Proyecto académico de programación de software en C# / .NET: solución de acceso a datos con Entity Framework Core y SQL Server para una tienda de fajas.

## Requisitos

- [.NET SDK 10.0](https://dotnet.microsoft.com/download)
- SQL Server (Express es suficiente) con autenticación de Windows
- Visual Studio Community 2026 (la solución usa el formato `.slnx`)

## Entregable

**Fecha límite:** 01 de octubre de 2026, 11:59 p. m.

| # | Condición | Cómo se cumple |
|---|---|---|
| 1 | Git commit por persona | Cada integrante hace commit desde su propia cuenta de git |
| 2 | 1 solución, 3 proyectos | `backend/tiendaFajas.slnx` con `lib_aplicaciones`, `csl_presentacion` y `mst_presentacion` |
| 2.1 | `Script.sql` en la raíz de la lib | `backend/lib_aplicaciones/Script.sql` con la base de datos y las 20 tablas |
| 2.2 | 20 entidades | `backend/lib_aplicaciones/entidades/`, un archivo por entidad |
| 2.3 | `IConexion` y `Conexion` | `interfaces/IConexion.cs` + `implementaciones/Conexion.cs` |
| 2.4 | 20 pruebas unitarias | `backend/mst_presentacion/`, una clase de prueba por entidad |

## Estructura

```
tiendaFajas/
├── backend/
│   ├── tiendaFajas.slnx
│   ├── lib_aplicaciones/               ← librería de acceso a datos (EF Core SqlServer 10.0.12)
│   │   ├── Script.sql
│   │   ├── entidades/                  ← 20 entidades
│   │   ├── interfaces/IConexion.cs
│   │   ├── implementaciones/Conexion.cs
│   │   └── nucleo/Datosgenerales.cs    ← cadena de conexión
│   ├── csl_presentacion/               ← consola: consulta las 20 tablas
│   └── mst_presentacion/               ← MSTest 4.0.1: 20 pruebas CRUD
└── README.md
```

## Modelo de datos (20 tablas)

| Módulo | Tablas |
|---|---|
| Personal | `Cargos`, `Sucursales`, `Empleados` |
| Clientes | `Clientes`, `Medidas` (cintura, cadera y busto del cliente para elegir la talla) |
| Catálogo | `Categorias`, `Marcas`, `Materiales`, `Productos`, `Tallas`, `Colores`, `Variantes` |
| Inventario | `Inventarios` (stock de una variante en una sucursal) |
| Ventas | `Ventas`, `Detalles_Ventas`, `Metodos_Pago`, `Pagos` |
| Compras | `Proveedores`, `Pedidos`, `Detalles_Pedidos` |

- **`Variantes`** es un producto en una talla y un color concretos (SKU). Es lo que se inventaría, se vende y se pide.
- Relaciones:
  - `Empleados` → `Sucursales`, `Cargos`
  - `Medidas` → `Clientes`
  - `Productos` → `Categorias`, `Marcas`, `Materiales`
  - `Variantes` → `Productos`, `Tallas`, `Colores`
  - `Inventarios` → `Variantes`, `Sucursales`
  - `Ventas` → `Clientes`, `Empleados`
  - `Detalles_Ventas` → `Ventas`, `Variantes`
  - `Pagos` → `Ventas`, `Metodos_Pago`
  - `Pedidos` → `Proveedores`, `Empleados`
  - `Detalles_Pedidos` → `Pedidos`, `Variantes`
- Columnas `Estado`:
  - En `Sucursales`, `Categorias`, `Marcas`, `Productos` y `Metodos_Pago` son `BIT` / `bool` (`1` = activo, `0` = inactivo).
  - En `Pedidos` es texto: `Pendiente`, `Enviado`, `Recibido` o `Cancelado`.

## Convenciones

- **Entidades:** propiedades automáticas públicas. Cada FK es un `int` más una navegación `[ForeignKey("X")] public Padre? _X`. El lado "uno" de la relación expone `List<Hijo>?`. Los montos son `decimal`.
- **`Script.sql`:**
  - Todo el contenido va dentro de `/* */`, incluido en el `.csproj` con `<Compile Include="Script.sql" />`.
  - `IDENTITY(1, 1)`, FK en línea con `REFERENCES`, `DECIMAL(10, 2)`, `SMALLDATETIME` y `UNIQUE` en `Cedula`, `Nit`, `Codigo` y `Sku`.
  - Cada tabla lleva un registro semilla con `Id = 1`.
- **Pruebas:** cada clase ejecuta `Insertar → Consultar → Actualizar → Borrar` sobre su tabla. Las entidades con FK apuntan a las semillas (`= 1`), y cada prueba borra lo que insertó.

## Cómo ejecutar

1. **Crear la base de datos:** abrir SQL Server Management Studio, pegar el contenido de `backend/lib_aplicaciones/Script.sql` sin los `/*` `*/` y ejecutar con **F5**.
2. **Cadena de conexión:** si el servidor no es `localhost\SQLEXPRESS`, cambiar `server=` en `lib_aplicaciones/nucleo/Datosgenerales.cs`.
3. **En Visual Studio Community:** abrir `backend/tiendaFajas.slnx`.
   - Para la consola: clic derecho en `csl_presentacion` → **Establecer como proyecto de inicio** → **F5**.
   - Para las pruebas: **Prueba → Explorador de pruebas → Ejecutar todas**.

Desde la terminal:

```bash
dotnet build backend/tiendaFajas.slnx
dotnet run --project backend/csl_presentacion
dotnet test backend/tiendaFajas.slnx
```
