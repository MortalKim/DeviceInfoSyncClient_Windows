using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace DeviceInfoSyncClient.Helpers
{
    public class TransmissionHelpers
    {
        UdpClient udpSender;
        public TransmissionHelpers(string ip, int port)
        {
            udpSender = new UdpClient(0);
            udpSender.Connect(ip, port);
        }

        public void sendMsg(string msg)
        {
            byte[] sendBytes = Encoding.ASCII.GetBytes(msg);
            udpSender.Send(sendBytes, sendBytes.Length);
        }

        public void close()
        {
            udpSender.Close();
        }
    }
}
