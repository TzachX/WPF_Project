using Ex1.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex1.ViewModels
{
    public class JoystickViewModel
    {
        // Properties Section
        private IPlaneModel planeModel;
        public JoystickViewModel(IPlaneModel model) { this.planeModel = model; }
        
        public double VM_Rudder { set { planeModel.setRudder(value); } }
        public double VM_Elevator { set { planeModel.setElevator(value); } }
        public double VM_Throttle { set { planeModel.setThrottle(value); } }
        public double VM_Aileron { set { planeModel.setAileron(value); } }
    }
}
