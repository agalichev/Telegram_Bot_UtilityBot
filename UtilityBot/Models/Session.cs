using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UtilityBot.Models
{   
    /// <summary>
    /// Упрощенная версия хранилища данных пользовательских сессий
    /// </summary>
    /// <param name="OperationCode">Значение параметра CallBack экранной кнопки</param>>
    public class Session
    {
        public string? OperationCode { get; set; }
    }
}
