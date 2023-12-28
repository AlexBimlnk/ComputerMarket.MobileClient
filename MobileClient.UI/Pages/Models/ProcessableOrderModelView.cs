using System.ComponentModel;
using System.Runtime.CompilerServices;
using MobileClient.Contract.BasketController;

using MobileClient.Contract.Orders;
using MobileClient.Logic.Account;
using MobileClient.Logic.Orders;
using MobileClient.Logic.Providers;

namespace MobileClient.UI.Pages.Models;

public class ProcessableOrderModelView : IQueryAttributable, INotifyPropertyChanged
{
    private readonly ISignInManager _signInManager;
    private readonly IProviderAccessor _providerAccessor;
    private readonly IOrdersAccessor _ordersAccessor;

    public ProcessableOrderModelView(ISignInManager signInManager, IProviderAccessor providerAccessor, IOrdersAccessor ordersAccessor)
    {
        _signInManager = signInManager;
        _providerAccessor = providerAccessor;
        _ordersAccessor = ordersAccessor;
    }

    public Order Order { get; set; }
    public ObservableCollection<PurchasableEntity> Products { get; set; } = new();
    public bool ReadyVisible { get; set; } = false;
    public bool RecieveVisible { get; set; } = false;
    public bool CancelVisible { get; set; } = false;
    public bool AproveVisible { get; set; } = false;


    public event PropertyChangedEventHandler PropertyChanged;

    public void Update()
    {
        ReadyVisible = false; RecieveVisible = false; AproveVisible = false; CancelVisible = false;
        if (Order.State == Contract.OrderState.PaymentWait)
        {

        }
        else if (Order.State == Contract.OrderState.ProviderAnswerWait)
        {
            CancelVisible = true;
            AproveVisible = true;
        }
        else if (Order.State == Contract.OrderState.ProductDeliveryWait)
        {
            ReadyVisible = true;
        }
        else if (Order.State == Contract.OrderState.Ready)
        {
            RecieveVisible = true;
        }

        Products.Clear();
        foreach (var item in Order.Items)
        {
            Products.Add(item);
        }

        OnPropertyChanged(nameof(Products));
        OnPropertyChanged(nameof(ReadyVisible));
        OnPropertyChanged(nameof(RecieveVisible));
        OnPropertyChanged(nameof(AproveVisible));
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

        await _providerAccessor.DeclineOrderRelatedWithAuthProviderByIdAsync(Order.Key.Value);
    }

    public async Task ReadyAsync()
    {
        var answer = await Application.Current.MainPage.DisplayAlert("Прибытие заказа", "Заказ прибыл?", "Да", "Нет");

        if (!answer)
        {
            return;
        }

        await _ordersAccessor.ReadyOrder(Order.Key.Value);
    }

    public async Task RecieveyAsync()
    {
        var answer = await Application.Current.MainPage.DisplayAlert("Получение заказа", "Заказ был получен?", "Да", "Нет");

        if (!answer)
        {
            return;
        }

        await _ordersAccessor.ReceiveOrder(Order.Key.Value);
    }

    public async Task AproveAsync()
    {
        var answer = await Application.Current.MainPage.DisplayAlert("Отправление заказа", "Заказ был отправлен?", "Да", "Нет");

        if (!answer)
        {
            return;
        }

        await _providerAccessor.ReadyOrderRelatedWithAuthProviderByIdAsync(Order.Key.Value);
    }

    public void OnPropertyChanged([CallerMemberName] string prop = "") => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
}