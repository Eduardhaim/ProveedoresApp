using SQLite;

namespace ProveedoresApp.Models;

public class Proveedor
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }

    public string Nombre { get; set; } = string.Empty;

    public string Empresa { get; set; } = string.Empty;

    public string Telefono { get; set; } = string.Empty;

    public string Correo { get; set; } = string.Empty;

    public string Direccion { get; set; } = string.Empty;

    public string Rtn { get; set; } = string.Empty;
}