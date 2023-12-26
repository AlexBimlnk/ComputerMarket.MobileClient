using System.Windows.Input;

using MobileClient.Contract.BasketController;
using MobileClient.Logic.Basket;

namespace MobileClient.UI.Pages.Models;

public class BasketViewModel
{
    private readonly IBasketAccessor _accessor;
    public BasketViewModel(IBasketAccessor basketAccessor)
    {
        _accessor = basketAccessor;
    }

    public ObservableCollection<PurchasableEntity> Products { get; set; } = new();

    public async Task ReloadDataAsync()
    {
        var result = await _accessor.GetPurchasableEntitiesAsync();
        Products.Clear();

        foreach (var pr in result)
        {
            Products.Add(pr);
        }
    }

    public async Task CreateAsync()
    {
        var items = await _accessor.GetPurchasableEntitiesAsync();

        var toOrder = items.Select(x => (x.Product.Item.Key, x.Product.Provider.Key)).ToHashSet();

        await _accessor.CreateOrderAsync(toOrder);
        await ReloadDataAsync();
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
        await ReloadDataAsync();
    }
}
