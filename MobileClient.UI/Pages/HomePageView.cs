using System.Windows.Input;

using CommunityToolkit.Maui.Markup;

using MobileClient.Contract;
using MobileClient.Contract.Products;
using MobileClient.UI.Pages.Models;

namespace MobileClient.UI.Pages;

public class HomePageView : ContentPage
{
	public HomePageView(HomeViewModel model)
	{
        BindingContext = model;
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
                    HeightRequest = 150,
                    WidthRequest = 150,
                    HorizontalOptions = LayoutOptions.Center
                }.Bind(Image.SourceProperty, "URL");

                var button = new Button
                {
                    Text = "Перейти",
                    Command = model.ItemChangedCommand
                }
                .Bind(Button.CommandParameterProperty, Binding.SelfPath);

                var data = new StackLayout()
                {
                    Children = { title, image, button }
                };

                var result = new Frame()
                {
                    HasShadow = true,
                    Margin = 20,
                    HeightRequest = 300,
                    HorizontalOptions = LayoutOptions.Center,
                    VerticalOptions = LayoutOptions.Center,
                    Content = data
                };

                return result;
            }
        };

        Content = view;
	}

    protected override async void OnNavigatedTo(NavigatedToEventArgs args) => await (BindingContext as HomeViewModel).ReloadDataAsync();
}