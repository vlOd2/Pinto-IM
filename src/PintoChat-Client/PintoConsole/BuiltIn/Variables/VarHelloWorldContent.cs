using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PintoChatNS.PintoConsole.BuiltIn.Variables
{
    public class VarHelloWorldContent : IConsoleVariable
    {
        private string helloWorldContent = "Hello, world!";

        public string GetName()
        {
            return "test_helloworldcontent";
        }

        public string GetDescription()
        {
            return "A variable that sets the value displayed when calling the test_helloworld command";
        }

        public ConsoleVariableType GetValueType()
        {
            return ConsoleVariableType.STRING;
        }

        public object GetValue()
        {
            return helloWorldContent;
        }

        public void SetValue(object value)
        {
            helloWorldContent = (string) value;
        }
    }
}
