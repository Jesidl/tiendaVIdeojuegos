# Tienda de Videojuegos

Proyecto académico de programación en C# / .NET, desarrollado siguiendo la rúbrica del profesor.

## Requisitos

- [.NET SDK 10.0](https://dotnet.microsoft.com/download) o superior

## Cómo ejecutar

```bash
dotnet run
```

## Estándar de código

Estándar extraído de los ejemplos del profesor. Para esta entrega **no se usa herencia, polimorfismo ni interfaces** — solo modelado de entidades planas.

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

Proyecto en construcción — estructura inicial del proyecto de consola.
