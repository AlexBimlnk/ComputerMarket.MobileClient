namespace MobileClient.UI.Pages.Views;

[INotifyPropertyChanged]
public partial class OrderCartViewModel
{
    [ObservableProperty]
    private Order _order;

    public OrderCartViewModel()
    {
        Order = AppData.Orders.First();
    }

    [RelayCommand]
    public static async Task PlaceOrderAsync() => await Application.Current.MainPage.DisplayAlert("Not Implemented", "Wouldn't it be cool tho?", "Okay");
}