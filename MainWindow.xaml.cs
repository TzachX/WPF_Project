using Ex1.Model;
using Ex1.ViewModels;
using System;
using System.Collections.Generic;
using System.Configuration;
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

namespace Ex1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private IPlaneModel model;
        public static readonly DependencyProperty firstIPSeg = DependencyProperty.Register("FirstIPSeg", typeof(string), typeof(MainWindow), new PropertyMetadata((ConfigurationManager.AppSettings["ip"].ToString().Split('.'))[0]));
        public static readonly DependencyProperty secondIPSeg = DependencyProperty.Register("SecondIPSeg", typeof(string), typeof(MainWindow), new PropertyMetadata((ConfigurationManager.AppSettings["ip"].ToString().Split('.'))[1]));
        public static readonly DependencyProperty thirdIPSeg = DependencyProperty.Register("ThirdIPSeg", typeof(string), typeof(MainWindow), new PropertyMetadata((ConfigurationManager.AppSettings["ip"].ToString().Split('.'))[2]));
        public static readonly DependencyProperty fourthIPSeg = DependencyProperty.Register("FourthIPSeg", typeof(string), typeof(MainWindow), new PropertyMetadata((ConfigurationManager.AppSettings["ip"].ToString().Split('.'))[3]));
        public static readonly DependencyProperty port = DependencyProperty.Register("Port", typeof(string), typeof(MainWindow), new PropertyMetadata(ConfigurationManager.AppSettings["port"].ToString()));

        public string FirstIPSeg
        {
            get { return GetValue(firstIPSeg).ToString(); }
            set { SetValue(firstIPSeg, value); }
        }

        public string SecondIPSeg
        {
            get { return GetValue(secondIPSeg).ToString(); }
            set { SetValue(secondIPSeg, value); }
        }

        public string ThirdIPSeg
        {
            get { return GetValue(thirdIPSeg).ToString(); }
            set { SetValue(thirdIPSeg, value); }
        }

        public string FourthIPSeg
        {
            get { return GetValue(fourthIPSeg).ToString(); }
            set { SetValue(fourthIPSeg, value); }
        }

        public string Port
        {
            get { return GetValue(port).ToString(); }
            set { SetValue(port, value); }
        }


   
        public MainWindow()
        {
            InitializeComponent();
          
        }

        private void Connect_OnClick(object sender, RoutedEventArgs e)
        {
            string ip = FirstIPSeg + "." + SecondIPSeg + "." + ThirdIPSeg + "." + FourthIPSeg;
            int portNum = Int32.Parse(Port);
            (Application.Current as App).connectionVM.Connect(ip, portNum);


        }

        public void Disconnect_OnClick(object sender, RoutedEventArgs e)
        {
            (Application.Current as App).connectionVM.disconnect();
        }

        private void Check_IfValidSeg(object sender, TextCompositionEventArgs e)
        {
            int num = -1;
            if (Int32.TryParse(((TextBox)sender).Text + e.Text, out num))
            {
                if (num <= 255 && num >= 0) { e.Handled = false; }
                else
                {
                    if (num > 255) { ((TextBox)sender).Text = "255"; }
                    e.Handled = true;
                }
            }
            else
            {
                e.Handled = true;
            }
        }

        private void Check_IfValidPort(object sender, TextCompositionEventArgs e)
        {
            int num = -1;
            if (Int32.TryParse(((TextBox)sender).Text + e.Text, out num))
            {
                if (num <= 65535 && num >= 0) { e.Handled = false; }
                else
                {
                    if (num > 65535) { ((TextBox)sender).Text = "65535"; }
                    e.Handled = true;
                }
            }
            else
            {
                e.Handled = true;
            }
        }
    }
}
