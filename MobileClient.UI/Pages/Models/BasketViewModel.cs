using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

using MobileClient.Contract.BasketController;
using MobileClient.Logic.Basket;
using MobileClient.Logic.Builder;

namespace MobileClient.UI.Pages.Models;

public class BasketViewModel : INotifyPropertyChanged
{
    private readonly IBasketAccessor _accessor;
    private readonly IBuilderAccessor _builderAccessor;

    public event PropertyChangedEventHandler PropertyChanged;

    public BasketViewModel(IBasketAccessor basketAccessor, IBuilderAccessor builderAccessor)
    {
        _builderAccessor = builderAccessor;
        _accessor = basketAccessor;
    }

    public ObservableCollection<PurchasableEntity> Products { get; set; } = new();

    public bool CanCheck { get; set; } = false;

    public string CheckResult { get; set; } = "";

    public async Task ReloadDataAsync()
    {
        CheckResult = "";
        CanCheck = true;
        var result = await _accessor.GetPurchasableEntitiesAsync();
        Products.Clear();

        var check = new Dictionary<int, int>();

        foreach (var pr in result)
        {
            if (!check.ContainsKey(pr.Product.Item.Type.Id) && CanCheck)
            {
                check[pr.Product.Item.Type.Id] = 1;
            }
            else
            {
                CanCheck = false;
            }
            
            Products.Add(pr);
        }

        if (!Products.Any() || Products.Count == 1)
        {
            CanCheck = false;
        }

        OnPropertyChanged(nameof(CanCheck));
        OnPropertyChanged(nameof(CheckResult));
    }

    public async Task CheckAsync()
    {
        var result = await _accessor.GetPurchasableEntitiesAsync();

        var proc = result.Where(x => x.Product.Item.Type.Id == 1).SingleOrDefault();
        var board = result.Where(x => x.Product.Item.Type.Id == 4).SingleOrDefault();
        var noData = proc is null || board is null;

        if (noData) 
        {
            CheckResult = "Не выполнена: не хватает данных";
            OnPropertyChanged(nameof(CheckResult));
            return;
        }

        try
        {
            var some = await _builderAccessor.GetBuildResultAsync(new Contract.Builder.RequestBuild
            {
                Processor = proc.Product.Item.Name,
                MotherBoard = board.Product.Item.Name
            });
            if (some.IsSucces)
            {
                CheckResult = "Успех: Сборка совместима";
            }
            else
            {
                CheckResult = "Ошибка: ";

                foreach (var error in some.ErrorsByType)
                {
                    CheckResult += error.Key;
                }
            }
        }
        catch (Exception ex) 
        {
            CheckResult = "Не выполнена: не хватает данных";
        }

        OnPropertyChanged(nameof(CheckResult));
    }

    public async Task CreateAsync()
    {
        var items = await _accessor.GetPurchasableEntitiesAsync();

        var toOrder = items.Select(x => (x.Product.Item.Key, x.Product.Provider.Key)).ToHashSet();

        await _accessor.CreateOrderAsync(toOrder);
        await ReloadDataAsync();
    }

#pragma warning disable CA1822 // Mark members as static
    public ICommand AddCommand => new Command<PurchasableEntity>(
        async (item) => await ChangeDeltaAsync(1, item ?? throw new ArgumentNullException())
    );
    public ICommand RemoveCommand => new Command<PurchasableEntity>(
        async (item) => await ChangeDeltaAsync(-1, item ?? throw new ArgumentNullException())
    );
    public ICommand DeleteCommand => new Command<PurchasableEntity>(
        async (item) => await ChangeDeltaAsync(0, item ?? throw new ArgumentNullException())
    );
#pragma warning restore CA1822 // Mark members as static

    private async Task ChangeDeltaAsync(int delta, PurchasableEntity entity)
    {
        if (delta < 0)
        {
            await _accessor.DecreaseInBasketAsync(entity.Product.Provider.Key.Value, entity.Product.Item.Key.Value);
        }
        else if (delta > 0)
        {
            await _accessor.AddOrIncreaseToBasketAsync(entity.Product.Provider.Key.Value, entity.Product.Item.Key.Value);
        }
        else
        {
            await _accessor.DeleteFromBasketAsync(entity.Product.Provider.Key.Value, entity.Product.Item.Key.Value);
        }
        await ReloadDataAsync();
    }

    public void OnPropertyChanged([CallerMemberName] string prop = "") => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
}
