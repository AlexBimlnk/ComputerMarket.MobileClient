using Microcharts;

using SkiaSharp.Views.Maui;

namespace MobileClient.UI.Pages;

public partial class DashboardPage : ContentPage
{
    public DashboardPage()
    {
        InitializeComponent();
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        DrawChart();
    }

    private void DrawChart()
    {
        TypesChart.Chart = new RadialGaugeChart
        {
            LabelTextSize = 24,
            BackgroundColor = Colors.Transparent.ToSKColor(),
            IsAnimated = true,
            Entries = new List<ChartEntry>
            {
                new ChartEntry(10)
                {
                    Color = Color.FromArgb("#EA7C69").ToSKColor()
                },
                new ChartEntry(51)
                {
                    Color = Color.FromArgb("#50D1AA").ToSKColor()
                },
                new ChartEntry(14)
                {
                    Color = Color.FromArgb("#FF7CA3").ToSKColor()
                },new ChartEntry(2)
                {
                    Color = Colors.WhiteSmoke.ToSKColor()
                }
            }
        };


    }

    private async void TapGestureRecognizer_TappedAsync(System.Object sender, System.EventArgs e) => 
        await DisplayAlert("Filter", "You are looking for a filter dialog that isn't yet implemented. Come back later", "Okay");
}
