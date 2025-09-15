using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAUIDMSMobile.Mvvm.Models.Odata4
{
    public class RendezVousAvenirModelOdata
    {
        public string ScheduledTimeNo { get; set; }         // Numéro du créneau planifié (ST00469)
        public string LoginMobile { get; set; }             // Identifiant mobile de l'utilisateur (email)
        public string IdentifiedVehicleID { get; set; }     // Identifiant du véhicule (si déjà validé ou reconnu)
        public string Brand { get; set; }                   // Marque du véhicule
        public string Model { get; set; }                   // Modèle du véhicule
        public string ChassisNumber { get; set; }           // Numéro de châssis
        public string MaintenancePackCode { get; set; }     // Code du pack d'entretien
        public int Mileage { get; set; }                    // Kilométrage du véhicule
        public string ScheduledDate { get; set; }           // Date planifiée du rendez-vous (format YYYY-MM-DD)
        public string ScheduledTime { get; set; }           // Heure planifiée du rendez-vous (format HH:mm:ss)
        public string Box { get; set; }                     // Code du box attribué (ex: C001)
        public int Duration { get; set; }                   // Durée du rendez-vous en minutes
    }

}
