using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace DeviceInfoSyncClient.Helpers
{
    public class TransmissionHelpers
    {
        UdpClient udpSender;

        public delegate void ReceiveResult(object data);

        public TransmissionHelpers(string ip, int port)
        {
            udpSender = new UdpClient(0);
            udpSender.Connect(ip, port);
        }

        public TransmissionHelpers(string ip, int port, int testca)
        {
            udpSender = new UdpClient(testca);
            udpSender.Connect(ip, port);
        }

        public void SendMsg(string msg)
        {
            byte[] sendBytes = Encoding.ASCII.GetBytes(msg);
            udpSender.Send(sendBytes, sendBytes.Length);
        }

        /// <summary>
        /// Recive once
        /// </summary>
        /// <param name="requestCallback">delegate callback</param>
        public void StartReceive(ReceiveResult receiveResult)
        {
            udpSender.BeginReceive((IAsyncResult ar) =>
            {
                try
                {
                    IPEndPoint? e = new IPEndPoint(IPAddress.Any, 0);
                    byte[] receiveBytes = udpSender.EndReceive(ar, ref e);
                    string receiveString = Encoding.ASCII.GetString(receiveBytes);
                    receiveResult.Invoke(receiveString);
                }
                catch(Exception e) { }
            }, null);
        }

        public void Close()
        {
            udpSender.Close();
        }

        public struct UdpState
        {
            public UdpClient u;
            public IPEndPoint e;
        }
    }
}
