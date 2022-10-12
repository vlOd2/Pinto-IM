using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PintoChatNS.PintoConsole
{
    public enum ConsoleVariableType
    {
        INT,
        BOOL,
        STRING
    }

    public static class ConsoleVariableTypeTools 
    {
        public static object GetStringAsVarType(string str, ConsoleVariableType type = ConsoleVariableType.STRING) 
        {
            try
            {
                if (type == ConsoleVariableType.INT)
                    return int.Parse(str);
                else if (type == ConsoleVariableType.BOOL)
                    return bool.Parse(str);
                else
                    return str;
            }
            catch 
            {
                return null;
            }
        }
    }
}
