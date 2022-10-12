using PintoChatNS.Logging;
using PintoChatNS.Misc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace PintoChatNS.Networking
{
    public class ConnectionManager
    {
        public bool IsConnected { get; internal set; }
        public bool IsLoggedIn { get; internal set; }
        public int CurrentTrID { get; protected set; }
        private readonly PintoNetworkingClient netClient;
        public readonly Dictionary<int, int> PacketsMap = new Dictionary<int, int>();
        public event Action ConnectionStarted;
        public event Action<Packet> ReceivedPacket;
        public event Action ConnectionEnded;

        public ConnectionManager() 
        {
            netClient = new PintoNetworkingClient();
            netClient.Connected += NetClient_Connected;
            netClient.ReceivedPacket += NetClient_ReceivedPacket;
            netClient.Disconnected += NetClient_Disconnected;
        }

        public bool Connect(string ip, int port)
        {
            if (IsConnected)
                Disconnect();
            IsConnected = netClient.Connect(ip, port);
            return IsConnected;
        }

        public void Disconnect()
        {
            if (IsLoggedIn)
                netClient.SendData(PacketCreator.Logout().ToData());
            netClient.Disconnect();
            IsConnected = false;
            IsLoggedIn = false;
        }

        private void NetClient_Connected(object sender, EventArgs e)
        {
            netClient.StartReceiving();
            ConnectionStarted.Invoke();
        }

        private void NetClient_ReceivedPacket(int packetID, int packetTrID, byte[] packetData)
        {
            Packet packet = new Packet();
            packet.ID = packetID;
            packet.TrID = packetTrID;
            packet.Init(Encoding.UTF8.GetString(packetData));
            ReceivedPacket.Invoke(packet);
        }

        private void NetClient_Disconnected(object sender, EventArgs e)
        {
            Disconnect();
            ConnectionEnded.Invoke();
        }
    }
}
