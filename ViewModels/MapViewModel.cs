using Ex1.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex1.ViewModels
{
    public class MapViewModel:INotifyPropertyChanged
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

        public string VM_Location { get { return planeModel.Location; } }
        public string VM_Longitude { get { return planeModel.Longitude; } }
        public string VM_Latitude { get { return planeModel.Latitude; } }
    }
}
