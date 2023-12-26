using MobileClient.Contract.Links;
using MobileClient.Logic.Links;

namespace MobileClient.UI.Pages;

public partial class LinkPage : ContentPage
{
    private readonly ILinksAccessor _linksAccessor;
    public LinkPage(ILinksAccessor linksAccessor)
	{
        _linksAccessor = linksAccessor;

        InitializeComponent();
	}

    public ObservableCollection<Link> Links { get; set; } = new();

    protected async override void OnNavigatedTo(NavigatedToEventArgs args)
    {
        var task = await _linksAccessor.GetLinksAsync();

        Links.Clear();

        foreach (var pr in task)
        {
            Links.Add(pr);
        }
    }

    private async void SaveButtonClickAsync(object sender, EventArgs e)
    {
        
    }
}