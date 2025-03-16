using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Server {
    class Program {
        static void Main(string[] args) {
            startServer();
        }

        public static void startServer() {
            IPHostEntry iphost = Dns.GetHostEntry(Dns.GetHostName());
            IPAddress ipaddr = iphost.AddressList[0];
            IPEndPoint localendpoint = new IPEndPoint(ipaddr, 11111);

            Socket listener = new Socket(ipaddr.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
            try {
                listener.Bind(localendpoint);
                listener.Listen(10);
                while(true) {
                    Console.WriteLine("Connecting");
                    Socket clientsocket = listener.Accept();
                    byte[] bytes = new byte[1024];
                    string data = null;

                    while(true) {
                        int numbyte = clientsocket.Receive(bytes);
                        data += Encoding.ASCII.GetString(bytes, 0, numbyte);

                        if (data.IndexOf("<EOF>") > -1) {
                            break;
                        }
                        

                    }

                    Console.WriteLine("Text: {0}", data);

                    byte[] message = Encoding.ASCII.GetBytes("Test server");
                    clientsocket.Send(message);
                    clientsocket.Shutdown(SocketShutdown.Both);
                    clientsocket.Close(); 
                }
            } catch (Exception e) {
                Console.WriteLine(e.ToString());
            }

        }
    }
}