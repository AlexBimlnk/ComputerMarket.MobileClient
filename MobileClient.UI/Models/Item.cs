namespace MobileClient.UI.Models;

[INotifyPropertyChanged]
public partial class Item
{
    [ObservableProperty]
    private string _title;

    [ObservableProperty]
    private int _quantity;

    [ObservableProperty]
    private string _image;

    [ObservableProperty]
    private double _price;

    partial void OnQuantityChanged(int value)
    {
        OnPropertyChanged(nameof(SubTotal));
    }

    public ItemCategory Category { get; set; }

    public double SubTotal => Price * Quantity;
}