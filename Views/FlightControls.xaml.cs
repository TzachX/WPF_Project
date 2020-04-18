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
        JoystickViewModel vm;

        public FlightControls()
        {
            InitializeComponent();
            vm = (Application.Current as App).joystickVM;
            DataContext = vm;
        }
    }
}
