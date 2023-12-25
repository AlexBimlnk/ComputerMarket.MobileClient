namespace MobileClient.UI.Pages.Views;

public class CarouselDataTemplateSelector : DataTemplateSelector
{
    public DataTemplate Tab1 { get; set; }
    public DataTemplate Tab2 { get; set; }

    public CarouselDataTemplateSelector()
    {
        Tab1 = new DataTemplate(typeof(ProfileInfoView));
        Tab2 = new DataTemplate(typeof(LoginView));
    }

    protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
    {
        var position = $"{item}";

        if (position == "1")
            return Tab1;

        return Tab2;
    }
}
