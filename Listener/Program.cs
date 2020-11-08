using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace Listener
{
    class Program
    {
        public static List<TcpClient> clients; 
        private static object localAddr;

        static void Main(string[] args)
        {
            clients = new List<TcpClient>();

            TcpListener server=null;
            try
            {
                // Set the TcpListener on port 13000.
                Int32 port = 13000;
                    localAddr = IPAddress.Parse("26.206.223.145");

                // TcpListener server = new TcpListener(port);
                server = new TcpListener((IPAddress)localAddr, port);

                // Start listening for client requests.
                server.Start();

                // Buffer for reading data
                Byte[] bytes = new Byte[256];
                String data = null;

                // Enter the listening loop.
                while(true)
                {
                    Console.Write("Waiting for a connection... ");
                    TcpClient client = server.AcceptTcpClient();
                    Console.WriteLine("Connection!");
                
                    Thread t = new Thread(new ParameterizedThreadStart(HandleConnection));  
                    t.Start(client);      
                }                         
            }

            catch(SocketException e)
            {
                Console.WriteLine("SocketException: {0}", e);
            }
            finally
            {
                // Stop listening for new clients.
                server.Stop();
            }

            Console.WriteLine("\nHit enter to continue...");
            Console.Read();
        }

        static void HandleConnection(Object c)
        {
                    Byte[] bytes = new Byte[256];
                    String data = null;
                    var client = c as TcpClient;

                    data = null;

                    // Get a stream object for reading and writing
                    NetworkStream stream = client.GetStream();

                    int i;

                    // Loop to receive all the data sent by the client.
                    while((i = stream.Read(bytes, 0, bytes.Length))!=0)
                    {
                        // Translate data bytes to a ASCII string.
                        data = System.Text.Encoding.ASCII.GetString(bytes, 0, i);
                        Console.WriteLine("Received: {0}", data);

                        // Process the data sent by the client.
                        //data = data.ToUpper();
                        switch (data)
                        {
                            case "A":
                                data = ":)";
                                //Console.Beep(500, 100);
                                break;
                            case "B":
                                data = ":(";
                                Console.Beep(100, 500);
                                break;
                            case "C":
                                data = "XD";
                                Console.Beep(1000, 200);
                                break;
                            case "D":
                                data = ":|";
                                Console.Beep(200, 1000);
                                break;
                            case "E":
                                data = ":))";
                                Console.Beep(300, 100);
                                break;
                        }

                        byte[] msg = System.Text.Encoding.ASCII.GetBytes(data);

                        // Send back a response.
                        stream.Write(msg, 0, msg.Length);
                        Console.WriteLine("Sent: {0}", data);
                    }

                // Shutdown and end connection
                client.Close();
        }
    }
}
