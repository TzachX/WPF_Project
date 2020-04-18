using System;
using System.ComponentModel;
using System.Threading;
using System.Threading.Tasks;

namespace Ex1.Model
{

    /// <summary>
    /// Defines the <see cref="PlaneModel" />.
    /// </summary>
    public class PlaneModel : IPlaneModel
    {




        //Dashboard values  
        public string headingDegree = "ERR";
        public string verticalSpeed = "ERR";
        public string groundSpeed = "ERR";
        public string gpsAltFt = "ERR";
        public string rollDegree = "ERR";
        public string pitchDegree = "ERR";
        public string AltitudeIndicator = "ERR";
        public string airSpeed = "ERR";

        public string latitude = "10.002644";
        public string longitude = "10.888781";
        public string location = "10.002644,10.888781";


        public string error;
        /// <summary>
        /// Defines the PropertyChanged.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        ModelTelnetClient client;
        readonly Mutex mute = new Mutex();
        volatile Boolean stop;
        public bool isConnected;


        public PlaneModel(ModelTelnetClient givenClient)
        {
            this.client = givenClient;
            isConnected = false;
            stop = false;
        }
        /// <summary>
        /// Sets the Aileron.
        /// </summary>




        public string ErrorList
        {
            get { return this.error; }
            set
            {
                error = value;
                this.NotifyPropertyChanged("ErrorList");
            }
        }

        /// <summary>
        /// Gets or sets the AirSpeed.
        /// </summary>
        public string AirSpeed
        {
            get
            {
                return this.airSpeed;
            }
            set
            {
                this.airSpeed = value;
                this.NotifyPropertyChanged("AirSpeed");
            }
        }

        /// <summary>
        /// Gets or sets the Altitude - gps!
        /// </summary>
        public string Altitude
        {
            get { return this.gpsAltFt; }
            set
            {
                this.gpsAltFt = value;
                this.NotifyPropertyChanged("Altitude");
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
                this.NotifyPropertyChanged("AltMeter");
            }
        }


        /// <summary>
        /// Gets or sets the GroundSpeed.
        /// </summary>
        public string GroundSpeed
        {
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
                this.NotifyPropertyChanged("Heading");
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
                this.NotifyPropertyChanged("Pitch");
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
                this.NotifyPropertyChanged("Longitude");
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
                this.NotifyPropertyChanged("Roll");
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

        public bool checkConnection
        {
            get { return this.client.checkConncetion(); }
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
            isConnected = client.checkConncetion();
            Console.WriteLine(isConnected);
            return client.checkConncetion();
        }

        /// <summary>
        /// The disconnect.
        /// </summary>
        public void disconnect()
        {
            if (client.checkConncetion())
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
                        Heading = checkStr;

                        client.write("get /instrumentation/gps/indicated-vertical-speed\n");
                        checkStr = client.read();
                        if (checkStr == "ERR") { getError("Vertical Speed"); }
                        VerticalSpeed = checkStr;


                        client.write("get /instrumentation/gps/indicated-ground-speed-kt\n");
                        checkStr = client.read();
                        if (checkStr == "ERR") { getError("Ground Speed"); }
                        GroundSpeed = checkStr;

                        client.write("get /instrumentation/airspeed-indicator/indicated-speed-kt\n");
                        checkStr = client.read();
                        if (checkStr == "ERR") { getError("Air Speed"); }
                        AirSpeed = checkStr;

                        client.write("get /instrumentation/attitude-indicator/internal-roll-deg\n");
                        checkStr = client.read();
                        if (checkStr == "ERR") { getError("Roll Degree"); }
                        Roll = checkStr;

                        client.write("get /instrumentation/gps/indicated-altitude-ft\n");
                        checkStr = client.read();
                        if (checkStr == "ERR") { getError("GPS altitude"); }
                        Altitude = checkStr;
                        if (Convert.ToDouble(this.gpsAltFt) < 0) { getError("GPS altitude"); }

                        client.write("get /instrumentation/attitude-indicator/internal-pitch-deg\n");
                        checkStr = client.read();
                        if (checkStr == "ERR") { getError("Pitch Degree"); }
                        Pitch = checkStr;

                        client.write("get /instrumentation/altimeter/indicated-altitude-ft\n");
                        checkStr = client.read();
                        if (checkStr == "ERR") { getError("Altitude"); }
                        AltMeter = checkStr;
                        if(Convert.ToDouble(this.gpsAltFt)<0) { getError("Altitude"); }

                        client.write("get /position/latitude-deg\n");
                        checkStr = client.read();
                        if (checkStr == "ERR") { getError("Latitude"); }
                        Latitude = checkStr;
                        if (Convert.ToDouble(this.latitude) < -90 || Convert.ToDouble(this.latitude) > 90) { getError("Latitude"); }




                        client.write("get /position/longitude-deg\n");
                        checkStr = client.read();
                        if (checkStr == "ERR") { getError("Longtitude"); }
                        Longitude = checkStr;
                        if (Convert.ToDouble(this.longitude) < -180 || Convert.ToDouble(this.latitude) > 180) { getError("Longtitude"); }

                        Location = latitude + "," + longitude;





                        mute.ReleaseMutex();
                        Thread.Sleep(250);

                    }

                }

                catch (ArgumentNullException)
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
            string command = "set /controls/engines/current-engine/throttle " + value + "\n";
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

            string command = "set /controls/flight/aileron " + value + "\n";
            this.DataSet(command);


        }



        public void NotifyPropertyChanged(String propName)
        {

            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
            }
        }


        public string Location{

            get { return this.location; }

            set
            {
                this.location = value;

                NotifyPropertyChanged("Location");
            }

            }





        public void DataSet(string setCommand)
        {
            this.mute.WaitOne();
            this.client.write(setCommand);
            Console.WriteLine(this.client.read());
            this.mute.ReleaseMutex();
        }

        public void getError(string propName)
        {
            error += "an error has occured while fetching data on " + propName + " property \n";
            ErrorList = error;
        }


       
    }
}
