using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using UtilityBot.Configuration;
using UtilityBot.Utilities;

namespace UtilityBot.Services
{
    public class BotOperationHandler : IOperationHandler
    {
        public string Operate(string? text, string operationCode)
        {
            string result = String.Empty;

            switch (operationCode)
            {
                case "Sum":
                    Console.WriteLine("Вычисляется сумма чисел...");
                    result = $"Сумма чисел: {Operation.GetLongSumFromString(text)}";
                    Console.WriteLine($"Операция выполнена.");
                    break;

                case "Length":
                    Console.WriteLine("Ведется подсчёт количества символов в сообщении...");
                    result = $"Количество символов в вашем сообщении: {Operation.GetCharsAmount(text)}";
                    Console.WriteLine($"Операция выполнена.");
                    break;
            }

            return result;
        }
    }
}
