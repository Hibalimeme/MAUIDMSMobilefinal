using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;


namespace MAUIDMSMobile.Mvvm.Models
{
    public  class Utilisateur 
{
       
        public string Prenom { get; set; }

       
        public string Nom { get; set; }


        public string Genre { get; set; }


        public string NumWhatsApp { get; set; } // Login


        public string Password { get; set; }


        public string Email { get; set; }


        public string Pays { get; set; }


        public string Ville { get; set; }


        public string CodePostal { get; set; }


        public string Adresse { get; set; }


        public string Langue { get; set; }
    }

}
