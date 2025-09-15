using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAUIDMSMobile.Mvvm.Models.Odata4
{
    public class AppointmentModel
    {
        // Vehicle information
        public string Brand { get; set; }
        public string Model { get; set; }
        public string ChassisNumber { get; set; }
        public string RegistrationNumber { get; set; }

        // Maintenance package
        public string MaintenancePackage { get; set; }

        // Service center
        public string ServiceCenter { get; set; }

        // Appointment date and time
        public DateTime AppointmentDateTime { get; set; }
    }

}
