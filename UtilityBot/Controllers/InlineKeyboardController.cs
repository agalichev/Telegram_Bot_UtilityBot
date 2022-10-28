using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using UtilityBot.Services;

namespace UtilityBot.Controllers
{
    /// <summary>
    /// Контроллер для обработки нажатий на кнопки 
    /// </summary>
    /// <param name="_telegramBotClient">Для подключения к Telegram Bot API</param>
    /// <param name="_memoryStorage">Для обновления данных пользовательской сессии</param>    
    internal class InlineKeyboardController
    {
        private readonly IStorage _memoryStorage;
        private readonly ITelegramBotClient _telegramBotClient;

        public InlineKeyboardController(ITelegramBotClient telegramBotClient, IStorage memoryStorage)
        {
            _telegramBotClient = telegramBotClient;
            _memoryStorage = memoryStorage;
        }

        /// <summary>
        ///  Асинхронная задача обработки нажатий на кнопки
        /// </summary>
        /// <param name="operationText">Содержит расшифровку значения параметра Callback</param>
        public async Task Handle(CallbackQuery? callbackQuery, CancellationToken ct)
        {
            if (callbackQuery?.Data == null) 
                return;

            _memoryStorage.GetSession(callbackQuery.From.Id).OperationCode = callbackQuery.Data;

            string operationText = callbackQuery.Data switch
            {
                "Sum" => "Сумма чисел",
                "Length" => "Длина строки",
                _ => String.Empty
            };

            /// <summary>
            /// Сообщение ботом о том, что он будет выполнять
            /// </summary>
            await _telegramBotClient.SendTextMessageAsync(callbackQuery.From.Id, $"<b>Я буду выполнять операцию - {operationText}.{Environment.NewLine}</b>"
                + $"Вы можете поменять её в главном меню.", cancellationToken: ct, parseMode: ParseMode.Html);
        }
    }
}
