# Tienda de Videojuegos

Proyecto académico de programación en C# / .NET, desarrollado siguiendo la rúbrica del profesor.

**Integrantes:** Jesid López · Johnny Garzón · Samuel Gallego

## Requisitos

- [.NET SDK 10.0](https://dotnet.microsoft.com/download) o superior
- SQL Server (Express es suficiente) con autenticación de Windows
- Visual Studio Community 2022 (17.13+) o 2026 — la solución usa el formato `.slnx`.
  También se puede trabajar desde VS Code con el CLI `dotnet`; el resultado es idéntico.
- Paquete NuGet `Microsoft.EntityFrameworkCore.SqlServer` 10.0.x (solo en `lib_aplicaciones`)

---

## Entregable 2 — Solución con acceso a datos

**Fecha límite:** 01 de octubre de 2026, 11:59 p. m.

| # | Condición | Cómo se cumple |
|---|---|---|
| 1 | Git commit por persona | Cada integrante hace commit desde su propia cuenta de git |
| 2 | 1 solución, 3 proyectos | `backend/tiendaVideojuegos.slnx` con `lib_aplicaciones`, `csl_presentacion` y `mst_presentacion` |
| 2.1 | `Script.sql` en la raíz de la lib | `backend/lib_aplicaciones/Script.sql` con la creación de la BD y las 20 tablas |
| 2.2 | 20 entidades | `backend/lib_aplicaciones/entidades/` — un archivo `.cs` por entidad |
| 2.3 | `IConexion` y `Conexion` | `interfaces/IConexion.cs` + `implementaciones/Conexion.cs` (DbContext de EF Core) |
| 2.4 | 20 pruebas unitarias | `backend/mst_presentacion/` — una clase de prueba por entidad |
| 3 | Entrega | Hasta el 01 oct 2026, 11:59 p. m. |

### Estructura de la solución

```
tiendaVIdeojuegos/
├── backend/
│   ├── tiendaVideojuegos.slnx
│   ├── lib_aplicaciones/                 ← librería de clases (acceso a datos)
│   │   ├── Script.sql
│   │   ├── entidades/                    ← 20 archivos: Clientes.cs, Ventas.cs, …
│   │   ├── interfaces/
│   │   │   └── IConexion.cs
│   │   ├── implementaciones/
│   │   │   └── Conexion.cs
│   │   └── nucleo/
│   │       └── Datosgenerales.cs         ← cadena de conexión centralizada
│   ├── csl_presentacion/                 ← aplicación de consola (referencia a la lib)
│   │   └── Program.cs
│   └── mst_presentacion/                 ← proyecto MSTest (referencia a la lib)
│       ├── MSTestSettings.cs
│       └── *Pruebas.cs                   ← 20 clases: ClientesPruebas.cs, VentasPruebas.cs, …
├── README.md
└── .gitignore
```

- Los tres proyectos y la solución viven **dentro de `backend/`**.
- El proyecto de consola anterior de la raíz (`tiendaVIdeojuegos.csproj` + `Program.cs`) **se elimina**: sus 20 clases pasan a `lib_aplicaciones/entidades/`, una por archivo. Queda en el historial de git.
  Motivo: un `.csproj` en la raíz compila por defecto todos los `.cs` de las subcarpetas, incluido `backend/`, y rompería el build con clases duplicadas.
- Todos los proyectos apuntan a `net10.0`, con `ImplicitUsings` y `Nullable` habilitados.

### Entidades (20)

| Módulo | Entidades |
|---|---|
| Personal | `Cargos`, `Empleados`, `Sucursales` |
| Clientes | `Clientes`, `Resenas` |
| Ventas | `Ventas`, `Detalle_Ventas`, `Metodo_Pagos` |
| Compras | `Proveedores`, `Pedidos`, `Detalle_Pedidos` |
| Catálogo | `Categorias`, `Videojuegos`, `Plataformas`, `VJ_Plataformas`, `Desarrolladores`, `Desarr_Videoj` |
| Inventario | `Inventarios` (stock de un `VJ_Plataformas` en una `Sucursales`) |
| Promociones | `Promociones`, `VJ_Promociones` |

- Namespace: `lib_aplicaciones.entidades`.
- Nombre del archivo = nombre de la clase (`Detalle_Ventas.cs`).
- Se mantiene el [estándar de modelado de entidades](#modelado-de-entidades), con dos ajustes que exige EF Core (patrón de `bibliotecas` / `auto_lavado`):
  - La navegación hacia el padre lleva `[ForeignKey("<Fk>")]` en la misma línea (requiere `using System.ComponentModel.DataAnnotations.Schema;`).
  - Las colecciones del lado "uno" son **nullable y sin inicializar**: `public List<Ventas>? Ventas { get; set; }`.

```csharp
using System.ComponentModel.DataAnnotations.Schema;

namespace lib_aplicaciones.entidades
{
    public class Ventas
    {
        public int Id { get; set; }
        public int Cliente { get; set; }
        public int Empleado { get; set; }
        public DateTime Fecha_Venta { get; set; }
        public decimal Total { get; set; }

        [ForeignKey("Cliente")] public Clientes? _Cliente { get; set; }
        [ForeignKey("Empleado")] public Empleados? _Empleado { get; set; }
        public List<Detalle_Ventas>? Detalle_Ventas { get; set; }
        public List<Metodo_Pagos>? Metodo_Pagos { get; set; }
    }
}
```

#### Correcciones al modelo del entregable 1

| # | Corrección | Cambio |
|---|---|---|
| 1 | El inventario tenía una sola `Cantidad` compartida por varios juegos | `Inventarios` es el stock de un juego-plataforma en una sucursal: FK `Vj_Plataforma` + `Sucursal`. `Videojuegos` pierde la FK `Inventario` |
| 2 | Unidades como `decimal` | `Cantidad` es `int` en `Detalle_Ventas` y `Detalle_Pedidos` |
| 3 | El pago no tenía monto | `Metodo_Pagos.Valor` (`decimal`); una venta puede pagarse con varios métodos |
| 4 | Nombres inconsistentes | `Cargos.Nombre_Cargo` → `Nombre`; `Pedidos.Fecha_Pedidos` → `Fecha_Pedido` |
| 5 | La venta y el pedido no indicaban la plataforma, aunque el precio depende de ella | `Detalle_Ventas` y `Detalle_Pedidos` apuntan a `VJ_Plataformas` (FK `Vj_Plataforma`) en vez de a `Videojuegos` |

`VJ_Plataformas` (un juego en una plataforma concreta) es ahora la unidad que se inventaría, se vende, se pide y se promociona. Las reseñas y los desarrolladores siguen asociados al `Videojuego`.

### `Script.sql`

- Ubicado en la raíz de `lib_aplicaciones`.
- Se ejecuta **manualmente en SQL Server Management Studio**: copiar solo el contenido que está entre `/*` y `*/`.
- Incluido en el `.csproj` como `<Compile Include="Script.sql" />`, con **todo su contenido dentro de un comentario `/* ... */`** (patrón del profesor), para que compile sin error.
- Crea la base de datos `db_tienda_videojuegos` y las 20 tablas:
  - Nombre de tabla = nombre de la entidad.
  - Nombre de columna = nombre de la propiedad (FK incluidas: columna `Cliente`, no `ClienteId`).
  - `[Id] INT NOT NULL PRIMARY KEY IDENTITY(1, 1)` en todas.
  - Tipos: textos `NVARCHAR(n)`, montos `DECIMAL(10, 2)`, fechas `SMALLDATETIME`, banderas `BIT`.
  - Todas las columnas `NOT NULL`.
  - `UNIQUE` de una columna en `Clientes.Cedula`, `Empleados.Cedula`, `Proveedores.Nit` y `Desarrolladores.Nit`.
  - Fechas de las semillas con `GETDATE()`.
  - Columnas `Estado` (valores que usa todo el equipo):

    | Tabla | Tipo SQL / C# | Valores |
    |---|---|---|
    | `Sucursales`, `Categorias`, `Plataformas` | `BIT` / `bool` (como `Libros.Estado` del profesor) | `1` = activa · `0` = inactiva |
    | `Videojuegos` | `NVARCHAR(20)` / `string?` | `Disponible`, `Descontinuado`, `Preventa` |
    | `Pedidos` | `NVARCHAR(20)` / `string?` | `Pendiente`, `Enviado`, `Recibido`, `Cancelado` |
  - Solo se usan construcciones vistas en los ejemplos de clase (sin `CHECK`, `UNIQUE` compuestos ni funciones de fecha adicionales).
  - FK en línea: `[Cliente] INT NOT NULL REFERENCES [Clientes]([Id]),`
- **Datos semilla:** después de cada `CREATE TABLE` va un `INSERT` con un registro. Así toda tabla tiene un registro con `Id = 1`, que las pruebas de las entidades hijas usan como FK.
- Orden de creación: primero las tablas sin dependencias (`Cargos`, `Sucursales`, `Clientes`, `Proveedores`, `Categorias`, `Plataformas`, `Promociones`, `Desarrolladores`) y después las que dependen de ellas.
- Debe poder ejecutarse completo en SSMS o con `sqlcmd` sobre un servidor vacío.

```sql
CREATE TABLE [Clientes] (
    [Id] INT NOT NULL PRIMARY KEY IDENTITY(1, 1),
    [Cedula] NVARCHAR(50) NOT NULL UNIQUE,
    [Nombre] NVARCHAR(200) NOT NULL,
    ...
);

INSERT INTO [Clientes] ([Cedula], [Nombre], ...) VALUES ('123', 'Cliente semilla', ...);

CREATE TABLE [Ventas] (
    [Id] INT NOT NULL PRIMARY KEY IDENTITY(1, 1),
    [Cliente] INT NOT NULL REFERENCES [Clientes]([Id]),
    [Empleado] INT NOT NULL REFERENCES [Empleados]([Id]),
    [Fecha_Venta] SMALLDATETIME NOT NULL,
    [Total] DECIMAL(10, 2) NOT NULL,
);
```

### `IConexion` y `Conexion`

```csharp
public interface IConexion
{
    string? StringConexion { get; set; }

    DbSet<Clientes>? Clientes { get; set; }
    // ... un DbSet por cada una de las 20 entidades

    EntityEntry<T> Entry<T>(T entity) where T : class;
    int SaveChanges();
}

public class Conexion : DbContext, IConexion
{
    public string? StringConexion { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(this.StringConexion!, p => { });
        optionsBuilder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
    }

    // ... un DbSet por cada una de las 20 entidades
}
```

- `IConexion` en `interfaces/`, `Conexion` en `implementaciones/`.
- Todo el código externo (consola y pruebas) trabaja contra `IConexion`, nunca contra `Conexion` directamente, salvo en el `new Conexion()`.

#### Mapeo de relaciones

La convención del estándar (FK `int Cliente` + navegación `Clientes? _Cliente`) **no la reconoce EF Core por sí solo**. Sin configuración, EF inventa columnas fantasma (`_ClienteId`) que no existen en `Script.sql` y las consultas fallan.

Se resuelve como el profesor, con la anotación `[ForeignKey("Cliente")]` sobre la navegación (ver [Entidades](#entidades-20)). EF empareja la navegación con la colección inversa del padre (`Clientes.Ventas`) por convención. Por eso `Conexion` **no** necesita `OnModelCreating`.

### Cadena de conexión

Centralizada en `lib_aplicaciones/nucleo/Datosgenerales.cs` (patrón de `Naturaleza`). La consola y las 20 pruebas la toman de ahí; nunca se escribe repetida:

```csharp
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
```

- `localhost\SQLEXPRESS` corresponde a una instancia SQL Server Express. Si tu SQL Server es la instancia por defecto, usa `server=localhost`.
- Cada integrante ajusta el `server=` a su máquina en ese único archivo.

### `csl_presentacion`

- Aplicación de consola que referencia a `lib_aplicaciones`.
- Crea un `IConexion`, toma la cadena de `Datosgenerales` y consulta las tablas con `.ToList()`. Las entidades hijas cargan su padre con `.Include(x => x._Padre)`.
- Todo va dentro de `try / catch`, e imprime la excepción si falla.

```csharp
try
{
    IConexion conexion = new Conexion();
    conexion.StringConexion = Datosgenerales.ObtenerStringConexion();
    var lista_clientes = conexion.Clientes!.ToList();
    var lista_ventas = conexion.Ventas!
        .Include(x => x._Cliente)
        .Include(x => x._Empleado)
        .ToList();
}
catch (Exception ex)
{
    Console.WriteLine(ex.ToString());
}
```

### Pruebas unitarias (`mst_presentacion`)

- Proyecto MSTest que referencia a `lib_aplicaciones`.
- **Una clase de prueba por entidad** (`<Entidad>Pruebas.cs`), 20 en total, en el namespace `pruebas_unitarias`.
- Cada clase sigue el patrón del profesor:

```csharp
[TestClass]
public class ClientesPruebas
{
    private IConexion conexion;
    private Clientes? entidad = null;

    public ClientesPruebas()
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

    public void Insertar()   { /* new Clientes() { ... } + Add + SaveChanges */ }
    public void Consultar()  { /* ToList(); si está vacía => throw */ }
    private void Actualizar() { /* cambiar un campo + Entry(...).State = Modified + SaveChanges */ }
    private void Borrar()     { /* Remove + SaveChanges */ }
}
```

- La conexión se declara como `IConexion`, no como `Conexion` (patrón de `bibliotecas` / `deportes` / `Naturaleza`).
- **Entidades con FK:** las FK apuntan al registro semilla (`Cliente = 1`) que crea `Script.sql`, igual que `LibrosPruebas` (`Autor = 1`). La prueba solo crea y borra su propio registro, nunca el del padre.
- Las pruebas se ejecutan en paralelo (`[assembly: Parallelize(...)]`). Ninguna prueba depende de lo que cree otra; solo de las semillas.
- Requieren que la BD exista y tenga las semillas (ejecutar antes `Script.sql`).

### Cómo ejecutar

```bash
# 1. Crear la base de datos: abrir SSMS, pegar el contenido de
#    backend/lib_aplicaciones/Script.sql (sin los /* */) y ejecutar con F5

# 2. Compilar la solución
dotnet build backend/tiendaVideojuegos.slnx

# 3. Correr la consola
dotnet run --project backend/csl_presentacion

# 4. Correr las 20 pruebas
dotnet test backend/tiendaVideojuegos.slnx
```

En Visual Studio Community: abrir `backend/tiendaVideojuegos.slnx`, compilar con **Ctrl+Shift+B** y ejecutar las pruebas desde el **Explorador de pruebas**.

### Cómo agregar una entidad a la solución

`Script.sql` ya tiene las 20 tablas; la solución tiene hoy **5 entidades con flujo completo** (`Cargos`, `Sucursales`, `Empleados`, `Categorias`, `Plataformas`), que sirven de modelo. Para cada entidad nueva:

1. **Entidad:** crear `lib_aplicaciones/entidades/<Entidad>.cs` con las mismas columnas de su tabla en `Script.sql`. Cada FK lleva `int` + `[ForeignKey("X")] public Padre? _X { get; set; }` (ver `Empleados.cs`).
2. **Colección inversa:** en cada entidad padre agregar `public List<<Entidad>>? <Entidad> { get; set; }` (ver `Sucursales.Empleados`).
3. **Conexión:** agregar `DbSet<<Entidad>>? <Entidad> { get; set; }` en `IConexion.cs` **y** en `Conexion.cs`.
4. **Consola:** agregar su `.ToList()` en `csl_presentacion/Program.cs`, con `.Include(x => x._Padre)` si tiene FK.
5. **Prueba:** crear `mst_presentacion/<Entidad>Pruebas.cs` copiando `CargosPruebas.cs` (sin FK) o `EmpleadosPruebas.cs` (con FK = 1 hacia las semillas). Los valores `UNIQUE` de la prueba deben ser distintos de los de la semilla.
6. `dotnet build` y `dotnet test`: la prueba nueva debe pasar y la tabla debe quedar solo con su semilla.

---

## Estándar de código

Estándar extraído de los ejemplos del profesor.

**Alcance:** las **entidades** no usan herencia, polimorfismo ni interfaces; son clases planas de datos.
La única interfaz del entregable es `IConexion`, y la única herencia es `Conexion : DbContext`, que exige EF Core.

### Modelado de entidades

- Una entidad es una clase de solo datos: propiedades automáticas públicas, sin lógica.
- Cada relación se codifica **dos veces**: FK como `int` plano + propiedad de navegación *nullable* con el mismo nombre prefijado con `_`. Nunca solo una de las dos.
- El lado "uno" de una relación 1–N expone `List<T>` de vuelta.
- Si hay más de una FK hacia el mismo tipo, se distingue con sufijo numérico (`_Jugador1` / `_Jugador2`).
- Relaciones N–N o líneas de detalle se resuelven con una entidad de asociación propia (con sus propios atributos), nunca M2M implícito.
- Campos monetarios siempre `decimal`, nunca `double`/`float`.

```csharp
public class Productos
{
    public int Id { get; set; }
    public string? Codigo { get; set; }
    public string? Nombre { get; set; }
    public decimal Precio { get; set; }
    public decimal Iva { get; set; }

    public List<Detalles> Detalles { get; set; }
}

public class Facturas
{
    public int Id { get; set; }
    public string? Codigo { get; set; }
    public int Cliente { get; set; }
    public DateTime Fecha { get; set; }
    public decimal Total { get; set; }

    public Clientes? _Cliente { get; set; }
    public List<Detalles> Detalles { get; set; }
}

public class Detalles
{
    public int Id { get; set; }
    public int Factura { get; set; }
    public int Producto { get; set; }
    public decimal Cantidad { get; set; }
    public decimal Total { get; set; }

    public Facturas? _Factura { get; set; }
    public Productos? _Producto { get; set; }
}
```

### Uso en código (Main / servicios)

- Instanciar con `new T()` + asignación de propiedades, o inicializador de objeto `new T() { Prop = valor }`.
- Colección de trabajo: `List<T>` + `.Add(...)`.
- Búsqueda: `.FirstOrDefault(x => x.Campo == valor)`, siempre validando `== null` antes de usar el resultado.

```csharp
var producto = lista_productos.FirstOrDefault(x => x.Codigo == "P001");
if (producto == null)
    Console.WriteLine("No se encontró");
else
    Console.WriteLine("Nombre: " + producto.Nombre);
```

## Estado

- **Entregable 1:** modelado de las 20 entidades en `Program.cs` (consola). ✔
- **Entregable 2:**
  - [x] Solución con los 3 proyectos en `backend/`
  - [x] `Script.sql` con las 20 tablas y sus semillas (probado en SQL Server Express)
  - [x] `IConexion`, `Conexion` y `nucleo/Datosgenerales`
  - [x] 5 entidades con flujo completo (entidad + `DbSet` + consola + prueba): `Cargos`, `Sucursales`, `Empleados`, `Categorias`, `Plataformas` — 5/5 pruebas correctas
  - [ ] 15 entidades restantes (ver [Cómo agregar una entidad](#cómo-agregar-una-entidad-a-la-solución))
