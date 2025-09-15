using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAUIDMSMobile.Mvvm.Models
{
    public class RendezVousAvenirModel
    {
        public string NumeroCreneau { get; set; }             // Numéro du créneau planifié (ex: ST00469)
        public string EmailUtilisateur { get; set; }          // Adresse e-mail de l'utilisateur mobile
        public string IdentifiantVehicule { get; set; }       // Identifiant du véhicule
        public string Marque { get; set; }                    // Marque du véhicule
        public string Modele { get; set; }                    // Modèle du véhicule
        public string NumeroChassis { get; set; }             // Numéro de châssis
        public string CodePackEntretien { get; set; }         // Code du pack d'entretien
        public int Kilometrage { get; set; }                  // Kilométrage du véhicule
        public string DatePlanifiee { get; set; }             // Date planifiée du rendez-vous (ex: 2025-05-28)
        public string HeurePlanifiee { get; set; }            // Heure planifiée du rendez-vous (ex: 17:00:00)
        public string CodeBox { get; set; }                   // Code du box attribué (ex: C001)
        public int DureeMinutes { get; set; }                 // Durée du rendez-vous en minutes (ex: 120)
    }

}
