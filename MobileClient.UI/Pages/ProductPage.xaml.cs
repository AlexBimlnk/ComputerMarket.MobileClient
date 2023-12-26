using MobileClient.Contract;
using MobileClient.Logic.Basket;

namespace MobileClient.UI.Pages;

[QueryProperty("Product", "Product")]
public partial class ProductPage : ContentPage
{
	public ProductPage(IBasketAccessor basket)
	{
		InitializeComponent();
        BindingContext = this;
	}

    public BindableProperty ProductName = BindableProperty.Create(nameof(ProductName), typeof(string), typeof(ProductPage));
    public BindableProperty ProviderName = BindableProperty.Create(nameof(ProductName), typeof(string), typeof(ProductPage));
    public BindableProperty Image = BindableProperty.Create(nameof(ProductName), typeof(string), typeof(ProductPage));


    protected override void OnNavigatedTo(NavigatedToEventArgs args)
    {
        SetValue(ProductName, Product.Item.Name);
        SetValue(Image, Product.Item.URL);
        SetValue(ProviderName, Product.Provider.Name);
    }

    public Product Product
    {
        get;
        set;
    }
}