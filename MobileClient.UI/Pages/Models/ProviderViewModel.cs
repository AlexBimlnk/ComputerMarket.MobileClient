using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

using MobileClient.Contract;
using MobileClient.Logic.Providers;

namespace MobileClient.UI.Pages.Models;
public class ProviderViewModel : IQueryAttributable, INotifyPropertyChanged
{
    private readonly IProviderAccessor _providerAccessor;

    public ProviderViewModel(IProviderAccessor providerAccessor)
    {
        _providerAccessor = providerAccessor;
    }

    public Provider Provider { get; set; }

    public string NewMargin { get; set; } = "0";

    public bool IsNeedAprove { get; set; } = false;

    public event PropertyChangedEventHandler PropertyChanged;

    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        Provider = query[nameof(Provider)] as Provider;

        NewMargin = Provider.Margin.Value.ToString();
        IsNeedAprove = !Provider.IsAproved;
        OnPropertyChanged(nameof(Provider));
        OnPropertyChanged(nameof(IsNeedAprove));
        OnPropertyChanged(nameof(NewMargin));
    }

    public void Update()
    {
        var pr = _providerAccessor;
    }

    public async Task AproveProviderAsync() => await _providerAccessor.AproveProviderAsync(new Contract.Providers.EditProvider
    {
        INN = Provider.PaymentTransactionsInformation.INN,
        Key = Provider.Key.Value,
        Name = Provider.Name,
        BankAccount = Provider.PaymentTransactionsInformation.BankAccount,
        Margin = "0,2",
        IsAproved = Provider.IsAproved,
    });

    public async Task SaveInfoAsync() => await _providerAccessor.EditProviderAssync(new Contract.Providers.EditProvider
    {
        INN = Provider.PaymentTransactionsInformation.INN,
        Key = Provider.Key.Value,
        Name = Provider.Name,
        BankAccount = Provider.PaymentTransactionsInformation.BankAccount,
        Margin = "0,3",
        IsAproved = Provider.IsAproved,
    });

    public void OnPropertyChanged([CallerMemberName] string prop = "") => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
}
