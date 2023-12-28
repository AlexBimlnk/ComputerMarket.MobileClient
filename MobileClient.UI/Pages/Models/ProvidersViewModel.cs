using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

using Android.Webkit;

using MobileClient.Contract;
using MobileClient.Contract.Products;
using MobileClient.Logic.Providers;

namespace MobileClient.UI.Pages.Models;
public class ProvidersViewModel
{
    private IProviderAccessor _providerAccessor;
    public ProvidersViewModel(IProviderAccessor providerAccessor)
    {
        _providerAccessor = providerAccessor;
    }

    public ObservableCollection<Provider> Items { get; set; } = new();

    public async Task ReloadDataAsync()
    {
        var result = await _providerAccessor.GetProvidersAsync();
        Items.Clear();

        foreach (var pr in result)
        {
            Items.Add(pr);
        }
    }

#pragma warning disable CA1822 // Mark members as static
    public ICommand ItemChangedCommand => new Command<Provider>(
        async (item) => await GotoSubAsync(item ?? throw new ArgumentNullException())
    );
    public ICommand GoTo => new Command<Provider>(
        async (item) => await GotoAsync(item ?? throw new ArgumentNullException())
    );
#pragma warning restore CA1822 // Mark members as static

    private static async Task GotoSubAsync(Provider item)
    {
        await Shell.Current.GoToAsync(nameof(ProviderPageView), true, new Dictionary<string, object>
        {
            ["Provider"] = item
        });
    }

    private static async Task GotoAsync(Provider item)
    {
        await Shell.Current.GoToAsync(nameof(AgentsPageView), true, new Dictionary<string, object>
        {
            ["Provider"] = item
        });
    }
}
