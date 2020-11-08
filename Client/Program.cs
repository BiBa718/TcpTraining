using System;
using System.Text;
using System.Net;
using System.Net.Sockets;

namespace Client
{
    class Program
    {
        public List<TcpClient> clients = new List<TcpClient>(); 
        static void Main(string[] args)
        {
            try
            {
                Int32 port = 13000;
                IPAddress addres = IPAddress.Parse("26.129.184.182");
                TcpClient client = new TcpClient();
                client.Connect(addres.ToString(), port);
                NetworkStream stream = client.GetStream();

                Byte[] bytes = new Byte[256];
                String data = "A";
                byte[] msg = System.Text.Encoding.ASCII.GetBytes(data);
                stream.Write(msg, 0, msg.Length);

                int i;
                while((i = stream.Read(bytes, 0, bytes.Length))!=0)
                {
                    data = System.Text.Encoding.ASCII.GetString(bytes, 0, i);
                    Console.WriteLine("Received: {0}", data);

                    if (data == ":)")
                        data = "A";

                    msg = System.Text.Encoding.ASCII.GetBytes(data);
                    stream.Write(msg, 0, msg.Length);
                    Console.WriteLine("Sent: {0}", data);
                }
            }
            catch (SocketException e)
            {
                Console.WriteLine("SocketException: {0}", e);
            }
            
        }
    }
}
