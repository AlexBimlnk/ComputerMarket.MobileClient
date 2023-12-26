using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

using MobileClient.Contract;
using MobileClient.Contract.Products;
using MobileClient.Logic.Basket;

namespace MobileClient.UI.Pages.Models;
public class CatalogProductViewModel: IQueryAttributable, INotifyPropertyChanged
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

        if (!check.Any()|| check.FirstOrDefault().Quantity == 0)
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

    public event PropertyChangedEventHandler PropertyChanged;

    public async void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        Product = query[nameof(Product)] as Product;
        await UpdateBasketStateAsync();
        OnPropertyChanged(nameof(Product));
    }

    public async Task AddToBasketAsync()
    {
        await _basket.AddOrIncreaseToBasketAsync(Product.Provider.Key.Value, Product.Item.Key.Value);
        await Shell.Current.GoToAsync("//basket", true, new Dictionary<string, object>());
    }

    public void OnPropertyChanged([CallerMemberName] string prop = "") => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
}
