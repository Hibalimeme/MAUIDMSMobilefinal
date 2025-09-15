using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAUIDMSMobile.Mvvm.Models.Odata4
{
    public class OdataVehicule
    {
        public string brand { get; set; }
        public string model { get; set; }
        public string chassisNumber { get; set; }
        public DateOnly firstRegistrationDate { get; set; }
        public string registrationNumber { get; set; }
        public string mobileUser { get; set; }
        public string registrationCardPhoto { get; set; }
        public string photoTwo { get; set; }
        public string photoThree { get; set; }


    }
}
