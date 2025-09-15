using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAUIDMSMobile.Mvvm.Models
{
    public class DevisModel
    {
        public string NumeroDevis { get; set; }        // Q-0001
        public int NumeroLigne { get; set; }           // 1
        public string NumeroChassis { get; set; }      // ABC123XYZ456
        public string LoginMobile { get; set; }        // user.mobile@example.com
        public DateTime Date { get; set; }             // 2024-05-07
        public string Libelle { get; set; }            // Remplacement pare-chocs avant
        public decimal Montant { get; set; }           // 450.75
        public string ImageBase64 { get; set; }        // ""
        public string Statut { get; set; }                // Statut: "en attente", "accepté", ou "refusé"
    }
}
