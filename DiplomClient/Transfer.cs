using System;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace DiplomClient
{
    class Transfer
    {
        const string ip = "127.0.0.1";
        const int port = 8080;
        private IPEndPoint tcpEndPoint;
        private Socket tcpSocket;

        public string TransferFunc(byte[] data)
        {
            tcpEndPoint = new IPEndPoint(IPAddress.Parse(ip), port);
            tcpSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            try
            {
                tcpSocket.Connect(tcpEndPoint);
                tcpSocket.Send(data);
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR: Exception in send data: " + ex);
            }

            byte[] buffer = new byte[256];
            int size = 0;
            StringBuilder answer = new StringBuilder();

            do
            {
                size = tcpSocket.Receive(buffer);
                answer.Append(Encoding.Unicode.GetString(buffer, 0, size));
            }
            while (tcpSocket.Available > 0);

            tcpSocket.Shutdown(SocketShutdown.Both);
            tcpSocket.Close();

            return answer.ToString();
        }

        public byte[] TransferFuncByte(byte[] data)
        {
            tcpEndPoint = new IPEndPoint(IPAddress.Parse(ip), port);
            tcpSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            try
            {
                tcpSocket.Connect(tcpEndPoint);
                tcpSocket.Send(data);
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR: Exception in send data: " + ex);
            }

            byte[] buffer = new byte[1000000];
            int size = 0;

            do
            {
                size = tcpSocket.Receive(buffer);
            }
            while (tcpSocket.Available > 0);

            tcpSocket.Shutdown(SocketShutdown.Both);
            tcpSocket.Close();

            return buffer.Take(size).ToArray();
        }
    }
}
