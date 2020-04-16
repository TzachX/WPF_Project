using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Ex1.Views
{
    /// <summary>
    /// Interaction logic for Dashboard.xaml
    /// </summary>
    public partial class Dashboard : UserControl
    {
        // Properties definition
        public static readonly DependencyProperty headingDeg = DependencyProperty.Register("HeadingDegree", typeof(double), typeof(Dashboard), null);
        public static readonly DependencyProperty verSpeed = DependencyProperty.Register("VerticalSpeed", typeof(double), typeof(Dashboard), null);
        public static readonly DependencyProperty groundSpeed = DependencyProperty.Register("GroundSpeed", typeof(double), typeof(Dashboard), null);
        public static readonly DependencyProperty speed = DependencyProperty.Register("Speed", typeof(double), typeof(Dashboard), null);
        public static readonly DependencyProperty altitude = DependencyProperty.Register("Altitude", typeof(double), typeof(Dashboard), null);
        public static readonly DependencyProperty rollDeg = DependencyProperty.Register("RollDegree", typeof(double), typeof(Dashboard), null);
        public static readonly DependencyProperty pitchDeg = DependencyProperty.Register("PitchDegree", typeof(double), typeof(Dashboard), null);
        public static readonly DependencyProperty altimAltitude = DependencyProperty.Register("AltimAltitude", typeof(double), typeof(Dashboard), null);

        // Getter + Setter for the headingDeg dependency property
        public double HeadingDegree
        {
            get { return Convert.ToDouble(GetValue(headingDeg)); }
            set { SetValue(headingDeg, value); }
        }

        // Getter + Setter for the verSpeed dependency property
        public double VerticalSpeed
        {
            get { return Convert.ToDouble(GetValue(verSpeed)); }
            set { SetValue(verSpeed, value); }
        }

        // Getter + Setter for the groundSpeed dependency property
        public double GroundSpeed
        {
            get { return Convert.ToDouble(GetValue(groundSpeed)); }
            set { SetValue(groundSpeed, value); }
        }

        // Getter + Setter for the speed dependency property
        public double Speed
        {
            get { return Convert.ToDouble(GetValue(speed)); }
            set { SetValue(speed, value); }
        }

        // Getter + Setter for the altitude dependency property
        public double Altitude
        {
            get { return Convert.ToDouble(GetValue(altitude)); }
            set { SetValue(altitude, value); }
        }

        // Getter + Setter for the rollDeg dependency property
        public double RollDegree
        {
            get { return Convert.ToDouble(GetValue(rollDeg)); }
            set { SetValue(rollDeg, value); }
        }

        // Getter + Setter for the pitchDeg dependency property
        public double PitchDegree
        {
            get { return Convert.ToDouble(GetValue(pitchDeg)); }
            set { SetValue(pitchDeg, value); }
        }

        // Getter + Setter for the altimAltitude dependency property
        public double AltimAltitude
        {
            get { return Convert.ToDouble(GetValue(altimAltitude)); }
            set { SetValue(altimAltitude, value); }
        }

        // C-tor
        public Dashboard()
        {
            InitializeComponent();
        }
    }
}
