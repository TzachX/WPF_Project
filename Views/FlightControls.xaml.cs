using Ex1.Model;
using Ex1.ViewModels;
using FlightSimulator.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Ex1.Views
{
    /// <summary>
    /// Interaction logic for FlightControls.xaml
    /// </summary>
    public partial class FlightControls : UserControl
    {
        JoystickViewModel joystickVM;
        public static readonly DependencyProperty fcRudder = DependencyProperty.Register("FC_Rudder", typeof(double), typeof(FlightControls), null);
        public static readonly DependencyProperty fcElevator = DependencyProperty.Register("FC_Elevator", typeof(double), typeof(FlightControls), null);
        public static readonly DependencyProperty fcThrottle = DependencyProperty.Register("FC_Throttle", typeof(double), typeof(FlightControls), null);
        public static readonly DependencyProperty fcAileron = DependencyProperty.Register("FC_Aileron", typeof(double), typeof(FlightControls), null);

        // Getter + Setter for the rudder dependency property
        public double FC_Rudder
        {
            get { return Convert.ToDouble(GetValue(fcRudder)); }
            set { SetValue(fcRudder, value); /*joystickVM.VM_Rudder = value;*/ }
        }

        // Getter + Setter for the elevator dependency property
        public double FC_Elevator
        {
            get { return Convert.ToDouble(GetValue(fcElevator)); }
            set { SetValue(fcElevator, value); /*joystickVM.VM_Elevator = value;*/ }
        }

        // Getter + Setter for the throttle dependency property
        public double FC_Throttle
        {
            get { return Convert.ToDouble(GetValue(fcThrottle)); }
            set { SetValue(fcThrottle, value); /*joystickVM.VM_Throttle = value;*/
            }
        }

        // Getter + Setter for the aileron dependency property
        public double FC_Aileron
        {
            get { return Convert.ToDouble(GetValue(fcAileron)); }
            set { SetValue(fcAileron, value); /*joystickVM.VM_Aileron = value;*/ }
        }

        public FlightControls()
        {
            InitializeComponent();
            joystickVM = new JoystickViewModel(new PlaneModel(new ModelTelnetClient()));
            DataContext = joystickVM;
        }

        private void ThrottleSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e) { FC_Throttle = e.NewValue; }

        private void AileronSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e) { FC_Aileron = e.NewValue; }
    }
}
