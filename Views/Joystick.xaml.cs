using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
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
        private Point LastPos = new Point();
        private readonly Storyboard centerKnob;
        public static readonly DependencyProperty elevator = DependencyProperty.Register("Elevator", typeof(double), typeof(Joystick), null);
        public static readonly DependencyProperty rudder = DependencyProperty.Register("Rudder", typeof(double), typeof(Joystick), null);
        public static readonly DependencyProperty aileronUnit = DependencyProperty.Register("AileronUnit", typeof(double), typeof(Joystick), null);
        //public static readonly DependencyProperty elevatorUnit = DependencyProperty.Register("elevatorUnit", typeof(double), typeof(Joystick), null);

        public double Rudder
        {
            get { return Convert.ToDouble(GetValue(rudder)); }
            set { SetValue(rudder, value); }
        }

        public double AileronUnit
        {
            get { return Convert.ToDouble(GetValue(aileronUnit)); }
            set
            {
                if (value < 1) value = 1; else if (value > 90) value = 90;
                SetValue(aileronUnit, Math.Round(value));
            }
        }

        public double Elevator
        {
            get { return Convert.ToDouble(GetValue(elevator)); }
            set { SetValue(elevator, value); }
        }

        //public double ElevatorUnit
        //{
        //    get { return Convert.ToDouble(GetValue(elevatorUnit)); }
        //    set
        //    {
        //        if (value < 1) value = 1; else if (value > 90) value = 90;
        //        SetValue(elevatorUnit, Math.Round(value));
        //    }
        //}

        public Joystick()
        {
            InitializeComponent();
            centerKnob = Knob.Resources["CenterKnob"] as Storyboard;
        }
        private void centerKnob_Completed(object o, EventArgs e) 
        {
            knobPosition.X = Rudder = 0;
            knobPosition.Y = Elevator = 0;
            Storyboard animStory = (Storyboard)Knob.Resources["CenterKnob"];
           centerKnob.Stop();
        }

        private void Knob_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                double xVal = e.GetPosition(this).X - LastPos.X;
                double yVal = e.GetPosition(this).Y - LastPos.Y;

                // If we "got out" of the circle, change movement to border movement
                if (Math.Sqrt(Math.Pow(xVal, 2) + Math.Pow(yVal, 2)) > Base.Width / 4)
                {
                    double angle = Math.Atan2(0 - yVal, xVal - 0);
                    xVal = Math.Cos(angle) * (Base.Width / 4);
                    yVal = Math.Sin(-angle) * (Base.Width / 4);
                    
                }

                // Update values
                knobPosition.X = xVal;
                Rudder = xVal / 85;
                Rudder = Convert.ToDouble(String.Format("{0:0.0}", Rudder));
                knobPosition.Y = yVal;
                Elevator = yVal / 85;
                Elevator = Convert.ToDouble(String.Format("{0:0.0}", -Elevator));
            }
        }

        private void Knob_MouseUp(object sender, MouseButtonEventArgs e)
        {
            centerKnob.Begin();
            Knob.ReleaseMouseCapture();
        }

        private void Knob_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            LastPos = e.GetPosition(this);
            Knob.CaptureMouse();
        }
    }
}
