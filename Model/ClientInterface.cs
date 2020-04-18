using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ex1.Model
{
    public interface ItelnetClient
    {

        Task<bool> connect(string ip, int port);
        void write(string command);
        string read(); //blocking call
        void disconnect();
    }
}