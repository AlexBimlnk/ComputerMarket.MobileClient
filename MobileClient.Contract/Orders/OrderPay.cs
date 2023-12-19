using Newtonsoft.Json;

namespace MobileClient.Contract.Orders;
public sealed class OrderPay
{
    [JsonProperty("account")]
    public string Account { get; set; } = default!;
}
