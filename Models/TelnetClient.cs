using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Ex1.Models
{
    class TelnetClient
    {
        private Socket socket;
        private NetworkStream networkStream;
        private TcpClient client;

        private void Connect(String ip, String port)
        {
            socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            try
            {
                socket.Connect(ip, Int32.Parse(port));
                Console.WriteLine("Connected"); //Test purpose only
            }
            catch
            {
                Console.WriteLine("ERROR"); //Test purpose only
            }
        }

        private void readInfo()
        {
            string command, recieved;

            //Read first thing givent o us
            recieved = read();
            Console.WriteLine(recieved);

            //Set up connection loop
            while (true)
            {
                Console.Write("Response: ");
                command = Console.ReadLine();

                if (command == "exit")
                    break;

                //write(command);
                recieved = read();

                Console.WriteLine(recieved);
            }

            Console.WriteLine("Disconnected from server");
            networkStream.Close();
            client.Close();
        }

        public string read()
        {
            byte[] data = new byte[1024];
            string recieved = "";

            int size = networkStream.Read(data, 0, data.Length);
            recieved = Encoding.ASCII.GetString(data, 0, size);

            return recieved;
        }
    }
}
