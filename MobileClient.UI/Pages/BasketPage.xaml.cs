using MobileClient.Contract.Products;
using MobileClient.Contract;
using MobileClient.Logic.Account;
using MobileClient.Logic.Products;
using MobileClient.Logic.Basket;
using MobileClient.Contract.BasketController;
using System.Windows.Input;

namespace MobileClient.UI.Pages;

public partial class BasketPage : ContentPage
{
    private readonly IBasketAccessor _accessor;
    
    public BasketPage(IBasketAccessor basket)
    {
        InitializeComponent();
        _accessor = basket ?? throw new ArgumentNullException(nameof(basket));
        BindingContext = this;
    }

    public ObservableCollection<PurchasableEntity> Products { get; set; } = new();

    private static int GetCurrentType() => 1;

    protected override async void OnNavigatedTo(NavigatedToEventArgs args) => await UpdateBasketAsync();

    private async Task UpdateBasketAsync()
    {
        var task = await _accessor.GetPurchasableEntitiesAsync();

        Products.Clear();

        foreach (var pr in task)
        {
            Products.Add(pr);
        }
    }

#pragma warning disable CA1822 // Mark members as static
    public ICommand AddCommand => new Command<PurchasableEntity>(
        async (item) => await ChangeDeltaAsync(1, item ?? throw new ArgumentNullException())
    );
    public ICommand RemoveCommand => new Command<PurchasableEntity>(
        async (item) => await ChangeDeltaAsync(-1, item ?? throw new ArgumentNullException())
    );
    public ICommand DeleteCommand => new Command<PurchasableEntity>(
        async (item) => await ChangeDeltaAsync(0, item ?? throw new ArgumentNullException())
    );
#pragma warning restore CA1822 // Mark members as static

    private async Task ChangeDeltaAsync(int delta, PurchasableEntity entity)
    {
        if (delta < 0)
        {
            await _accessor.DecreaseInBasketAsync(entity.Product.Provider.Key.Value, entity.Product.Item.Key.Value);
        }
        else if (delta > 0)
        {
            await _accessor.AddOrIncreaseToBasketAsync(entity.Product.Provider.Key.Value, entity.Product.Item.Key.Value);
        }
        else
        {
            await _accessor.DeleteFromBasketAsync(entity.Product.Provider.Key.Value, entity.Product.Item.Key.Value);
        }
        await UpdateBasketAsync();
    }

    private async void SaveButtonClickAsync(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(FilterPage), true, new Dictionary<string, object>
        {
            
        });
    }
}