using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MAUIDMSMobile.Resx;

namespace MAUIDMSMobile.Mvvm.Models
{
    public class UtilisateurLogin
    {
        [Required(
ErrorMessageResourceName = nameof(AppResources.EmailRequis),
ErrorMessageResourceType = typeof(AppResources)
)]
        public string email { get; set; }
        [Required(
    ErrorMessageResourceName = nameof(AppResources.MpRequis),
    ErrorMessageResourceType = typeof(AppResources)
)]
        public string password { get; set; }
        public bool Checkkeepsessionopen { get; set; }
    }
}
