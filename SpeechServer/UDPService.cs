using System;
using System.Text;
using System.Net;
using System.Net.Sockets;

namespace SpeechServer
{
    class UDPService
    {
        
        private const string LOCALHOST_ADDRESS = "127.0.0.1";
        private const int PORT = 11001;

        Socket sendingSocket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
        IPAddress listenerAddress;
        IPEndPoint sendingEndpoint;

        public void Send(string text)
        {
            SetupUDP();
            // the socket object must have an array of bytes to send.
            // this loads the string entered by the user into an array of bytes.
            byte[] buffer = Encoding.ASCII.GetBytes(text);

            try
            {
                sendingSocket.SendTo(buffer, sendingEndpoint);
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e);
            }

        }   // End of Send()

        private void SetupUDP()
        {
            listenerAddress = IPAddress.Parse(LOCALHOST_ADDRESS);
            sendingEndpoint = new IPEndPoint(listenerAddress, PORT);
        }

        private void SetupUDP(string ipAddress)
        {
            listenerAddress = IPAddress.Parse(ipAddress);
            sendingEndpoint = new IPEndPoint(listenerAddress, PORT);
        }

        private void SetupUDP(int port)
        {
            listenerAddress = IPAddress.Parse(LOCALHOST_ADDRESS);
            sendingEndpoint = new IPEndPoint(listenerAddress, port);
        }

        private void SetupUDP(string ipAddress, int port)
        {
            listenerAddress = IPAddress.Parse(ipAddress);
            sendingEndpoint = new IPEndPoint(listenerAddress, port);
        }
    }
}
