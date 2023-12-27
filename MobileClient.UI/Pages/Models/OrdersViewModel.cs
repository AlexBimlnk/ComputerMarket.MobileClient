using MobileClient.Contract.Orders;
using MobileClient.Logic.Orders;

namespace MobileClient.UI.Pages.Models;

public class OrdersViewModel
{
    private readonly IOrdersAccessor _ordersAccessor;
    public OrdersViewModel(IOrdersAccessor accessor)
    {
        _ordersAccessor = accessor;
    }

    public ObservableCollection<Order> Orders { get; set; } = new();

    public async Task ReloadDataAsync()
    {
        var result = await _ordersAccessor.GetOrdersAsync();
        Orders.Clear();

        foreach (var pr in result)
        {
            Orders.Add(pr);
        }
    }

}
