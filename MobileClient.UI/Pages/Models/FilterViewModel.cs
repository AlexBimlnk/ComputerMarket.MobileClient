
using System.ComponentModel;
using System.Runtime.CompilerServices;

using MobileClient.Contract;
using MobileClient.Contract.Products;
using MobileClient.Logic.Products;

namespace MobileClient.UI.Pages.Models;

public class FilterViewModel: IQueryAttributable, INotifyPropertyChanged
{
    private readonly IProductsAccessor _productsAccessor;

    public FilterViewModel(IProductsAccessor productsAccessor)
    {
        _productsAccessor = productsAccessor ?? throw new ArgumentNullException();
    }

    public event PropertyChangedEventHandler PropertyChanged;

    public Catalog Catalog { get; set; } = new();

    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        Catalog = query["Catalog"] as Catalog;
        OnPropertyChanged("Catalog");
    }

    public void OnPropertyChanged([CallerMemberName] string prop = "") => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
}
