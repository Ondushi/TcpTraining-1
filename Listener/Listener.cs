using System.Net;
using System.Net.Sockets;
namespace ListenerNamespace
{
    public class Listener
    {
        private TcpListener _server;
        public void Start()
        {
            _server = null;
            int port = 13000;
            IPAddress localAddr = IPAddress.Parse("127.0.0.1");
            _server = new TcpListener(localAddr, port);
            _server.Start();
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