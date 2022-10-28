using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using UtilityBot.Extensions;

namespace UtilityBot.Utilities
{   
    /// <summary>
    /// Утилита с операциями над входящей строкой
    /// </summary>
    public static class Operation
    {
        /// <summary>
        /// Метод вычисления суммы чисел из строки
        /// </summary>
        /// <param name="numbersList">Список чисел long, изъятых из строки</param>
        /// <returns>Сумма чисел</returns>
        public static long GetLongSumFromString(string? intputString)
        {
            var numbersList = StringExtension.GetLongArrayFromString(intputString);
            long sum = 0;

            if(numbersList.Count == 0)
                return sum;

            foreach (var number in numbersList)
            {
                sum += number;
            }

            return sum;
        }

        /// <summary>
        /// Метод подсчета количества символов в строке
        /// </summary>
        /// <param name="stringInfo">Объект класса StringInfo позволяет решить проблему суррогатной пары в случае символов Юникода</param>
        /// <returns>Количество символов в строке</returns>
        public static int GetCharsAmount(string? inputString)
        {
            StringInfo stringInfo = new StringInfo(inputString);
            var charsAmount = stringInfo.LengthInTextElements;
            return charsAmount;
        }
    }
}
