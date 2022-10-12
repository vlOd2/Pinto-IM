using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PintoChatNS.Networking
{
    public abstract class NetworkingClient
    {
        public bool IsConnected { get; protected set; }
        protected Socket socket;
        protected Thread receiveThread;
        public event EventHandler Connected;
        public event EventHandler Disconnected;

        public bool Connect(string ipAddress, int port)
        {
            try
            {
                socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                receiveThread = new Thread(new ThreadStart(ReceiveThread_Func));

                IsConnected = true;
                socket.Connect(new IPEndPoint(IPAddress.Parse(ipAddress), port));

                if (Connected != null)
                    Connected.Invoke(this, EventArgs.Empty);

                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool StartReceiving() 
        {
            try
            {
                if (!IsConnected) return false;
                receiveThread.Start();
                return true;
            }
            catch 
            {
                return false;
            }
        }

        public void Disconnect()
        {
            if (!IsConnected) return;
            IsConnected = false;

            if (socket != null)
            {
                if (socket.Connected)
                    socket.Disconnect(false);
                socket.Close();
            }

            socket = null;
            receiveThread = null;
            if (Disconnected != null)
                Disconnected.Invoke(this, EventArgs.Empty);
        }

        public abstract byte[] ReadData(int amount);
        public abstract void SendData(byte[] data);
        protected abstract void ReceiveThread_Func();
    }
}
