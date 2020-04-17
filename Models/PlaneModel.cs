using System;
using System.ComponentModel;
using System.Threading;
using System.Threading.Tasks;

namespace Ex1.Model
{

    /// <summary>
    /// Defines the <see cref="PlaneModel" />.
    /// </summary>
    class PlaneModel : IPlaneModel
    {

        
           
     
        //Dashboard values  
        public string headingDegree = "ERR";
        public string verticalSpeed = "ERR";
        public string groundSpeed = "ERR";
        public string gpsAltFt = "ERR";
        public string rollDegree = "ERR";
        public string pitchDegree = "ERR";
        public string AltitudeIndicator = "ERR";
        public string airSpeed = "ERR"  ;

        public string latitude = "32.002644";
        public string longitude = "34.888781";
        public string location = "32.002644, 34.888781";


        public string error;
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
        



        public string ErrorList { get { return this.error; }
            set { error = value;
                this.NotifyPropertyChanged("ErrorList");
            } }

        /// <summary>
        /// Gets or sets the AirSpeed.
        /// </summary>
        public string AirSpeed { get
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
        public string Altitude
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
        public string AltMeter
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
        public string GroundSpeed {
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
        public string Heading
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
        public string Pitch
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
        public string Latitude
        {
            get { return this.latitude; }
            set
            {
                this.latitude = value;
                this.NotifyPropertyChanged("Latitude");
            }
        }

        /// <summary>
        /// Gets or sets the PlaneVert.
        /// </summary>
        public string Longitude
        {
            get { return this.longitude; }
            set
            {
                this.longitude = value;
                this.NotifyPropertyChanged("Longtitude");
            }
        }

  

        /// <summary>
        /// Gets or sets the Roll.
        /// </summary>
        public string Roll
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
        public string VerticalSpeed
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
        public async Task<bool> connect(string ip, int port)
        {
            await client.connect(ip, port);
            stop = false;
            return client.checkConncetion();
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




            string checkStr = "";
            new Thread(delegate ()
            {
              
                try
                {
                    while (!stop)
                    {
                        mute.WaitOne();

                        client.write("get /instrumentation/heading-indicator/indicated-heading-deg\n");
                        checkStr = client.read();
                        if (checkStr == "ERR") { getError("Heading"); }
                        this.headingDegree = checkStr;

                        client.write("get /instrumentation/gps/indicated-vertical-speed\n");
                        checkStr = client.read();
                        if (checkStr == "ERR") { getError("Vertical Speed"); }
                        this.verticalSpeed = checkStr;

                            
                        client.write("get /instrumentation/gps/indicated-ground-speed-kt\n");
                        checkStr = client.read();
                        if (checkStr == "ERR") { getError("Ground Speed"); }
                        this.groundSpeed = checkStr;

                        client.write("get /instrumentation/airspeed-indicator/indicated-speed-kt\n");
                        checkStr = client.read();
                        if (checkStr == "ERR") { getError("Air Speed"); }
                        this.airSpeed= checkStr;

                        client.write("get /instrumentation/attitude-indicator/internal-roll-deg\n");
                        checkStr = client.read();
                        if (checkStr == "ERR") { getError("Roll Degree"); }
                        this.rollDegree= checkStr;

                        client.write("get /instrumentation/gps/indicated-altitude-ft\n");
                        checkStr = client.read();
                        if (checkStr == "ERR") { getError("GPS altitude"); }
                        this.gpsAltFt= checkStr;

                        client.write("get /instrumentation/attitude-indicator/internal-pitch-deg\n");
                        checkStr = client.read();
                        if (checkStr == "ERR") { getError("Pitch Degree"); }
                        this.pitchDegree= checkStr;

                        client.write("get /instrumentation/altimeter/indicated-altitude-ft\n");
                        checkStr = client.read();
                        if (checkStr == "ERR") { getError("Altitude"); }
                        this.AltitudeIndicator= checkStr;

                        client.write("get /position/latitude-deg\n");
                        checkStr = client.read();
                        if (checkStr == "ERR") { getError("Latitude"); }
                        this.latitude = checkStr;

                        client.write("get /position/longitude-deg\n");
                        checkStr = client.read();
                        if (checkStr == "ERR") { getError("Longtitude"); }
                        this.longitude = checkStr;

                        Location = latitude + ", " + longitude;

                        mute.ReleaseMutex();
                        Thread.Sleep(250);

                    }

                }

                catch(ArgumentNullException)
                {
                    ErrorList += "Client connection problem, please reconnect\n";
                  
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
       
        public void getError(string propName)
        {
            error += "an error has occured while fetching data on " + propName + " property \n";
        }

    }
}
