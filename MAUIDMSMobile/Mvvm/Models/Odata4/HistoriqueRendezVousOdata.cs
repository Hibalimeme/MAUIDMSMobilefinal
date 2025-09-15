using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAUIDMSMobile.Mvvm.Models.Odata4
{
    public class HistoriqueRendezVousOdata
    {

        public string ScheduledTimeNo { get; set; }          // Numéro du créneau planifié
        public string ScheduledDate { get; set; }            // Date planifiée du rendez-vous (format: yyyy-MM-dd)
        public string ScheduledTime { get; set; }            // Heure planifiée du rendez-vous (format: HH:mm:ss)
        public string ResponsibilityCenter { get; set; }     // Code du centre de service
        public string Status { get; set; }                   // Statut du rendez-vous (e.g., Reserved)
        public int Duration { get; set; }                    // Durée du rendez-vous en minutes
        public string UserLogin { get; set; }                // Login de l'utilisateur
    }
}

