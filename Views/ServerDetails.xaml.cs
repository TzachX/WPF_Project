using System.Configuration;
using System.Collections.Specialized;
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
    /// Interaction logic for ServerDetails.xaml
    /// </summary>
    public partial class ServerDetails : UserControl
    {
        public static readonly DependencyProperty ip = DependencyProperty.Register("IP", typeof(string), typeof(ServerDetails), new PropertyMetadata(ConfigurationManager.AppSettings["ip"].ToString()));
        public static readonly DependencyProperty port = DependencyProperty.Register("Host", typeof(int), typeof(ServerDetails), new PropertyMetadata(Int32.Parse(ConfigurationManager.AppSettings["port"].ToString())));

        public string IP
        {
            get { return Convert.ToString(GetValue(ip)); }
            set { SetValue(ip, value); }
        }

        public int Port
        {
            get { return Convert.ToInt32(GetValue(port)); }
            set { SetValue(port, value); }
        }
        public ServerDetails()
        {
            InitializeComponent();
        }
    }
}
