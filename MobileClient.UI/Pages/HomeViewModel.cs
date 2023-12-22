namespace MobileClient.UI.Pages;

[INotifyPropertyChanged]
public partial class HomeViewModel
{
    [ObservableProperty]
    private ObservableCollection<Item> _products;

    [ObservableProperty]
    private string _category = ItemCategory.Noodles.ToString();

    partial void OnCategoryChanged(string cat)
    {
        ItemCategory category = (ItemCategory)Enum.Parse(typeof(ItemCategory), cat);
        _products = new ObservableCollection<Item>(
            AppData.Items.Where(x => x.Category == category).ToList()
        );
        OnPropertyChanged(nameof(Products));
    }

    public HomeViewModel()
    {
        _products = new ObservableCollection<Item>(
            AppData.Items.Where(x => x.Category == ItemCategory.Noodles).ToList()
        );
    }

    [RelayCommand]
    private async Task PreferencesAsync() => await Shell.Current.GoToAsync($"{nameof(SettingsPage)}?sub=appearance");

    [RelayCommand]
    private void AddProduct() => MessagingCenter.Send<HomeViewModel, string>(this, "action", "add");
}