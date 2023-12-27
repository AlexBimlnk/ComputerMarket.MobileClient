using CommunityToolkit.Maui.Markup;

using MobileClient.UI.Pages.Models;

namespace MobileClient.UI.Pages;

public class HomePageView : ContentPage
{
    public HomePageView(HomeViewModel model)
    {
        Title = "Главная";
        BindingContext = model;
        var label = new Label
        {
            Text = "Категории",
            FontSize = 22,
            HorizontalOptions = LayoutOptions.Start,
            Margin = 20,
            FontAttributes = FontAttributes.Bold
        };
        var view = new CarouselView().ItemsSource(model.Categories);
        view.ItemTemplate = new DataTemplate
        {
            LoadTemplate = () =>
            {
                var title = new Label
                {
                    FontAttributes = FontAttributes.Bold,
                    FontSize = 18,
                    HorizontalOptions = LayoutOptions.Center,
                    VerticalOptions = LayoutOptions.Center
                }.Bind(Label.TextProperty, "Name");
                var image = new Image
                {
                    Aspect = Aspect.AspectFill,
                    HeightRequest = 200,
                    HorizontalOptions = LayoutOptions.Center
                }.Bind(Image.SourceProperty, "URL");

                var button = new Button
                {
                    Text = "Перейти",
                    Command = model.ItemChangedCommand,
                    HorizontalOptions = LayoutOptions.Start,
                    BackgroundColor = Colors.DarkBlue,
                    Margin = 10
                }
                .Bind(Button.CommandParameterProperty, Binding.SelfPath);

                var data = new StackLayout()
                {
                    Children = { title, image, button }
                };

                var frame = new Frame()
                {
                    HasShadow = true,
                    Margin = 20,
                    HeightRequest = 350,
                    HorizontalOptions = LayoutOptions.Center,
                    VerticalOptions = LayoutOptions.Center,
                    Content = data

                    /* Unmerged change from project 'MobileClient.UI (net8.0-maccatalyst)'
                    Before:
                                    };



                                    return frame;
                    After:
                                    };



                                    return frame;
                    */

                    /* Unmerged change from project 'MobileClient.UI (net8.0-ios)'
                    Before:
                                    };



                                    return frame;
                    After:
                                    };



                                    return frame;
                    */

                    /* Unmerged change from project 'MobileClient.UI (net8.0-windows10.0.19041.0)'
                    Before:
                                    };



                                    return frame;
                    After:
                                    };



                                    return frame;
                    */
                };



                return frame;
            }
        };

        Content = new StackLayout
        {
            Children = { label, view }
        };
    }

    protected override async void OnNavigatedTo(NavigatedToEventArgs args) => await (BindingContext as HomeViewModel).ReloadDataAsync();
}