using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Ex1.Model
{
    interface IPlaneModel : INotifyPropertyChanged
    {
        //connection
        Task<bool> connect(string ip, int port);
        void disconnect();
        void start();

        //Plane Information 
        string AirSpeed { get; set; }
        string Altitude { get; set; }
        string Roll { get; set; }
        string Pitch { get; set; }
        string AltMeter { get; set; }
        string Heading { get; set; }
        string GroundSpeed { get; set; }
        string VerticalSpeed { get; set; }


        string PlaneHoriz { get; set; }
        string PlaneVert { get; set; }


        void setAileron(double value);
        void setElevator(double value);
        void setRudder(double value);
        void setThrottle(double value);

        string ErrorList { get; set; }


    }
}
