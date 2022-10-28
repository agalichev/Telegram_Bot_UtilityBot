using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Extensions.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Microsoft.Extensions.Hosting;
using UtilityBot.Controllers;

namespace UtilityBot
{   
    /// <summary>
    /// Основная логика бота, которая вызывает обработчики событий, которая постоянно активна
    /// </summary>
    internal class Bot : BackgroundService
    {
        private ITelegramBotClient _telegramBotClient;

        private InlineKeyboardController _inlineKeyboardController;
        private TextMessageController _textMessageController;
        private VoiceMessageController _voiceMessageController;
        private DefaultMessageController _defaultMessageController;

        public Bot(ITelegramBotClient telegramBotClient, 
                   InlineKeyboardController inlineKeyboardController, 
                   TextMessageController textMessageController, 
                   VoiceMessageController voiceMessageController, 
                   DefaultMessageController defaultMessageController)
        {
            _telegramBotClient = telegramBotClient;
            _inlineKeyboardController = inlineKeyboardController;
            _textMessageController = textMessageController;
            _voiceMessageController = voiceMessageController;
            _defaultMessageController = defaultMessageController;
        }

        protected override async Task ExecuteAsync(CancellationToken stopingToken)
        {
            _telegramBotClient.StartReceiving(
                HandleUpdateAsync,
                HandleErrorAsync,
                new ReceiverOptions() { AllowedUpdates = { } },
                cancellationToken: stopingToken);

            Console.WriteLine("Бот запущен.");
        }

        /// <summary>
        /// Метод обработки штатных событий
        /// </summary>
        /// <param name="botClient">Клиент, благодаря которому бот подключится к Telegram Bot API</param>
        /// <param name="update">Обновление любого типа</param>
        /// <param name="cancellationToken">Специальная структура для отмены зависшей задачи</param>
        async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            if (update.Type == UpdateType.CallbackQuery)
            {
                await _inlineKeyboardController.Handle(update.CallbackQuery, cancellationToken);
                return;
            }

            if (update.Type == UpdateType.Message)
            {
                switch (update.Message!.Type)
                {
                    case MessageType.Voice:
                        await _voiceMessageController.Handle(update.Message, cancellationToken);
                        return;

                    case MessageType.Text:
                        await _textMessageController.Handle(update.Message, cancellationToken);
                        return;

                    default:
                        await _defaultMessageController.Handle(update.Message, cancellationToken);
                        return;
                }
            }
        }

        /// <summary>
        /// Метод обработки проблемных событий
        /// </summary>
        /// <param name="exception">Любое исключение</param>
        Task HandleErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
        {   
            var errorMessage = exception switch
            {
                ApiRequestException apiRequestException
                    => $"Telegram API Error:\n[{apiRequestException.ErrorCode}]\n{apiRequestException.Message}",
                _ => exception.ToString()
            };

            Console.WriteLine(errorMessage);
            Console.WriteLine("Ожидаем 10 секунд перед повторным подключением");
            Thread.Sleep(10000);

            return Task.CompletedTask;
        }
    }
}
