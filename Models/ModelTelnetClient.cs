using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Sockets;
using System.Text;

namespace Ex1.Model
{
    class ModelTelnetClient : ItelnetClient
    {
        //Client based on built-in tcpClient 
        private volatile TcpClient tcpField;
        //NS - Stream based on network access
        NetworkStream stream;
        //Stream Reader to read data from the server
        private StreamReader reader;
        public ModelTelnetClient()
        {
            //Construction creates a new TCP Client
            tcpField = new TcpClient();

        }

        /// <summary>
        /// Connect method - connects to a server by port and IP and creates a new stream
        /// </summary>
        /// <param name="ip"></param>
        /// <param name="port"></param>
        public void connect(string ip, int port)
        {
            //Connnection may fail, we would like to catch that and not crush!
            try
            {
                //connecting through tcpclient
                tcpField.Connect(ip, port);
                //creating a Network Stream based on said connection
                stream = tcpField.GetStream();
                //DEBUG - DELETE ON SUBMISSION
                Console.WriteLine("We Are IN!");
            }

            catch (Exception e)
            {
                Console.WriteLine("Connection Error", e);
                Console.WriteLine(e.ToString());

            }


        }


        public void disconnect()
        {
            if (tcpField != null) { tcpField.Close(); }

        }

        /// <summary>
        /// Streams the data from server
        /// </summary>
        /// <returns>Either data or fail exception</returns>
        public string read()
        {
            try
            {

                this.reader = new StreamReader(stream);
                string send = reader.ReadLine();
                return send;


            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                return null;

            }
        }

        /// <summary>
        /// Writes get or set command to server using Byte
        /// </summary>
        /// <param name="command">Get or Set Command following a property path and \n </param>
        public void write(string command)
        {
            try
            {

                Byte[] message = System.Text.Encoding.ASCII.GetBytes(command);
                tcpField.GetStream().Write(message, 0, message.Length);

            }
            catch (Exception e)
            {
                Console.WriteLine("Write ERR : " + e.ToString());
            }

        }
    }
}
}
