using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace UtilityBot.Controllers
{   /// <summary>
    /// Контроллер для обработки голосовых сообщений
    /// </summary>
    internal class VoiceMessageController
    {
        private readonly ITelegramBotClient _telegramBotClient;

        public VoiceMessageController(ITelegramBotClient telegramBotClient)
        {
            _telegramBotClient = telegramBotClient;
        }   

        public async Task Handle(Message message, CancellationToken ct)
        {
            Console.WriteLine($"Контроллер {GetType().Name} получил сообщение");
            await _telegramBotClient.SendTextMessageAsync(message.Chat.Id, $"Не умею обрабатывать аудиосообщения",cancellationToken: ct);
        }
    }
}
