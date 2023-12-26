using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

using MobileClient.Contract;
using MobileClient.Contract.Products;
using MobileClient.Logic.Products;

namespace MobileClient.UI.Pages.Models;
public class HomeViewModel
{
    private readonly IProductsAccessor _productsAccessor;
    public HomeViewModel(IProductsAccessor productsAccessor)
    {
        _productsAccessor  = productsAccessor ?? throw new ArgumentNullException();
    }

    public ObservableCollection<ItemType> Categories { get; set; } = new();

    public async Task ReloadDataAsync()
    {
        var result = await _productsAccessor.GetCategoriesAsync();
        Categories.Clear();

        foreach (var pr in result)
        {
            Categories.Add(pr);
        }
    }

#pragma warning disable CA1822 // Mark members as static
    public ICommand ItemChangedCommand => new Command<ItemType>(
        async (item) => await GoToCatalogAsync(item ?? throw new ArgumentNullException())
    );
#pragma warning restore CA1822 // Mark members as static

    private static async Task GoToCatalogAsync(ItemType type)
    {
        await Shell.Current.GoToAsync("///catalog", true, new Dictionary<string, object>
        {
            ["Catalog"] = new Catalog()
            {
                TypeId = type.Id
            }
        });
    }

}
