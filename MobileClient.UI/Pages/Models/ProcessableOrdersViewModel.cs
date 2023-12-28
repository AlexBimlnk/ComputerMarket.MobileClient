using MobileClient.Contract;
using System.ComponentModel;

using MobileClient.Logic.Account;
using MobileClient.Logic.Orders;
using MobileClient.Logic.Providers;
using MobileClient.Contract.Orders;

namespace MobileClient.UI.Pages.Models;

public class ProcessableOrdersViewModel
{
    private readonly ISignInManager _signInManager;
    private readonly IProviderAccessor _providerAccessor;
    private readonly IOrdersAccessor _ordersAccessor;

    public ProcessableOrdersViewModel(ISignInManager signInManager, IProviderAccessor providerAccessor, IOrdersAccessor ordersAccessor)
    {
        _signInManager = signInManager;
        _providerAccessor = providerAccessor;
        _ordersAccessor = ordersAccessor;
    }

    public ObservableCollection<Order> Items { get; set; } = new();

    private async Task<IReadOnlyCollection<Order>> GetDependOnUserAsync()
    {
        var user = await _signInManager.GetCurrentUserAsync();

        if (user.Type == UserType.Manager)
        {
            return await _ordersAccessor.GetOrdersForAproveAsync();
        }
        return (await _providerAccessor.GetOrdersRelatedWithAuthProviderAsync()).Where(x => x.State == OrderState.ProviderAnswerWait).ToHashSet();
    }

    public async Task ReloadDataAsync()
    {
        var result = await GetDependOnUserAsync();
        Items.Clear();

        foreach (var pr in result)
        {
            Items.Add(pr);
        }
    }
}
