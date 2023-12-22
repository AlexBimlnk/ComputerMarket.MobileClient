namespace MobileClient.UI.Pages;

[INotifyPropertyChanged]
public partial class DashboardViewModel
{
    [RelayCommand]
    public static async Task ViewAllAsync() => await Application.Current.MainPage.DisplayAlert("Not Implemented", "Wouldn't it be nice tho?", "Okay");
}

