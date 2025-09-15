using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAUIDMSMobile.Mvvm.Models.Odata4
{
     public class DevisModelPostOdata
    {
        public string quoteNo { get; set; }
        public int clientResponse { get; set; } // 0: refusal, 1: acceptance
        public string loginMobile { get; set; }

    }
   
}
