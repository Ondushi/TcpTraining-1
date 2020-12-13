using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace ListenerNamespace
{
    class Program
    {
        // Functions:
        // Start()
        // StartWaitingForConnections()
        // (priv) AddNewConnection()
        // (priv) StopWaitingForConnections()
        // Stop()
        static void Main()
        {
            Listener listener = new Listener();
            listener.Start();
        }

        static void LastMain(string[] args)
        {
            #region class Listener part 1
            #region class Listener Start();
            TcpListener server = null;
            try
            {
                // Set the TcpListener on port 13000.
                int port = 13000;
                IPAddress localAddr = IPAddress.Parse("127.0.0.1");

                // TcpListener server = new TcpListener(port);
                server = new TcpListener(localAddr, port);

                // Start listening for client requests.
                server.Start();
                #endregion class Listener Start()                

                // Buffer for reading data
                Byte[] bytes = new Byte[256];
                String data = null;
                #region class Listener StartWaitingForConnections()
                // Enter the listening loop.
                while (true)
                {
                    Console.Write("Waiting for a connection... ");

                    // Perform a blocking call to accept requests.
                    // You could also use server.AcceptSocket() here.
                    TcpClient client = server.AcceptTcpClient();
                    Console.WriteLine("Connected!");
                    #endregion class Listener StartWaitingForConnections()
                    #endregion class Listener part1

                    #region class User
                    Thread clientThread = new Thread(() => {
                        data = null;

                        // Get a stream object for reading and writing
                        NetworkStream stream = client.GetStream();
                        int i;

                        // Loop to receive all the data sent by the client.
                        while ((i = stream.Read(bytes, 0, bytes.Length)) != 0)
                        {
                            // Translate data bytes to a ASCII string.
                            data = System.Text.Encoding.ASCII.GetString(bytes, 0, i);
                            Console.WriteLine("Received: {0}", data);

                            // Process the data sent by the client.
                            switch (data)
                            {
                                case "a":
                                    Console.WriteLine("Cursor Move Upper Left");
                                    Console.Beep(4000, 300);
                                    break;
                                case "b":
                                    Console.WriteLine("Cursor Move Upper Right");
                                    Console.Beep(2000, 300);
                                    break;
                                case "c":
                                    Console.WriteLine("Cursor Move Lower Right");
                                    Console.Beep(5000, 300);
                                    break;
                                case "d":
                                    Console.WriteLine("Cursor Move Lower Left");
                                    Console.Beep(6000, 300);
                                    break;
                                case "e":
                                    Console.WriteLine("Cursor Move Center");
                                    Console.Beep(8000, 300);
                                    break;
                                default:
                                    Console.WriteLine($"Symbol: {data} received but no reaction found");
                                    break;
                            }

                            byte[] msg = System.Text.Encoding.ASCII.GetBytes(data);

                            // Send back a response.
                            stream.Write(msg, 0, msg.Length);
                            Console.WriteLine("Sent: {0}", data);
                        }

                        // Shutdown and end connection
                        client.Close(); 
                    });

                    clientThread.Start();
                    #endregion class User
                    #region class Listener part 2
                }
            }
            catch (SocketException e)
            {
                Console.WriteLine("SocketException: {0}", e);
            }
            finally
            {
                // Stop listening for new clients.
                server.Stop();
            }
            #endregion class Listener part 2
            Console.WriteLine("\nHit enter to continue...");
            Console.Read();
        }
    }
}

