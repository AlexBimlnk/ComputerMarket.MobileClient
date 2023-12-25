using MobileClient.Contract;

namespace MobileClient.UI.Pages;

[QueryProperty("Product", "Product")]
public partial class ProductPage : ContentPage
{
	public ProductPage()
	{
		InitializeComponent();
	}

    public Product Product
    {
        get => BindingContext as Product;
        set => BindingContext = value;
    }
}