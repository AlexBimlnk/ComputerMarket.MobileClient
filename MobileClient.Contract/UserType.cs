using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileClient.Contract;
public enum UserType
{
    /// <summary xml:lang = "ru">
    /// Обычный пользователь системы.
    /// </summary>
    Customer = 1,

    /// <summary xml:lang = "ru">
    /// Представитель.
    /// </summary>
    Agent = 2,

    /// <summary xml:lang = "ru">
    /// Менеджер.
    /// </summary>
    Manager = 3
}
