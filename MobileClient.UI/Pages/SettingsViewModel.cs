namespace MobileClient.UI.Pages;

[INotifyPropertyChanged]
public partial class SettingsViewModel
{
    [ObservableProperty]
    private ObservableCollection<Item> _products;

    public SettingsViewModel()
    {
        _products = new ObservableCollection<Item>(
            AppData.Items
        );
    }

    [RelayCommand]
    private static async Task NotImplementedAsync() => await Application.Current.MainPage.DisplayAlert("Not Implemented", "Wouldn't it be nice tho?", "Okay");
}

