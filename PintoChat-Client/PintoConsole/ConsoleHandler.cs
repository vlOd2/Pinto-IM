using PintoChatNS.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace PintoChatNS.PintoConsole
{
    public class ConsoleHandler
    {
        public readonly List<IConsoleCommand> ConsoleCommands = new List<IConsoleCommand>();
        public readonly List<IConsoleVariable> ConsoleVariables = new List<IConsoleVariable>();

        public void HandleInput() 
        {
            Console.Write("> ");
            string input = Console.ReadLine();

            if (!string.IsNullOrWhiteSpace(input)) 
            {
                (string, string[]) inputParsed = ParseInput(input.Trim());
                ProcessCommand(inputParsed.Item1, inputParsed.Item2);
            }
        }

        private (string, string[]) ParseInput(string input) 
        {
            string cmd = null;
            string[] cmdArgs = new string[0];

            if (input.Contains(((char)0x20).ToString()))
            {
                string[] splittedInput = Regex.Split(input, @"[ ](?=(?:[^""]*""[^""]*"")*[^""]*$)");
                cmd = splittedInput[0];
                cmdArgs = splittedInput.Skip(1).ToArray();

                for (int cmdArgIndex = 0; cmdArgIndex < cmdArgs.Length; cmdArgIndex++) 
                {
                    string cmdArg = cmdArgs[cmdArgIndex];

                    if (cmdArg.StartsWith("\""))
                        cmdArg = cmdArg.Substring(1, cmdArg.Length - 1);
                    if (cmdArg.EndsWith("\""))
                        cmdArg = cmdArg.Substring(0, cmdArg.Length - 1);

                    cmdArgs[cmdArgIndex] = cmdArg;
                }
            }
            else
                cmd = input;

            return (cmd, cmdArgs);
        }

        public void ProcessCommand(string cmd, string[] cmdArgs) 
        {
            IConsoleCommand conCmd = GetCommandByName(cmd);
            IConsoleVariable conVar = GetVariableByName(cmd);

            if (conCmd != null)
            {
                if (!(cmdArgs.Length < conCmd.GetMinArgsCount() || cmdArgs.Length > conCmd.GetMaxArgsCount()))
                    conCmd.Execute(cmdArgs);
                else
                    Logger.LogInfo($"Usage: {conCmd.GetUsage()}");
            }
            else if (conVar != null)
            {
                if (cmdArgs.Length < 1)
                    Logger.LogInfo($"\"{conVar.GetName()}\" \"{conVar.GetValue()}\" ({conVar.GetValueType()}) - {conVar.GetDescription()}");
                else 
                {
                    object argsValue = ConsoleVariableTypeTools.GetStringAsVarType(cmdArgs[0], conVar.GetValueType());

                    if (argsValue != null) 
                        conVar.SetValue(argsValue);
                    else
                        Logger.LogWarn("Invalid value provided for this convar!");
                }
            }
            else
                Logger.LogWarn($"Unrecognized command \"{cmd}\"!");
        }

        public IConsoleCommand GetCommandByName(string name) 
        {
            foreach (IConsoleCommand cmd in ConsoleCommands) 
            {
                if (cmd.GetName().Equals(name, StringComparison.InvariantCultureIgnoreCase))
                    return cmd;
            }

            return null;
        }

        public IConsoleVariable GetVariableByName(string name)
        {
            foreach (IConsoleVariable var in ConsoleVariables)
            {
                if (var.GetName().Equals(name, StringComparison.InvariantCultureIgnoreCase))
                    return var;
            }

            return null;
        }
    }
}
