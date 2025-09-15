using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAUIDMSMobile.Mvvm.Models.Odata4
{
    public class ValidatedVNModel
    {
        public string User_ID { get; set; }
        public string NumChassis { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public DateOnly DateMiseEnCirculation { get; set; }
        public string? Immatriculation { get; set; }
    }
}
