using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;

namespace MAUIDMSMobile.Mvvm.Models
{
    public class Creneau
    {
         public string CodeCreneau { get; set; }            // Numéro du créneau
        public string IdentifiantUtilisateur { get; set; } // Identifiant de l'utilisateur mobile
        public string IdVehicule { get; set; }             // Identifiant du véhicule sélectionné
        public string Marque { get; set; }                 // Marque du véhicule
        public string Modele { get; set; }                 // Modèle du véhicule
        public string NumeroChassis { get; set; }          // Numéro de châssis
        public int Duree { get; set; }
        public string CodePackMaintenance { get; set; }    // Code du pack de maintenance
        public int Kilometrage { get; set; }               // Kilométrage
        public string DatePlanifiee { get; set; }          // Date planifiée du rendez-vous
        public string HeurePlanifiee { get; set; }         // Heure planifiée du rendez-vous
        public string CodeCentre { get; set; }
        public int Index { get; set; }
        public bool IsSelected { get; set; }
        public bool IsEnabled { get; set; } = true;

    }
}
