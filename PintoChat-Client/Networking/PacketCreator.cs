using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PintoChatNS.Networking
{
    public static class PacketCreator
    {
        public static Packet ProtocolAgreement(int trid,
            int protocolVersion, 
            string protocolHandlerName, 
            int protocolHandlerVer)
        {
            Packet packet = new Packet();
            packet.ID = 0;
            packet.TrID = trid;
            packet.SetData("PROTOCOL_VERSION", protocolVersion.ToString());
            packet.SetData("PROTOCOL_HANDLER_NAME", protocolHandlerName);
            packet.SetData("PROTOCOL_HANDLER_VER", protocolHandlerVer.ToString());
            return packet;
        }

        public static Packet Login(int trid, 
            string username, 
            string passwordHash, 
            int hashAlgorithm) 
        {
            Packet packet = new Packet();
            packet.ID = 1;
            packet.TrID = trid;
            packet.SetData("USERNAME", username);
            packet.SetData("PASSWORD", passwordHash);
            packet.SetData("HASHALGORITHM", hashAlgorithm.ToString());
            return packet;
        }

        public static Packet LoginChallenge(int trid,
            string content)
        {
            Packet packet = new Packet();
            packet.ID = 2;
            packet.TrID = trid;
            packet.SetData("CONTENT", content);
            return packet;
        }

        public static Packet Logout()
        {
            Packet packet = new Packet();
            packet.ID = 3;
            packet.TrID = -1;
            return packet;
        }
    }
}
