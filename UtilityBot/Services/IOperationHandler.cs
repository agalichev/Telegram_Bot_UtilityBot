using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UtilityBot.Services
{
    public interface IOperationHandler
    {
        string Operate(string? messageText, string operationCode);
    }
}
