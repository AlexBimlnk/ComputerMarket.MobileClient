namespace MobileClient.UI.Pages.Handheld;

[INotifyPropertyChanged]
[QueryProperty("Order", "Order")]
public partial class ReceiptViewModel
{
    [ObservableProperty]
    private Order _order;

    [RelayCommand]
    private static async Task DoneAsync() => await Shell.Current.GoToAsync("///orders");
}