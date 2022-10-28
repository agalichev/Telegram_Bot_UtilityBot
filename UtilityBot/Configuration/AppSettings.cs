using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UtilityBot.Configuration
{   
    /// <summary>
    /// Данные конфигурации приложения
    /// </summary>
    /// <param name="BotToken">Специальный ключ для авторизации бота в Telegram</param>
    public class AppSettings
    {
        public string? BotToken { get; set; }

    }
}
