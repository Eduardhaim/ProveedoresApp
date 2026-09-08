using SQLite;
using ProveedoresApp.Models;

namespace ProveedoresApp.Services;

public class ProveedorService
{
    private SQLiteAsyncConnection? _database;

    private async Task InicializarBaseDatos()
    {
        if (_database != null)
            return;

        string ruta = Path.Combine(
            FileSystem.AppDataDirectory,
            "proveedores.db3"
        );

        _database = new SQLiteAsyncConnection(ruta);

        await _database.CreateTableAsync<Proveedor>();
    }

    public async Task<List<Proveedor>> ObtenerProveedoresAsync()
    {
        await InicializarBaseDatos();

        return await _database!
            .Table<Proveedor>()
            .OrderBy(p => p.Nombre)
            .ToListAsync();
    }

    public async Task<int> GuardarProveedorAsync(Proveedor proveedor)
    {
        await InicializarBaseDatos();

        if (proveedor.Id == 0)
        {
            return await _database!.InsertAsync(proveedor);
        }

        return await _database!.UpdateAsync(proveedor);
    }

    public async Task<int> EliminarProveedorAsync(Proveedor proveedor)
    {
        await InicializarBaseDatos();

        return await _database!.DeleteAsync(proveedor);
    }
}