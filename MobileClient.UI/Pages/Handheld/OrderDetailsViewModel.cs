namespace MobileClient.UI.Pages.Handheld;

[INotifyPropertyChanged]
[QueryProperty("Order", "Order")]
[QueryProperty("Added", "Added")]
public partial class OrderDetailsViewModel
{
    [ObservableProperty]
    private Order _order;

    [ObservableProperty]
    private Item _added;

    [RelayCommand]
    private async Task PayAsync()
    {
        try
        {
            var navigationParameter = new Dictionary<string, object>
            {
                { "Order", _order }
            };
            await Shell.Current.GoToAsync($"{nameof(TipPage)}", navigationParameter);
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
        }
    }

    [RelayCommand]
    private static async Task AddAsync() => await Task.Run(() => Task.CompletedTask);// await Shell.Current.GoToAsync($"{nameof(ScanPage)}");
}