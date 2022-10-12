using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PintoChatNS.PintoConsole
{
    public interface IConsoleCallable
    {
        string GetName();
        string GetDescription();
        void SetConsoleHandler(ConsoleHandler handler);
    }
}
