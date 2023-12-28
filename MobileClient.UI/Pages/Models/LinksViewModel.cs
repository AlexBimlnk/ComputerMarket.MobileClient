using MobileClient.Contract.Links;
using MobileClient.Logic.Links;

namespace MobileClient.UI.Pages.Models;

public class LinksViewModel
{
    private readonly ILinksAccessor _linksAccessor;
    public LinksViewModel(ILinksAccessor linksAccessor)
    {
        _linksAccessor = linksAccessor;
    }

    public ObservableCollection<Link> Items { get; set; } = new();

    public async Task ReloadDataAsync()
    {
        var result = await _linksAccessor.GetLinksAsync();
        Items.Clear();

        foreach (var pr in result)
        {
            Items.Add(pr);
        }

    }
}
