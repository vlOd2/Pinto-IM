using PintoChatNS.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PintoChatNS.PintoConsole.BuiltIn.Commands
{
    public class CmdHelloWorld : IConsoleCommand
    {
        public string GetName()
        {
            return "test_helloworld";
        }

        public string GetUsage()
        {
            return "test_helloworld";
        }

        public string GetDescription()
        {
            return "A basic hello world command";
        }

        public void Execute(string[] args)
        {
            IConsoleVariable varHelloWorldContent = PintoChat.Instance.CslHandler.GetVariableByName("test_helloworldcontent");
            string str = "Hello, world!";

            if (!(varHelloWorldContent != null && 
                !string.IsNullOrWhiteSpace(str = (string) varHelloWorldContent.GetValue()))) 
            {
                str = "Hello, world!";
            }

            Logger.LogInfo(str);
        }

        public int GetMaxArgsCount()
        {
            return 0;
        }

        public int GetMinArgsCount()
        {
            return 0;
        }
    }
}
