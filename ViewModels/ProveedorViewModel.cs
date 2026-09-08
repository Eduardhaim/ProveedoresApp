using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ProveedoresApp.Models;
using ProveedoresApp.Services;

namespace ProveedoresApp.ViewModels;

public partial class ProveedorViewModel : ObservableObject
{
    private readonly ProveedorService _proveedorService;

    public ObservableCollection<Proveedor> Proveedores { get; } = new();

    [ObservableProperty]
    private int id;

    [ObservableProperty]
    private string nombre = string.Empty;

    [ObservableProperty]
    private string empresa = string.Empty;

    [ObservableProperty]
    private string telefono = string.Empty;

    [ObservableProperty]
    private string correo = string.Empty;

    [ObservableProperty]
    private string direccion = string.Empty;

    [ObservableProperty]
    private string rtn = string.Empty;

    [ObservableProperty]
    private string mensaje = string.Empty;

    [ObservableProperty]
    private string tituloFormulario = "Nuevo proveedor";

    public ProveedorViewModel(ProveedorService proveedorService)
    {
        _proveedorService = proveedorService;

        _ = CargarProveedoresAsync();
    }

    [RelayCommand]
    private async Task CargarProveedoresAsync()
    {
        var lista = await _proveedorService.ObtenerProveedoresAsync();

        Proveedores.Clear();

        foreach (var proveedor in lista)
        {
            Proveedores.Add(proveedor);
        }
    }

    [RelayCommand]
    private async Task GuardarAsync()
    {
        if (string.IsNullOrWhiteSpace(Nombre))
        {
            Mensaje = "Debe ingresar el nombre.";
            return;
        }

        if (string.IsNullOrWhiteSpace(Empresa))
        {
            Mensaje = "Debe ingresar la empresa.";
            return;
        }

        bool editando = Id != 0;

        var proveedor = new Proveedor
        {
            Id = Id,
            Nombre = Nombre,
            Empresa = Empresa,
            Telefono = Telefono,
            Correo = Correo,
            Direccion = Direccion,
            Rtn = Rtn
        };

        await _proveedorService.GuardarProveedorAsync(proveedor);

        if (editando)
        {
            Mensaje = "Proveedor actualizado correctamente.";
        }
        else
        {
            Mensaje = "Proveedor agregado correctamente.";
        }

        LimpiarCampos();

        await CargarProveedoresAsync();
    }

    [RelayCommand]
    private void Editar(Proveedor proveedor)
    {
        Id = proveedor.Id;
        Nombre = proveedor.Nombre;
        Empresa = proveedor.Empresa;
        Telefono = proveedor.Telefono;
        Correo = proveedor.Correo;
        Direccion = proveedor.Direccion;
        Rtn = proveedor.Rtn;

        TituloFormulario = "Editar proveedor";
        Mensaje = "Modifique los datos y presione Guardar.";
    }

    [RelayCommand]
    private async Task EliminarAsync(Proveedor proveedor)
    {
        await _proveedorService.EliminarProveedorAsync(proveedor);

        if (Id == proveedor.Id)
        {
            LimpiarCampos();
        }

        Mensaje = "Proveedor eliminado correctamente.";

        await CargarProveedoresAsync();
    }

    [RelayCommand]
    private void Nuevo()
    {
        LimpiarCampos();
        Mensaje = string.Empty;
    }

    private void LimpiarCampos()
    {
        Id = 0;
        Nombre = string.Empty;
        Empresa = string.Empty;
        Telefono = string.Empty;
        Correo = string.Empty;
        Direccion = string.Empty;
        Rtn = string.Empty;

        TituloFormulario = "Nuevo proveedor";
    }
}