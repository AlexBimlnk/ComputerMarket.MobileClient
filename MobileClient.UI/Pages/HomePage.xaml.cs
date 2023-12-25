
using System.Threading.Tasks;
using System.Windows.Input;

using MobileClient.Contract;
using MobileClient.Contract.Builder;
using MobileClient.Contract.Products;
using MobileClient.Logic.Account;
using MobileClient.Logic.Basket;
using MobileClient.Logic.Builder;
using MobileClient.Logic.Links;
using MobileClient.Logic.Orders;
using MobileClient.Logic.Products;
using MobileClient.Logic.Providers;

using static AndroidX.ConstraintLayout.Core.Motion.Utils.HyperSpline;


namespace MobileClient.UI.Pages;

public partial class HomePage : ContentPage
{
    private readonly ISignInManager _loginHandler; // login works
    private readonly IProductsAccessor _productsAccessor;
    

    public HomePage(
        ISignInManager loginHandler,
        IProductsAccessor productsAccessor)
    {
        _loginHandler = loginHandler ?? throw new ArgumentNullException(nameof(loginHandler));
        _productsAccessor = productsAccessor ?? throw new ArgumentNullException(nameof(productsAccessor));

        InitializeComponent();
        BindingContext = this;
    }

    public ObservableCollection<ItemType> Categories { get; set; } = new();

    /*public async Task TestBasketAsync()
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
    }*/

    public ICommand ItemChangedCommand => new Command<ItemType>(
        async (item) => await HomePage.GoToCatalogAsync(item ?? throw new ArgumentNullException())
    );


    protected override async void OnNavigatedTo(NavigatedToEventArgs args)
    {
        var types = await _productsAccessor.GetCategoriesAsync();
        Categories.Clear();

        foreach (var pr in types)
        {
            Categories.Add(pr);
        }
    }

    private static async Task GoToCatalogAsync(ItemType type)
    {
        await Shell.Current.GoToAsync("///catalog", true, new Dictionary<string, object>
        {
            ["Catalog"] = new Catalog()
            {
                TypeId = type.Id
            }
        });
    }
}