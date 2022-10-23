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
    internal class InlineKeyboardController
    {
        private readonly IStorage _memoryStorage;
        private readonly ITelegramBotClient _telegramBotClient;

        public InlineKeyboardController(ITelegramBotClient telegramBotClient, IStorage memoryStorage)
        {
            _telegramBotClient = telegramBotClient;
            _memoryStorage = memoryStorage;
        }

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

            await _telegramBotClient.SendTextMessageAsync(callbackQuery.From.Id, $"<b>Я буду выполнять операцию - {operationText}.{Environment.NewLine}</b>"
                + $"Вы можете поменять её в главном меню.", cancellationToken: ct, parseMode: ParseMode.Html);
        }
    }
}
