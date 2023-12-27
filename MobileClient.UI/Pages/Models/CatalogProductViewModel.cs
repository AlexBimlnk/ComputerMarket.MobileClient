using System.ComponentModel;
using System.Runtime.CompilerServices;

using MobileClient.Contract;
using MobileClient.Logic.Basket;

namespace MobileClient.UI.Pages.Models;
public class CatalogProductViewModel : IQueryAttributable, INotifyPropertyChanged
{
    private readonly IBasketAccessor _basket;
    public CatalogProductViewModel(IBasketAccessor basket)
    {
        _basket = basket;
    }

    public async Task UpdateBasketStateAsync()
    {
        var items = await _basket.GetPurchasableEntitiesAsync();
        var check = items.Where(
            x => x.Product.Item.Key.Value == Product.Item.Key.Value &&
            x.Product.Provider.Key.Value == Product.Provider.Key.Value
        );

        if (!check.Any() || check.FirstOrDefault().Quantity == 0)
        {
            BasketState = "Нет в корзине";
        }
        else
        {
            BasketState = $"В корзине: {check.FirstOrDefault().Quantity}";
        }
        OnPropertyChanged(nameof(BasketState));
    }

    public string BasketState { get; set; }

    public Product Product { get; set; }

    public ObservableCollection<ItemProperty> Properties { get; set; } = new();

    public event PropertyChangedEventHandler PropertyChanged;

    public async void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        Product = query[nameof(Product)] as Product;
        await UpdateBasketStateAsync();

        Properties.Clear();
        foreach (var property in Product.Item.Properties)
        {
            Properties.Add(property);
        }

        OnPropertyChanged(nameof(Product));
        OnPropertyChanged(nameof(Properties));
    }

    public async Task AddToBasketAsync()
    {
        await _basket.AddOrIncreaseToBasketAsync(Product.Provider.Key.Value, Product.Item.Key.Value);
        await Shell.Current.GoToAsync("//basket", true, new Dictionary<string, object>());
    }

    public void OnPropertyChanged([CallerMemberName] string prop = "") => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
}
