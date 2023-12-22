using System.ComponentModel;

namespace MobileClient.UI.Models
{
    public enum OrderType
    {
        [Description("Dine In")]
        DineIn,
        [Description("Carry Out")]
        CarryOut,
        [Description("Delivery")]
        Delivery
    }
}

