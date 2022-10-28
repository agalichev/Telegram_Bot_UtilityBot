using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace UtilityBot.Extensions
{   /// <summary>
    /// Данный класс содержит методы расширения string
    /// </summary>
    public static class StringExtension
    {   
        /// <summary>
        /// Данный метод проверяет строку на числа, 
        /// при обнаружении разбивает ее на строки содержащие числа, 
        /// которые вносятся далее в список
        /// </summary>
        /// <param name="inputString">Проверяема строка (текст)</param>
        /// <param name="pattern">Шаблон для проверки строки</param>
        /// <param name="substrings">Массив строк с числами</param>
        /// <returns>Список long чисел</returns>
        public static List<long> GetLongArrayFromString(string? inputString)
        {
            List<long> numbers = new List<long> ();
            string pattern = @"\D+";

            if (inputString != null)
            {
                string[] substrings = Regex.Split(inputString, pattern);

                foreach (string substring in substrings)
                {
                    if (long.TryParse(substring, out long temp))
                        numbers.Add(temp);
                }
            }
            return numbers;
        }
    }
}
