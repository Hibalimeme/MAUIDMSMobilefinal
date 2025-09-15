using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAUIDMSMobile.Mvvm.Models
{
    
    public class VehiculeAttent
    {
        public string userlogin { get; set; }
        public string Marque { get; set; }
        public string Model { get; set; }
        public string NumChassis { get; set; }
        public DateOnly DateMiseEnCirculation { get; set; }
        public string Immatriculation { get; set; }
    }
}
