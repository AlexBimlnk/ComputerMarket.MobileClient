
using MobileClient.Contract;
using MobileClient.Contract.Products;
using MobileClient.Logic.Products;

namespace MobileClient.UI.Pages;

[QueryProperty("Catalog", "Catalog")]
public partial class FilterPage : ContentPage
{
    private readonly IProductsAccessor _productsAccessor;
	public FilterPage(IProductsAccessor productsAccessor)
	{
        _productsAccessor = productsAccessor;

        InitializeComponent();
        BindingContext = this;
    }

    public Catalog Catalog { get; set; } = new();

    protected override void OnNavigatedTo(NavigatedToEventArgs args)
    {
        
    }

    private async void OpenButtonClickAsync(object sender, EventArgs e)
    {
        Catalog.SearchString = searchEntry.Text;
        await ApplyAsync();
    }

    public async Task ApplyAsync()
    {
        await Shell.Current.GoToAsync("///catalog", true, new Dictionary<string, object>
        {
            ["Catalog"] = Catalog
        });
    }
}