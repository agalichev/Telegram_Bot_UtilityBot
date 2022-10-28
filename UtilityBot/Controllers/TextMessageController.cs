using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Globalization;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using UtilityBot.Services;

namespace UtilityBot.Controllers
{   /// <summary>
    /// Контроллер для обработки текстовых сообщений
    /// </summary>
    /// <param name="_botOperationHandler">Вызов сервиса для обработки операций бота над текстовыми сообщениями</param>
    /// <param name="_memoryStorage">Используем данные пользовательской сессии</param>
    internal class TextMessageController
    {
        private readonly IStorage _memoryStorage;
        private readonly ITelegramBotClient _telegramBotClient;
        private readonly IOperationHandler _botOperationHandler;

        public TextMessageController(ITelegramBotClient telegramBotClient, IOperationHandler botOperationHandler, IStorage memoryStorgae)
        {
            _telegramBotClient = telegramBotClient;
            _botOperationHandler = botOperationHandler;
            _memoryStorage = memoryStorgae;
        }

        public async Task Handle(Message message, CancellationToken ct)
        {
            var messageText = message.Text;
            var userOperationCode = _memoryStorage.GetSession(message.Chat.Id).OperationCode;

            switch (message.Text)
            {
                case "/start":

                    var buttons = new List<InlineKeyboardButton[]>();
                    buttons.Add(new[]
                    { 
                        InlineKeyboardButton.WithCallbackData($"Сумма чисел", $"Sum"),
                        InlineKeyboardButton.WithCallbackData($"Количество символов", $"Length")
                    });

                    await _telegramBotClient.SendTextMessageAsync(message.Chat.Id, $"<b>Я могу вычислить сумму чисел{Environment.NewLine}" +
                        $"или посчитать количество символов в строке{Environment.NewLine}</b>",
                        cancellationToken: ct, parseMode: ParseMode.Html,
                        replyMarkup: new InlineKeyboardMarkup(buttons));
                    break;

                default:
                    if(String.IsNullOrEmpty(userOperationCode))
                    {
                        await _telegramBotClient.SendTextMessageAsync(message.Chat.Id, $"Вы не выбрали операцию. Не знаю, что мне делать.", cancellationToken: ct);
                        break;
                    }
                    var answer = _botOperationHandler.Operate(messageText, userOperationCode);
                        await _telegramBotClient.SendTextMessageAsync(message.Chat.Id, answer, cancellationToken: ct);
                    break;
            }
        }
    }
}
