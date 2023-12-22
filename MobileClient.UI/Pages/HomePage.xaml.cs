
using MobileClient.Contract.Builder;
using MobileClient.Logic.Account;
using MobileClient.Logic.Basket;
using MobileClient.Logic.Builder;
using MobileClient.Logic.Links;
using MobileClient.Logic.Orders;

using MobileClient.UI.Messages;

namespace MobileClient.UI.Pages;

public partial class HomePage : ContentPage
{
    private readonly ILoginHandler _loginHandler; // login works
    private readonly IBasketAccessor _basketAccessor; // all works
    private readonly IBuilderAccessor _builderAccessor;
    private readonly IOrdersAccessor _ordersAccessor;
    private readonly ILinksAccessor _linksAccessor;

       
    public HomePage(
        ILoginHandler loginHandler,
        IBasketAccessor basketAccessor,
        IBuilderAccessor builderAccessor,
        IOrdersAccessor ordersAccessor,
        ILinksAccessor linksAccessor
    )
    {
        _loginHandler = loginHandler ?? throw new ArgumentNullException(nameof(loginHandler));
        _basketAccessor = basketAccessor ?? throw new ArgumentNullException(nameof(basketAccessor));
        _builderAccessor = builderAccessor ?? throw new ArgumentNullException(nameof(builderAccessor));
        _ordersAccessor = ordersAccessor ?? throw new ArgumentNullException(nameof(ordersAccessor));
        _linksAccessor = linksAccessor ?? throw new ArgumentNullException(nameof(linksAccessor));
        
        InitializeComponent();

        WeakReferenceMessenger.Default.Register<AddProductMessage>(this, (r, m) =>
        {
            NavSubContentAsync(m.Value);
        });
    }

    public async Task TestBasketAsync()
    {
        var result = await _basketAccessor.GetPurchasableEntitiesAsync(); //work

        await _basketAccessor.AddOrIncreaseToBasketAsync(1, 10); // work
        await _basketAccessor.AddOrIncreaseToBasketAsync(1, 10); // work

        await _basketAccessor.DecreaseInBasketAsync(1, 10);

        await _basketAccessor.DeleteFromBasketAsync(1, 10);

        await _basketAccessor.AddOrIncreaseToBasketAsync(1, 10); // work
    }

    public async Task TestBuilderAsync()
    {
        var goodRequest = new RequestBuild
        {
            Processor = "AMD Ryzen 5 5600X BOX",
            MotherBoard = "GIGABYTE B550 AORUS ELITE V2"
        };

        var badRequest = new RequestBuild
        {
            Processor = "Intel Core i7-13700K BOX",
            MotherBoard = "GIGABYTE B550 AORUS ELITE V2"
        };

        var good = await _builderAccessor.GetBuildResultAsync(goodRequest); //ok
        var bad = await _builderAccessor.GetBuildResultAsync(badRequest); // 500 error need check market
    }

    public async Task TestOrdersAsync()
    {
        var result = await _ordersAccessor.GetOrdersAsync();
        var r2 = await _ordersAccessor.GetOrderByIdAsync(1);
    }

    public async Task TestLinksAsync()
    {
        var result = await _linksAccessor.GetLinksAsync();
    }

    private void MenuFlyoutItem_ParentChanged(System.Object sender, System.EventArgs e)
    {
        if (sender is BindableObject bo)
            bo.BindingContext = BindingContext;
    }



    public async void NavSubContentAsync(bool show)
    {
        await _loginHandler.LogInAsync(new MobileClient.Contract.AccountController.Login
        {
            Email = "centuriin@yandex.ru",
            Password = "12345678"
        });

        await TestBasketAsync();

        var displayWidth = DeviceDisplay.Current.MainDisplayInfo.Width;

        if (show)
        {
            var addForm = new AddProductView();
            PageGrid.Add(addForm, 1);
            Grid.SetRowSpan(addForm, 3);
            // translate off screen right
            addForm.TranslationX = displayWidth - addForm.X;
            addForm.TranslateTo(0, 0, 800, easing: Easing.CubicOut);
        }
        else
        {
            // remove the product window

            var view = (AddProductView)PageGrid.Children.Where(v => v.GetType() == typeof(AddProductView)).SingleOrDefault();

            var x = DeviceDisplay.Current.MainDisplayInfo.Width;
            view.TranslateTo(displayWidth - view.X, 0, 800, easing: Easing.CubicIn);

            if (view != null)
                PageGrid.Children.Remove(view);

        }
    }
}