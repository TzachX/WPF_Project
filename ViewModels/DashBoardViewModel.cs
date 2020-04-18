using Ex1.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex1.ViewModels
{
    public class DashBoardViewModel:INotifyPropertyChanged
    {
        private IPlaneModel planeModel;
        public DashBoardViewModel(IPlaneModel model) { this.planeModel = model;
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

        public string VM_AirSpeed { get { return planeModel.AirSpeed; } }

        public string VM_Altitude { get { return planeModel.Altitude; } }

        public string VM_AltMeter { get { return planeModel.AltMeter; } }

        public string VM_GroundSpeed { get { return planeModel.GroundSpeed; } }

        public string VM_Heading { get { return planeModel.Heading; } }

        public string VM_Pitch { get { return planeModel.Pitch; } }

        public string VM_Roll { get { return planeModel.Roll; } }

        public string VM_VerticalSpeed { get { return planeModel.VerticalSpeed; } }

        public string VM_ErrorList { get { return planeModel.ErrorList; } }

    }
}
