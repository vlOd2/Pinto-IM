using System;

namespace PintoChatNS.PintoConsole
{
    public interface IConsoleVariable : IConsoleCallable
    {
        ConsoleVariableType GetValueType();
        object GetValue();
        void SetValue(object value);
    }
}
