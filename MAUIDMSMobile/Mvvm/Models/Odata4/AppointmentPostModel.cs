using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAUIDMSMobile.Mvvm.Models.Odata4
{
    public class AppointmentPostModel
    {
        public string validatedVehicleID { get; set; }  

        public int vehicleMileage { get; set; }          

        public string maintenancePackageCode { get; set; } 

        public string serviceCenterCode { get; set; }  

        public string slotDataJson { get; set; }        

        public string mobileLogin { get; set; }      
    }
}
