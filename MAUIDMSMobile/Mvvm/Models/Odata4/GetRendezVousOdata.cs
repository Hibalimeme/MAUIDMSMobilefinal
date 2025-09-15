using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAUIDMSMobile.Mvvm.Models.Odata4
{
    public class GetRendezVousOdata
    {
        public string Scheduled_Time_No { get; set; }      // Numéro du créneau
        public string LoginMobile { get; set; }            // Login mobile
        public string IdentifiedVehicleID { get; set; }    // ID véhicule identifié
        public string Brand { get; set; }                  // Marque
        public string Model { get; set; }                  // Modèle
        public string ChassisNumber { get; set; }          // Numéro de châssis
        public string MaintenancePackCode { get; set; }    // Code pack maintenance
        public int Mileage { get; set; }                   // Kilométrage
        public string ScheduledDate { get; set; }          // Date programmée
        public string ScheduledTime { get; set; }          // Heure programmée
        public string Box { get; set; }
    }
}
