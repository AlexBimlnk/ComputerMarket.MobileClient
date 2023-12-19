using MobileClient.Logic;
using MobileClient.Logic.Account;

namespace MobileClient.UI;

public partial class MainPage : ContentPage
{
    int count = 0;

    private readonly ILoginHandler _loginHandler; // login works
    private readonly IBasketAccessor _basketAccessor; // all works

    public MainPage(
        ILoginHandler loginHandler,
        IBasketAccessor basketAccessor)
    {
        _loginHandler = loginHandler ?? throw new ArgumentNullException(nameof(loginHandler));
        _basketAccessor = basketAccessor ?? throw new ArgumentNullException(nameof(basketAccessor));

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

    private async void OnCounterClickedAsync(object sender, EventArgs e)
    {
        count++;

        await _loginHandler.LogInAsync(new Contract.AccountController.Login
        {
            Email = "centuriin@yandex.ru",
            Password = "12345678"
        });

        //await TestBasketAsync();
        

        if (count == 1)
            CounterBtn.Text = $"Clicked {count} time";
        else
            CounterBtn.Text = $"Clicked {count} times";

        SemanticScreenReader.Announce(CounterBtn.Text);

    }
}