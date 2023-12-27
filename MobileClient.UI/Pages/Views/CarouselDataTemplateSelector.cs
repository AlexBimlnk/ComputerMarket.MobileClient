namespace MobileClient.UI.Pages.Views;

public class CarouselDataTemplateSelector : DataTemplateSelector
{
    public DataTemplate Tab1 { get; set; }
    public DataTemplate Tab2 { get; set; }
    public DataTemplate Tab3 { get; set; }

    public CarouselDataTemplateSelector()
    {
        Tab1 = new DataTemplate(typeof(ManagerProfileView));
        Tab2 = new DataTemplate(typeof(CustomerProfileView));
        Tab3 = new DataTemplate(typeof(AgentProfileView));
    }

    protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
    {
        var position = $"{item}";

        if (position == "1")
            return Tab1;

        if (position == "3")
            return Tab3;

        return Tab2;
    }
}
