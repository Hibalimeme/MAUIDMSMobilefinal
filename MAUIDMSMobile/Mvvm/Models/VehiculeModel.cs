using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MAUIDMSMobile.Resx;

namespace MAUIDMSMobile.Mvvm.Models
{
    public class VehiculeModel
    {
        [Required(
ErrorMessageResourceName = nameof(AppResources.ereurmarque),
ErrorMessageResourceType = typeof(AppResources)
)]
        public string Marque { get; set; }
        // Obligatoire

        [Required(
ErrorMessageResourceName = nameof(AppResources.ereurmodel),
ErrorMessageResourceType = typeof(AppResources)
)]
        public string Modele { get; set; }                     // Obligatoire
        [Required(
ErrorMessageResourceName = nameof(AppResources.ereurnchassis),
ErrorMessageResourceType = typeof(AppResources)
)]
        public string NumeroChassis { get; set; }                 // Obligatoire
        [Required(
ErrorMessageResourceName = nameof(AppResources.ereurdateimmatriculation),
ErrorMessageResourceType = typeof(AppResources)
)]
        public DateTime? DateMiseEnCirculation { get; set; }    // Obligatoire
        [Required(
ErrorMessageResourceName = nameof(AppResources.ereurimtriculation),
ErrorMessageResourceType = typeof(AppResources)
)]
        public string Immatriculation { get; set; }            // Optionnel
        [Required(
ErrorMessageResourceName = nameof(AppResources.ereurcartegrise),
ErrorMessageResourceType = typeof(AppResources)
)]
        public string PhotoCarteGrise { get; set; }
        public string Photo2 { get; set; }
        public string Photo3 { get; set; }  // Obligatoire (Base64)
        public string UserLogin { get; set; }  // Optionnel (Base64)
       

    }

}
