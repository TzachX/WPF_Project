using Ex1.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex1.ViewModels
{
    public class ConnectionViewModel: INotifyPropertyChanged
    {
        private IPlaneModel planeModel;
        public ConnectionViewModel(IPlaneModel model)
        {
            this.planeModel = model;
            model.PropertyChanged += delegate (Object sender, PropertyChangedEventArgs e)
            {
                NotifyProperyChange("VM_" + e.PropertyName);
            };

        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void NotifyProperyChange(string propName)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
            }
        }

   
        public void disconnect()
        {
           
            this.planeModel.disconnect();
        }


        public async void Connect(string ip, int port)
        {
            _ = await planeModel.connect(ip, port);

            if (planeModel.checkConnection)
            {
                planeModel.start();
            }

            else
            {
                this.planeModel.ErrorList += "Connection Failed";
            }


        }

        public void start()
        {
            planeModel.start();
        }

        public bool isUp()
        {
            return planeModel.checkConnection;
        }
    
    }
}
