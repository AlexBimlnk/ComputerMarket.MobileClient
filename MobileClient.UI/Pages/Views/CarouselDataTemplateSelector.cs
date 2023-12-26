namespace MobileClient.UI.Pages.Views;

public class CarouselDataTemplateSelector : DataTemplateSelector
{
    public DataTemplate Tab1 { get; set; }
    public DataTemplate Tab2 { get; set; }

    public CarouselDataTemplateSelector()
    {
        Tab1 = new DataTemplate(typeof(ManagerUserView));
        Tab2 = new DataTemplate(typeof(SimpleUserView));
    }

    protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
    {
        var position = $"{item}";

        if (position == "1")
            return Tab1;

        return Tab2;
    }
}
