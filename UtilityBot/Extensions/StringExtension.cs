using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace UtilityBot.Extensions
{
    public static class StringExtension
    {
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
