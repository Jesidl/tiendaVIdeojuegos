using lib_aplicaciones.entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace lib_aplicaciones.interfaces
{
    public interface IConexion
    {
        string? StringConexion { get; set; }

        DbSet<Cargos>? Cargos { get; set; }
        DbSet<Sucursales>? Sucursales { get; set; }
        DbSet<Empleados>? Empleados { get; set; }
        DbSet<Categorias>? Categorias { get; set; }
        DbSet<Plataformas>? Plataformas { get; set; }

        EntityEntry<T> Entry<T>(T entity) where T : class;
        int SaveChanges();
    }
}
