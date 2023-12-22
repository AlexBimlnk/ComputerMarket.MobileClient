using MobileClient.UI.Messages;

namespace MobileClient.UI.Pages.Handheld;

[INotifyPropertyChanged]
[QueryProperty("Order", "Order")]
public partial class SignatureViewModel
{
    [ObservableProperty]
    private Order _order;

    [RelayCommand]
    private async Task DoneAsync()
    {
        WeakReferenceMessenger.Default.Send<SaveSignatureMessage>(
            new SaveSignatureMessage(_order.Table)
        );

        var navigationParameter = new Dictionary<string, object>
        {
            { "Order", _order }
        };
        await Shell.Current.GoToAsync($"{nameof(ReceiptPage)}", navigationParameter);
    }

    [RelayCommand]
    private static void Clear()
    {
        WeakReferenceMessenger.Default.Send<ClearSignatureMessage>(
            new ClearSignatureMessage(true)
        ); ;
    }
}