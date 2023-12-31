﻿using MobileClient.Contract.Builder;
using MobileClient.Logic.Account;
using MobileClient.Logic.Basket;
using MobileClient.Logic.Builder;
using MobileClient.Logic.Links;
using MobileClient.Logic.Orders;
using MobileClient.Logic.Products;
using MobileClient.Logic.Providers;

namespace MobileClient.UI;

public partial class MainPage : ContentPage
{
    int count = 0;

    private readonly ILoginHandler _loginHandler; // login works
    private readonly IBasketAccessor _basketAccessor; // all works
    private readonly IBuilderAccessor _builderAccessor;
    private readonly IOrdersAccessor _ordersAccessor;
    private readonly ILinksAccessor _linksAccessor;
    private readonly IProductsAccessor _productsAccessor;
    private readonly IProviderAccessor _providerAccessor;

    public MainPage(
        ILoginHandler loginHandler,
        IBasketAccessor basketAccessor,
        IBuilderAccessor builderAccessor,
        IOrdersAccessor ordersAccessor,
        ILinksAccessor linksAccessor,
        IProductsAccessor productsAccessor,
        IProviderAccessor providerAccessor)
    {
        _loginHandler = loginHandler ?? throw new ArgumentNullException(nameof(loginHandler));
        _basketAccessor = basketAccessor ?? throw new ArgumentNullException(nameof(basketAccessor));
        _builderAccessor = builderAccessor ?? throw new ArgumentNullException(nameof(builderAccessor));
        _ordersAccessor = ordersAccessor ?? throw new ArgumentNullException(nameof(ordersAccessor));
        _linksAccessor = linksAccessor ?? throw new ArgumentNullException(nameof(linksAccessor));
        _productsAccessor = productsAccessor ?? throw new ArgumentNullException(nameof(productsAccessor));
        _providerAccessor = providerAccessor ?? throw new ArgumentNullException(nameof(providerAccessor));

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

    public async Task TestOrdersAsync()
    {
        var result = await _ordersAccessor.GetOrdersAsync();
        var r2 = await _ordersAccessor.GetOrderByIdAsync(1);
    }

    public async Task TestLinksAsync()
    {
        var result = await _linksAccessor.GetLinksAsync();
    }

    public async Task TestProductsAsync()
    {
        var categories = await _productsAccessor.GetCategoriesAsync();

        var product = await _productsAccessor.GetProductAsync(1, 1);

        var catalog = await _productsAccessor.GetCatalogAsync(new Contract.Products.Catalog
        {
            TypeId = 1, // processor
        });
    }

    public async Task TestProviderAsync()
    {
        var a = await _providerAccessor.GetOrdersRelatedWithAuthProviderAsync();
    }

    private async void OnCounterClickedAsync(object sender, EventArgs e)
    {
        count++;

        await _loginHandler.LogInAsync(new Contract.AccountController.Login
        {
            Email = "agent@mail.ru",
            Password = "12345678"
        });

        //await TestBasketAsync();
        //await TestBuilderAsync();
        //await TestOrdersAsync();
        //await TestLinksAsync();

        //await TestProductsAsync();

        await TestProviderAsync();

        if (count == 1)
            CounterBtn.Text = $"Clicked {count} time";
        else
            CounterBtn.Text = $"Clicked {count} times";

        SemanticScreenReader.Announce(CounterBtn.Text);

    }
}