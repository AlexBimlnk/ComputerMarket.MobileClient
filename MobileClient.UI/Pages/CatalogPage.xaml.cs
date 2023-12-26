using CommunityToolkit.Maui.Core.Extensions;

using MobileClient.Contract;
using MobileClient.Contract.Products;
using MobileClient.Logic.Account;
using MobileClient.Logic.Products;

namespace MobileClient.UI.Pages;

[QueryProperty("Catalog", "Catalog")]
public partial class CatalogPage : ContentPage
{
    private readonly IProductsAccessor _productsAccessor;
    private readonly ISignInManager _manager;

    private static Catalog s_catalog = new();

    public CatalogPage(IProductsAccessor productsAccessor, ISignInManager manager)
	{
        InitializeComponent();
        _manager = manager;
        _productsAccessor = productsAccessor ?? throw new ArgumentNullException(nameof(productsAccessor));
        BindingContext = this;
	}

    public static Catalog Catalog { get => s_catalog; set => s_catalog = value; }

    public ObservableCollection<Product> Products { get; set; } = new();

    private static int GetCurrentType() => 1;

    protected override async void OnNavigatedTo(NavigatedToEventArgs args)
    {
        var task = await _productsAccessor.GetCatalogAsync(s_catalog);

        Products.Clear();

        foreach(var pr in task.Products)
        {
            Products.Add(pr);
        }
    }

    private async void SaveButtonClickAsync(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(FilterPage), true, new Dictionary<string, object>
        {
            ["Catalog"] = Catalog
        });
    }

    public async void OnCollectionViewSelectionChangedAsync(object sender, SelectionChangedEventArgs e) 
    {
        if (e.CurrentSelection.FirstOrDefault() is not Product item)
            return;
        await _manager.LogOutAsync();
        await Shell.Current.GoToAsync(nameof(ProductPage), true, new Dictionary<string, object>
        {
            ["Product"] = item
        });
    }
}