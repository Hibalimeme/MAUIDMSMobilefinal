using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAUIDMSMobile.Mvvm.Models
{
    
    public class DevisModelPost
    {
        public string NumDevis { get; set; }
        public int ReponseClient { get; set; } // 0 : refus, 1 : acceptation
        public string LoginMobile { get; set; }
    }
}
