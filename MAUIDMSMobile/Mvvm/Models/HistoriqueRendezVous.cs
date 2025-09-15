using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAUIDMSMobile.Mvvm.Models
{
    public class HistoriqueRendezVous
    {
        public string NumeroCreneau { get; set; }              // Numéro du créneau planifié (ScheduledTimeNo)
        public string DatePlanifiee { get; set; }              // Date planifiée du rendez-vous (ScheduledDate)
        public string HeurePlanifiee { get; set; }             // Heure planifiée du rendez-vous (ScheduledTime)
        public string CodeCentreResponsable { get; set; }      // Code du centre de service (ResponsibilityCenter)
        public string Statut { get; set; }                     // Statut du rendez-vous (Status)
        public int DureeEnMinutes { get; set; }                // Durée du rendez-vous en minutes (Duration)
        public string IdentifiantUtilisateur { get; set; }
    }
}
