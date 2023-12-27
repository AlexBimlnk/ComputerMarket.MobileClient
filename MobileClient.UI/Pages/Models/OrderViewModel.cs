using System.ComponentModel;
using System.Runtime.CompilerServices;

using MobileClient.Contract.BasketController;



/* Unmerged change from project 'MobileClient.UI (net8.0-maccatalyst)'
Before:
using MobileClient.Contract.Orders;
using MobileClient.Logic.Orders;
using Microsoft.Maui.Controls;
using MobileClient.Contract;
After:
using MobileClient.Contract.Controls;
using MobileClient.Contract;
using MobileClient.Contract.BasketController;
using MobileClient.Contract.Orders;
*/

/* Unmerged change from project 'MobileClient.UI (net8.0-ios)'
Before:
using MobileClient.Contract.Orders;
using MobileClient.Logic.Orders;
using Microsoft.Maui.Controls;
using MobileClient.Contract;
After:
using MobileClient.Contract.Controls;
using MobileClient.Contract;
using MobileClient.Contract.BasketController;
using MobileClient.Contract.Orders;
*/

/* Unmerged change from project 'MobileClient.UI (net8.0-windows10.0.19041.0)'
Before:
using MobileClient.Contract.Orders;
using MobileClient.Logic.Orders;
using Microsoft.Maui.Controls;
using MobileClient.Contract;
After:
using MobileClient.Contract.Controls;
using MobileClient.Contract;
using MobileClient.Contract.BasketController;
using MobileClient.Contract.Orders;
*/
using MobileClient.Contract.Orders;
using MobileClient.Logic.Orders;

namespace MobileClient.UI.Pages.Models;
public class OrderViewModel : IQueryAttributable, INotifyPropertyChanged
{
    private readonly IOrdersAccessor _ordersAccessor;
    public OrderViewModel(IOrdersAccessor ordersAccessor)
    {
        _ordersAccessor = ordersAccessor;
    }

    public Order Order { get; set; }
    public ObservableCollection<PurchasableEntity> Products { get; set; } = new();
    public bool PayVisible { get; set; } = true;
    public bool CancelVisible { get; set; } = true;

    public event PropertyChangedEventHandler PropertyChanged;

    public void Update()
    {
        PayVisible = true;
        CancelVisible = true;
        if (Order.State == Contract.OrderState.PaymentWait)
        {

        }
        else if (Order.State is not (Contract.OrderState.Received or Contract.OrderState.Cancel))
        {
            PayVisible = false;
        }
        else
        {
            PayVisible = false;
            CancelVisible = false;
        }
        Products.Clear();
        foreach (var item in Order.Items)
        {
            Products.Add(item);
        }

        OnPropertyChanged(nameof(Products));
        OnPropertyChanged(nameof(PayVisible));
        OnPropertyChanged(nameof(CancelVisible));
    }

    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        Order = query[nameof(Order)] as Order;
        OnPropertyChanged(nameof(Order));
        Update();
    }

    public async Task CancelAsync()
    {
        var answer = await Application.Current.MainPage.DisplayAlert("Отмена заказа", "Вы уверены?", "Да", "Нет");

        if (!answer)
        {
            return;
        }

        await _ordersAccessor.CancelOrderByIdAsync(Order.Key.Value);
    }

    public async Task PayAsync()
    {
        var result = await Application.Current.MainPage.DisplayPromptAsync("Оплата заказа", "Введите номер счёта:");

        result = "12345123451234512345";

        await _ordersAccessor.PayOrderAsync(Order.Key.Value, new OrderPay
        {
            Account = result
        });
    }

    public void OnPropertyChanged([CallerMemberName] string prop = "") => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
}
