using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAUIDMSMobile.Mvvm.Models.Odata4
{
    public class PackOdata
    {
        public string MaintenancePackCode { get; set; }  // Code of the maintenance pack
        public string Brand { get; set; }                // Vehicle brand
        public string Model { get; set; }                // Vehicle model
        public int Mileage { get; set; }                 // Vehicle mileage
        public int DurationInMinutes { get; set; }       // Duration of the maintenance in minutes
    }
}
