using PintoChatNS.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace PintoChatNS.Networking
{
    public class PintoNetworkingClient : NetworkingClient
    {
        public event Action<int, int, byte[]> ReceivedPacket;

        public override byte[] ReadData(int amount)
        {
            return null;
        }

        public override void SendData(byte[] data)
        {

        }

        protected override void ReceiveThread_Func()
        {
            while (IsConnected) 
            {
                if (socket.Poll(1, SelectMode.SelectRead) && socket.Available == 0)
                {
                    Disconnect();
                    break;
                }
                
                if (socket.Available == 0) continue;

                byte[] finalData = new byte[0];
                while (socket.Available > 0)
                {
                    byte[] buffer = new byte[1024];
                    socket.Receive(buffer, 0, buffer.Length, SocketFlags.None);
                    finalData = finalData.Concat(buffer).ToArray();
                }

                if (Encoding.UTF8.GetString(finalData.Take(4).ToArray()).Equals("PMSG"))
                {
                    byte[] packetSizeRaw = finalData.Skip(4).Take(2).ToArray();
                    short packetSize = IPAddress.NetworkToHostOrder(BitConverter.ToInt16(packetSizeRaw, 0));

                    byte[] packetIDRaw = finalData.Skip(6).Take(4).ToArray();
                    int packetID = IPAddress.NetworkToHostOrder(BitConverter.ToInt32(packetIDRaw, 0));

                    byte[] packetTrIDRaw = finalData.Skip(10).Take(4).ToArray();
                    int packetTrID = IPAddress.NetworkToHostOrder(BitConverter.ToInt32(packetTrIDRaw, 0));

                    ReceivedPacket.Invoke(packetID, packetTrID, finalData.Skip(14).Take(packetSize).ToArray());
                }
                else
                {
                    Logger.LogWarn("Got invalid packet!");
                }
            }
        }
    }
}
