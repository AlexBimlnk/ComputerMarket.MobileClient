using MobileClient.Contract.Products;
using MobileClient.Contract;
using MobileClient.Logic.Products;
using Android.Webkit;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace MobileClient.UI.Pages.Models;

public class CatalogViewModel: IQueryAttributable, INotifyPropertyChanged
{
    private readonly IProductsAccessor _productsAccessor;
    private static Catalog s_catalog = new();

    public event PropertyChangedEventHandler PropertyChanged;

    public CatalogViewModel(IProductsAccessor productsAccessor)
    {
            _productsAccessor = productsAccessor ?? throw new ArgumentNullException();
    }
#pragma warning disable CA1822 // Mark members as static
    public Catalog Catalog { get => s_catalog; set => s_catalog = value; }
#pragma warning restore CA1822 // Mark members as static

    public ObservableCollection<Product> Products { get; set; } = new();

    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        Catalog = query["Catalog"] as Catalog;
        OnPropertyChanged("Catalog");
    }
    public async Task ReloadDataAsync()
    {
        var result = await _productsAccessor.GetCatalogAsync(Catalog);
        Products.Clear();

        foreach (var pr in result.Products)
        {
            Products.Add(pr);
        }
    }

    public void OnPropertyChanged([CallerMemberName] string prop = "") => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
}
