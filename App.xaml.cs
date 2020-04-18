using Ex1.Model;
using Ex1.ViewModels;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace Ex1
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application

    {
        public IPlaneModel model { get; private set; }
        public DashBoardViewModel dashBoardVM { get; private set; }
        public JoystickViewModel joystickVM { get; private set; }
        public MapViewModel mapVM { get; private set; }
        public ConnectionViewModel connectionVM { get; private set; }


        private void Application_Exit(object sender, ExitEventArgs e)
        {
            this.model.disconnect();
        }

        private void App_Start(object sender, StartupEventArgs e)
        {
            this.model = new PlaneModel(new ModelTelnetClient());
            this.dashBoardVM = new DashBoardViewModel(model);
            this.joystickVM = new JoystickViewModel(model);
            this.mapVM = new MapViewModel(model);
            this.connectionVM = new ConnectionViewModel(model);
        }

    }
}
