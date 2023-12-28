using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

using MobileClient.Contract;
using MobileClient.Contract.Orders;
using MobileClient.Logic.Providers;

namespace MobileClient.UI.Pages.Models;

public class AgentsViewModel : IQueryAttributable, INotifyPropertyChanged
{
    private readonly IProviderAccessor _providerAccessor;

    public AgentsViewModel(IProviderAccessor providerAccessor)
    {
        
            _providerAccessor = providerAccessor;
    }

    public Provider Provider { get; set; }

    public ObservableCollection<User> Items { get; set; } = new();

    public event PropertyChangedEventHandler PropertyChanged;

    public async void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        Provider = query[nameof(Provider)] as Provider;



        OnPropertyChanged(nameof(Provider));

        await ReloadDataAsync();
    }

    public async Task ReloadDataAsync()
    {
        var result = await _providerAccessor.GetAgentsByProviderIdAsync(Provider.Key.Value);
        Items.Clear();

        foreach (var pr in result)
        {
            Items.Add(pr);
        }

        OnPropertyChanged(nameof(Items));
    }

    public ICommand ItemChangedCommand => new Command<User>(
        async (item) => await GotoSubAsync(item ?? throw new ArgumentNullException())
    );


    private async Task GotoSubAsync(User item)
    {
        var answer = await Application.Current.MainPage.DisplayAlert("Удаление агента", "Вы уверены?", "Да", "Нет");

        if (!answer)
        {
            return;
        }

        await _providerAccessor.RemoveAgentByProviderIdAsync(Provider.Key.Value, item.Key.Value);
        await ReloadDataAsync();
    }

    public async Task AddNewAsync()
    {
        var result = await Application.Current.MainPage.DisplayPromptAsync("Добавить нового", "Введите email агента:");


        await _providerAccessor.AddAgentsByProviderIdAsync(Provider.Key.Value, new Contract.Providers.NewAgent
        {
            Email = result
        });
        await ReloadDataAsync();
    }

    public void OnPropertyChanged([CallerMemberName] string prop = "") => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
}
