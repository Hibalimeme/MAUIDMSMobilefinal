using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace MAUIDMSMobile.Mvvm.Models
{
    public class PackDeMaintenance
    {
        public string CodePackMaintenance { get; set; }  // Code du pack de maintenance
        public string Marque { get; set; }               // Marque du véhicule
        public string Modele { get; set; }               // Modèle du véhicule
        public int Kilometrage { get; set; }             // Kilométrage du véhicule
        public int DureeEnMinutes { get; set; }          // Durée de la maintenance en minutes
        public string Resume =>
        $"{CodePackMaintenance} -  {DureeEnMinutes} ";
    }
}
