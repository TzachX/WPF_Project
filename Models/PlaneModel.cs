using System;
using System.ComponentModel;
using System.Threading;

namespace Ex1.Model
{

    /// <summary>
    /// Defines the <see cref="PlaneModel" />.
    /// </summary>
    class PlaneModel : IPlaneModel
    {

        


        //Dashboard values  
        public double headingDegree = 0.0;
        public double verticalSpeed = 0.0;
        public double groundSpeed = 0.0;
        public double gpsAltFt = 0.0;
        public double rollDegree = 0.0;
        public double pitchDegree = 0.0;
        public double AltitudeIndicator = 0.0;
        public double airSpeed = 0.0;

        public double lan;
        public double lon;
        /// <summary>
        /// Defines the PropertyChanged.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        ModelTelnetClient client;
        Mutex mute = new Mutex();
        volatile Boolean stop;
        bool isConnected;


        public PlaneModel(ModelTelnetClient givenClient)
        {
            this.client = givenClient;
            isConnected = false;
            stop = false;
        }
        /// <summary>
        /// Sets the Aileron.
        /// </summary>
   

        /// <summary>
        /// Gets or sets the AirSpeed.
        /// </summary>
        public double AirSpeed { get
            {
                return this.airSpeed;
            }
            set {
                this.airSpeed = value;
                this.NotifyPropertyChanged("AirSpeed");
            } }

        /// <summary>
        /// Gets or sets the Altitude - gps!
        /// </summary>
        public double Altitude
        {
            get { return this.gpsAltFt; }
            set
            {
                this.gpsAltFt = value;
                this.NotifyPropertyChanged("gpsAltFt");
            }
        }

        /// <summary>
        /// Gets or sets the AltMeter.
        /// </summary>
        public double AltMeter
        {
            get { return this.AltitudeIndicator; }
            set
            {
                this.AltitudeIndicator = value;
                this.NotifyPropertyChanged("AltitudeIndicator");
            }
        }

    
        /// <summary>
        /// Gets or sets the GroundSpeed.
        /// </summary>
        public double GroundSpeed {
            get { return this.groundSpeed; }
            set
            {
                this.groundSpeed = value;
                this.NotifyPropertyChanged("GroundSpeed");
            }
        }

        /// <summary>
        /// Gets or sets the Heading.
        /// </summary>
        public double Heading
        {
            get { return this.headingDegree; }
            set
            {
                this.headingDegree = value;
                this.NotifyPropertyChanged("HeadingDegree");
            }
        }

        /// <summary>
        /// Gets or sets the Pitch.
        /// </summary>
        public double Pitch
        {
            get { return this.pitchDegree; }
            set
            {
                this.pitchDegree = value;
                this.NotifyPropertyChanged("PitchDegree");
            }
        }

        /// <summary>
        /// Gets or sets the PlaneHoriz.
        /// </summary>
        public double PlaneHoriz
        {
            get { return this.lan; }
            set
            {
                this.lan = value;
                this.NotifyPropertyChanged("Latitude");
            }
        }

        /// <summary>
        /// Gets or sets the PlaneVert.
        /// </summary>
        public double PlaneVert
        {
            get { return this.lon; }
            set
            {
                this.lon = value;
                this.NotifyPropertyChanged("Longtitude");
            }
        }

        /// <summary>
        /// Gets or sets the Roll.
        /// </summary>
        public double Roll
        {
            get { return this.rollDegree; }
            set
            {
                this.rollDegree = value;
                this.NotifyPropertyChanged("RollDegree");
            }
        }

   
        /// <summary>
        /// Gets or sets the VerticalSpeed.
        /// </summary>
        public double VerticalSpeed
        {
            get { return this.verticalSpeed; }
            set
            {
                this.verticalSpeed = value;
                this.NotifyPropertyChanged("VerticalSpeed");
            }
        }   

        /// <summary>
        /// The connect.
        /// </summary>
        /// <param name="ip">The ip<see cref="string"/>.</param>
        /// <param name="port">The port<see cref="int"/>.</param>
        public void connect(string ip, int port)
        {
            if (isConnected == false) { this.client.connect(ip, port); }
           
        }

        /// <summary>
        /// The disconnect.
        /// </summary>
        public void disconnect()
        {
            if (isConnected)
            {
                client.disconnect();
                stop = true;
            }
        }

        /// <summary>
        /// The start.
        /// </summary>
        public void start()
        {
           



            new Thread(delegate ()
            {
              
                try
                {
                    while (!stop)
                    {
                        mute.WaitOne();
                        client.write("get /instrumentation/heading-indicator/indicated-heading-deg\n");
                        this.headingDegree = Convert.ToDouble(client.read());
                        client.write("get /instrumentation/gps/indicated-vertical-speed\n");
                        this.verticalSpeed = Convert.ToDouble(client.read());
                        client.write("get /instrumentation/gps/indicated-ground-speed-kt\n");
                        this.groundSpeed = Convert.ToDouble(client.read());
                        client.write("get /instrumentation/airspeed-indicator/indicated-speed-kt\n");
                        this.airSpeed= Convert.ToDouble(client.read());
                        client.write("get /instrumentation/attitude-indicator/internal-roll-deg\n");
                        this.rollDegree= Convert.ToDouble(client.read());
                        client.write("get /instrumentation/gps/indicated-altitude-ft\n");
                        this.gpsAltFt= Convert.ToDouble(client.read());
                        client.write("get /instrumentation/attitude-indicator/internal-pitch-deg\n");
                        this.pitchDegree= Convert.ToDouble(client.read());
                        client.write("get /instrumentation/altimeter/indicated-altitude-ft\n");
                        this.AltitudeIndicator= Convert.ToDouble(client.read());
                        client.write("get /position/latitude-deg\n");
                        this.lan= Convert.ToDouble(client.read());
                        client.write("get /position/longitude-deg\n");
                        this.lon= Convert.ToDouble(client.read());
                        mute.ReleaseMutex();


                    }

                }

                catch
                {
                   
                }


            }).Start();
        }

        /// <summary>
        /// Sets the Rudder.
        /// </summary>
        public void setRudder(double value)
        {
            string command = "set /controls/flight/rudder " + value + "\n";
            this.DataSet(command);

        }

        /// <summary>
        /// Sets the Throttle.
        /// </summary>
        public void setThrottle(double value)
        {
            string command = "set /controls/flight/throttle " + value + "\n";
            this.DataSet(command);

        }
        /// <summary>
        /// Sets the Elevator.
        /// </summary>
        public void setElevator(double value)
        {
            string command = "set /controls/flight/elevator " + value + "\n";
            this.DataSet(command);



        }




        public void setAileron(double value)
        {

            string command = "set /controls/flight/throttle " + value + "\n";
            this.DataSet(command);


        }



        public void NotifyPropertyChanged(String propName)
        {

            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
            }
        }







        public void DataSet(string setCommand)
        {
            this.mute.WaitOne();
            this.client.write(setCommand);
            this.client.read();
            this.mute.ReleaseMutex();
        }
       

    }
}
