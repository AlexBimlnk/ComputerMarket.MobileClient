using MobileClient.Logic;

namespace MobileClient.UI;

public partial class MainPage : ContentPage
{
    int count = 0;

    private readonly IHttpClientFacade _httpClientFacade;

    public MainPage(IHttpClientFacade facade)
    {
        _httpClientFacade = facade ?? throw new ArgumentNullException(nameof(facade));
        InitializeComponent();
    }

    private void OnCounterClicked(object sender, EventArgs e)
    {
        count++;

        if (count == 1)
            CounterBtn.Text = $"Clicked {count} time";
        else
            CounterBtn.Text = $"Clicked {count} times";

        SemanticScreenReader.Announce(CounterBtn.Text);
    }
}