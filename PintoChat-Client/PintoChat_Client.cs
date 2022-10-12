using System;
using System.Collections.Generic;
using System.Threading;
using PintoChatNS.Logging;
using PintoChatNS.Networking;
using PintoChatNS.Misc;
using System.Text;
using PintoChatNS.PintoConsole;
using PintoChatNS.PintoConsole.BuiltIn.Commands;
using PintoChatNS.PintoConsole.BuiltIn.Variables;

namespace PintoChatNS
{
    public class PintoChat
    {
        public readonly static PintoChat Instance = new PintoChat();
        public bool IsRunning { get; internal set; }
        public ConnectionManager ConHandler;
        public readonly ConsoleHandler CslHandler = new ConsoleHandler();

        public void ClientMain()
        {
            IsRunning = true;

            ConHandler = new ConnectionManager();
            ConHandler.ConnectionStarted += HandleConnectionStarted;
            ConHandler.ReceivedPacket += HandleReceivedPacket;
            ConHandler.ConnectionStarted += HandleDisconnectionStarted;

            CslHandler.ConsoleCommands.Add(new CmdHelloWorld());
            CslHandler.ConsoleVariables.Add(new VarHelloWorldContent());

            while (IsRunning)
            {
                CslHandler.HandleInput();
            }
        }

        private void HandleConnectionStarted() 
        {
        
        }

        private void HandleReceivedPacket(Packet packet) 
        {
        
        }

        private void HandleDisconnectionStarted()
        {

        }
    }
}