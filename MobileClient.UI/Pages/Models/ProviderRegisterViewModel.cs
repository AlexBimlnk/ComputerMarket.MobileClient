using MobileClient.Logic.Providers;

namespace MobileClient.UI.Pages.Models;

public class ProviderRegisterViewModel
{
    private readonly IProviderAccessor _providerAccessor;

    public ProviderRegisterViewModel(IProviderAccessor provider)
    {
        _providerAccessor = provider;
    }

    public string INN { get; set; }
    public string Name { get; set; }
    public string Account { get; set; }

    public async Task RegisterAsync() => await _providerAccessor.RegisterAsync(new Contract.Providers.ProviderRegister
    {
        Name = Name,
        INN = INN,
        BankAccount = Account
    });
}