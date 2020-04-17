using Ex1.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex1.ViewModels
{
    class MapViewModel
    {
        private IPlaneModel planeModel;
        public MapViewModel(IPlaneModel model)
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

        public double VM_Location { get { return planeModel.AirSpeed; } }
        public double VM_Longtitude { get { return planeModel.PlaneVert; } }
        public double VM_Latitude { get { return planeModel.PlaneHoriz; } }
    }
}
