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
    public static class Operation
    {
        public static long LongSumFromString(string? intputString)
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

        public static int GetMessageLength(string message)
        {
            StringInfo stringInfo = new StringInfo(message);
            var messageLength = stringInfo.LengthInTextElements;
            return messageLength;
        }
    }
}
