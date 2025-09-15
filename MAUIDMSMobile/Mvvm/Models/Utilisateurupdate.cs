using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using MAUIDMSMobile.Resx;
namespace MAUIDMSMobile.Mvvm.Models
{
    public class Utilisateurupdate
    {
        [Required(ErrorMessageResourceName = nameof(AppResources.PrenomRequis), ErrorMessageResourceType = typeof(AppResources))]
        public string firstName { get; set; }

        [Required(ErrorMessageResourceName = nameof(AppResources.NomRequis), ErrorMessageResourceType = typeof(AppResources))]
        public string lastName { get; set; }

        [Required(ErrorMessageResourceName = nameof(AppResources.GenderRequis), ErrorMessageResourceType = typeof(AppResources))]
        public string gender { get; set; }

        [Required(ErrorMessageResourceName = nameof(AppResources.numWhatsApp), ErrorMessageResourceType = typeof(AppResources))]
        public string numWhatsApp { get; set; } // Login

        [Required(ErrorMessageResourceName = nameof(AppResources.EmailRequis), ErrorMessageResourceType = typeof(AppResources))]
        public string email { get; set; }

        [Required(ErrorMessageResourceName = nameof(AppResources.PaysRequis), ErrorMessageResourceType = typeof(AppResources))]
        public string country { get; set; }

        [Required(ErrorMessageResourceName = nameof(AppResources.VilleRequis), ErrorMessageResourceType = typeof(AppResources))]
        public string city { get; set; }

        [Required(ErrorMessageResourceName = nameof(AppResources.CodePostalRequis), ErrorMessageResourceType = typeof(AppResources))]

        public string postalCode { get; set; }

        [Required(ErrorMessageResourceName = nameof(AppResources.AddressRequis), ErrorMessageResourceType = typeof(AppResources))]
        public string address { get; set; }
        [Required(ErrorMessageResourceName = nameof(AppResources.LanguageRequis), ErrorMessageResourceType = typeof(AppResources))]
        public string language { get; set; }

    }
}
