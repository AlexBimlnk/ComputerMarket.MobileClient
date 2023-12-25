using CommunityToolkit.Maui.Core.Extensions;

using MobileClient.Contract;
using MobileClient.Logic.Account;
using MobileClient.Logic.Products;
using MobileClient.UI.Pages.Viewsl;

namespace MobileClient.UI.Pages;

public partial class CatalogPage : ContentPage
{


    private readonly IProductsAccessor _productsAccessor;
    private readonly ISignInManager _manager;
    public CatalogPage(IProductsAccessor productsAccessor, ISignInManager manager)
	{
        InitializeComponent();
        _manager = manager;
        _productsAccessor = productsAccessor ?? throw new ArgumentNullException(nameof(productsAccessor));
        BindingContext = this;
	}

    public ObservableCollection<Product> Products { get; set; } = new();

    private static int GetCurrentType() => 1;

    protected override async void OnNavigatedTo(NavigatedToEventArgs args)
    {
        var task = await _productsAccessor.GetCatalogAsync(new Contract.Products.Catalog()
        {
            TypeId = GetCurrentType()
        });


        Products.Clear();

        foreach(var pr in task.Products)
        {
            Products.Add(pr);
        }
    }

    public async void OnCollectionViewSelectionChangedAsync(object sender, SelectionChangedEventArgs e) 
    {
        if (e.CurrentSelection.FirstOrDefault() is not Product item)
            return;
        _manager.IsLoggedIn = !_manager.IsLoggedIn;
        await Shell.Current.GoToAsync(nameof(ProductPage), true, new Dictionary<string, object>
        {
            ["Product"] = item
        });
    }
}