using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAUIDMSMobile.Mvvm.Models
{
    public class RendezVousPosteModel
    {
        public string IdVehiculeValide { get; set; }   // Obligatoire

        // Le kilométrage actuel du véhicule
        public int KilometrageVehicule { get; set; }           // Obligatoire

        // Le code du package de maintenance sélectionné
        public string CodePackageMaintenance { get; set; } // Obligatoire

        // Le code du centre de service sélectionné
        public string CodeCentreService { get; set; }      // Obligatoire

        // Les créneaux horaires sous forme de données JSON (liste des créneaux disponibles avec date/heure)
        public string DonneesSlotJson { get; set; }           // Obligatoire

        // Le login mobile (email) du client
        public string LoginMobile { get; set; }  // Obligatoire
    }
}
