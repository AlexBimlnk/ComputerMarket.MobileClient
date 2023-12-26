using MobileClient.Contract;
using MobileClient.Contract.Products;
using MobileClient.Logic.Basket;

namespace MobileClient.UI.Pages;

[QueryProperty("Product", "Product")]
public partial class ProductPage : ContentPage
{
    private readonly IBasketAccessor _basket;

    public ProductPage(IBasketAccessor basket)
	{
        _basket = basket;
		InitializeComponent();
	}

    private async void SaveButtonClickAsync(object sender, EventArgs e)
    {
        await _basket.AddOrIncreaseToBasketAsync(Product.Provider.Key.Value, Product.Item.Key.Value);
        await Shell.Current.GoToAsync("//basket", true, new Dictionary<string, object>());
    }

    public Product Product
    {
        get => BindingContext as Product;
        set => BindingContext = value ;
    }
}