using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Concurrent;
using UtilityBot.Models;

namespace UtilityBot.Services
{
    public class MemoryStorage : IStorage
    {
        /// <summary>
        /// Хранилище сессий
        /// </summary>
        /// <param name="_sessions">Данные сессии пользователя</param>
        private readonly ConcurrentDictionary<long, Session> _sessions;

        public MemoryStorage()
        {
            _sessions = new ConcurrentDictionary<long, Session>();
        }

        /// <summary>
        /// Данный метод возвращает сессию по ключу, если она существует, или создает и возвращает новую
        /// </summary>
        /// <param name="chatId">Уникальный идентификатор чата (ключ)</param>
        /// <returns>
        /// Существующая или новая сессия
        /// </returns>
        public Session GetSession(long chatId)
        {
            if (_sessions.ContainsKey(chatId))
                return _sessions[chatId];

            var newSession = new Session() { /*OperationCode = "Length"*/ };
            _sessions.TryAdd(chatId, newSession);
            return newSession;
        }
    }
}
