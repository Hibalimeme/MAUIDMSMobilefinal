using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using MAUIDMSMobile.Resx;

namespace MAUIDMSMobile.Mvvm.Models
{
    public class Utilisateurmotdepasseoublié
    {
        [Required(
ErrorMessageResourceName = nameof(AppResources.EmailRequis),
ErrorMessageResourceType = typeof(AppResources)
)]
       
        public string email { get; set; }
    }
}
