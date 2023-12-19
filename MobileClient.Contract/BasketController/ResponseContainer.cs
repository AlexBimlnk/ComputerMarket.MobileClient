using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileClient.Contract.BasketController;
public class ResponseContainer
{
    public IReadOnlyCollection<PurchasableEntity> PurchasableEntities { get; set; }
}
