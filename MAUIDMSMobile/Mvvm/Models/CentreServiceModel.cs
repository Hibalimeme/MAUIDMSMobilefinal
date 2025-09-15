using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace MAUIDMSMobile.Mvvm.Models
{
    public class CentreServiceModel
    {
        public string Code { get; set; }      // Code du centre de service
        public string NomCentre { get; set; }      // Nom du centre de service
        public string AddressECentre { get; set; }   // Adresse du centre de service
        public string Resume =>
        $"{Code} -  {NomCentre} ";
    }
}
