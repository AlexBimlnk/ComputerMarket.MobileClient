using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Newtonsoft.Json;

namespace MobileClient.Contract;
public sealed class Margin
{
    [JsonProperty("value")]
    public decimal Value { get; set; }
}
