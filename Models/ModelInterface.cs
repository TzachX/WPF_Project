using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Text;

namespace Ex1.Model
{
    interface IPlaneModel : INotifyPropertyChanged
    {
        //connection
        void connect(string ip, int port);
        void disconnect();
        void start();

        //Plane Information 
        double AirSpeed { get; set; }
        double Altitude { get; set; }
        double Roll { get; set; }
        double Pitch { get; set; }
        double AltMeter { get; set; }
        double Heading { get; set; }
        double GroundSpeed { get; set; }
        double VerticalSpeed { get; set; }


        double PlaneHoriz { get; set; }
        double PlaneVert { get; set; }


        void setAileron(double value);
        void setElevator(double value);
        void setRudder(double value);
        void setThrottle(double value);



    }
}
