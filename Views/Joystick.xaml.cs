using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms.VisualStyles;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace FlightSimulator.Views
{
    /// <summary>
    /// Interaction logic for Joystick.xaml
    /// </summary>
    public partial class Joystick : UserControl
    {
        // Properties definition
        private Point LastPos = new Point();
        private readonly Storyboard centerKnob;
        public static readonly DependencyProperty elevator =
            DependencyProperty.Register("Elevator", typeof(double), typeof(Joystick), null);
        public static readonly DependencyProperty rudder =
            DependencyProperty.Register("Rudder", typeof(double), typeof(Joystick), null);
        const int MAX_JOYSTICK_VALUE = 85;

        // Getter + Setter for the rudder dependency property
        public double Rudder
        {
            get { return Convert.ToDouble(GetValue(rudder)); }
            set { SetValue(rudder, value); }
        }

        // Getter + Setter for the elevator dependency property
        public double Elevator
        {
            get { return Convert.ToDouble(GetValue(elevator)); }
            set { SetValue(elevator, value); }
        }

        // Reset the actual knob position when the animation ends
        private void centerKnob_Completed(object o, EventArgs e)
        {
            knobPosition.X = Rudder = 0;
            knobPosition.Y = Elevator = 0;
            Storyboard animStory = (Storyboard)Knob.Resources["CenterKnob"];
            centerKnob.Stop();
        }

        // Call this function if the mouse is moved
        private void Knob_MouseMove(object sender, MouseEventArgs e)
        {
            // If the mouse is moved AND the left mouse button is pressed
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                // Initialize the x and y values of the cursor
                double xVal = e.GetPosition(this).X - LastPos.X;
                double yVal = e.GetPosition(this).Y - LastPos.Y;

                // If we "got out" of the circle, change movement to a cirucular border movement
                if (Math.Sqrt(Math.Pow(xVal, 2) + Math.Pow(yVal, 2)) > Base.Width / 4)
                {
                    // Calculate the position according to the angle created between the cursor and the center of the joystick
                    double angle = Math.Atan2(0 - yVal, xVal - 0);
                    xVal = Math.Cos(angle) * (Base.Width / 4);
                    yVal = Math.Sin(-angle) * (Base.Width / 4);

                }

                // Update values
                knobPosition.X = xVal;
                Rudder = xVal / MAX_JOYSTICK_VALUE;
                Rudder = Convert.ToDouble(String.Format("{0:0.0}", Rudder)); // For nicer display and UX
                knobPosition.Y = yVal;
                Elevator = yVal / MAX_JOYSTICK_VALUE;
                Elevator = Convert.ToDouble(String.Format("{0:0.0}", -Elevator)); // For nicer display and UX
            }
        }

        // Call this function if the mouse's button is released
        private void Knob_MouseUp(object sender, MouseButtonEventArgs e)
        {
            // Begin the animation and return the knob to its place
            centerKnob.Begin();
            Knob.ReleaseMouseCapture();
        }

        // Call this function if the mouse's left button is pressed
        private void Knob_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            LastPos = e.GetPosition(this);
            Knob.CaptureMouse();
        }

        // C-tor
        
        public Joystick()
        {
            InitializeComponent();
            centerKnob = Knob.Resources["CenterKnob"] as Storyboard;
        }
    }
}