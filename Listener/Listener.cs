using System.Net;
using System.Net.Sockets;
namespace ListenerNamespace
{
    public class Listener
    {
        private TcpListener _server;
        public void Start()
        {
            int port = 13000;
            IPAddress localAddr = IPAddress.Parse("127.0.0.1");
            _server = new TcpListener(localAddr, port);
            _server.Start();
            StartWaitingForConnections();
        }
        private void StartWaitingForConnections()
        {
            while (true)
            {
                TcpClient client = _server.AcceptTcpClient();
            }
        }
    }
}