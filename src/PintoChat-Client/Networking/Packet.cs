using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

namespace PintoChatNS.Networking
{
    public class Packet
    {
        /// <summary>
        /// The ID of this packet
        /// </summary>
        public int ID;
        public int TrID;
        protected bool genericDataIOEnabled = true;
        protected readonly Dictionary<string, string> data = new Dictionary<string, string>();

        /// <summary>
        /// Initializes this packet with the specified raw data
        /// </summary>
        /// <param name="rawData">The raw data</param>
        public virtual void Init(string rawData) 
        {
            foreach (string rawKeyValuePair in rawData.Split(';')) 
            {
                string[] splittedRawKeyValuePair = rawKeyValuePair.Split('=');
                string key = splittedRawKeyValuePair[0];
                string value = splittedRawKeyValuePair[1];

                if (!data.ContainsKey(key))
                    data.Add(key, value);
            }
        }

        /// <summary>
        /// Gets a data entry using the specified key
        /// </summary>
        /// <param name="key">The key of the data entry</param>
        /// <exception cref="InvalidOperationException">If generic IO is disabled or the specified data entry cannot be found</exception>
        /// <returns>The data entry</returns>
        public virtual string GetData(string key) 
        {
            if (!genericDataIOEnabled || 
                !data.ContainsKey(key)) throw new InvalidOperationException();
            return data[key];
        }

        /// <summary>
        /// Sets the value of a data entry found with the specified key
        /// </summary>
        /// <param name="key">The key of the data entry</param>
        /// <param name="value">The new value of the data entry</param>
        /// <exception cref="InvalidOperationException">If generic IO is disabled</exception>
        public virtual void SetData(string key, string value)
        {
            if (!genericDataIOEnabled) throw new InvalidOperationException();
            data[key] = value;
        }

        public override string ToString()
        {
            string finalStr = "";

            bool firstEntry = true;
            foreach (KeyValuePair<string, string> dataEntry in data) 
            {
                if (firstEntry)
                {
                    finalStr += $"{dataEntry.Key}={dataEntry.Value}";
                    firstEntry = false;
                }
                else 
                {
                    finalStr += $";{dataEntry.Key}={dataEntry.Value}";
                }
            }

            return finalStr;
        }

        /// <summary>
        /// Converts this packet to a byte array that can be sent
        /// </summary>
        /// <returns></returns>
        public byte[] ToData() 
        {
            byte[] packetDataEncoded = Encoding.UTF8.GetBytes(ToString());
            return Encoding.UTF8.GetBytes("PMSG")
                .Concat(
                    BitConverter.GetBytes(
                        IPAddress.HostToNetworkOrder((short)packetDataEncoded.Length)
                    ))
                .Concat(
                    BitConverter.GetBytes(
                        IPAddress.HostToNetworkOrder(ID)
                    ))
                .Concat(
                    BitConverter.GetBytes(
                        IPAddress.HostToNetworkOrder(TrID)
                    ))
                .Concat(packetDataEncoded)
                .ToArray();
        }
    }
}
