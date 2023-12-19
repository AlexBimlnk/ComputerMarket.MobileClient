using MobileClient.Contract.Builder;
using MobileClient.Logic.Account;
using MobileClient.Logic.Basket;
using MobileClient.Logic.Builder;

namespace MobileClient.UI;

public partial class MainPage : ContentPage
{
    int count = 0;

    private readonly ILoginHandler _loginHandler; // login works
    private readonly IBasketAccessor _basketAccessor; // all works
    private readonly IBuilderAccessor _builderAccessor;

    public MainPage(
        ILoginHandler loginHandler,
        IBasketAccessor basketAccessor,
        IBuilderAccessor builderAccessor)
    {
        _loginHandler = loginHandler ?? throw new ArgumentNullException(nameof(loginHandler));
        _basketAccessor = basketAccessor ?? throw new ArgumentNullException(nameof(basketAccessor));
        _builderAccessor = builderAccessor ?? throw new ArgumentNullException(nameof(builderAccessor));

        InitializeComponent();
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

    private async void OnCounterClickedAsync(object sender, EventArgs e)
    {
        count++;

        await _loginHandler.LogInAsync(new Contract.AccountController.Login
        {
            Email = "centuriin@yandex.ru",
            Password = "12345678"
        });

        //await TestBasketAsync();
        await TestBuilderAsync();

        if (count == 1)
            CounterBtn.Text = $"Clicked {count} time";
        else
            CounterBtn.Text = $"Clicked {count} times";

        SemanticScreenReader.Announce(CounterBtn.Text);

    }
}