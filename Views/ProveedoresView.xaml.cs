using ProveedoresApp.Services;
using ProveedoresApp.ViewModels;

namespace ProveedoresApp.Views;

public partial class ProveedoresView : ContentPage
{
    public ProveedoresView()
    {
        InitializeComponent();

        var proveedorService = new ProveedorService();

        BindingContext = new ProveedorViewModel(proveedorService);
    }
}