using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAUIDMSMobile.Mvvm.ViewModels
{
    class StaticResourcesViewModel : BindableObject
    {
        public string StrLogin
        {
            get => Static.StaticResources.strLogin;
            set
            {
                if (Static.StaticResources.strLogin != value)
                {
                    Static.StaticResources.strLogin = value;
                    OnPropertyChanged();
                }
            }
        }

        public string StrNumero
        {
            get => Static.StaticResources.strNumero;
            set
            {
                if (Static.StaticResources.strNumero != value)
                {
                    Static.StaticResources.strNumero = value;
                    OnPropertyChanged();
                }
            }
        }

        public string StrPassword
        {
            get => Static.StaticResources.strpassword;
            set
            {
                if (Static.StaticResources.strpassword != value)
                {
                    Static.StaticResources.strpassword = value;
                    OnPropertyChanged();
                }
            }
        }

        public string StrMotDePasseOublie
        {
            get => Static.StaticResources.strmpoublié;
            set
            {
                if (Static.StaticResources.strmpoublié != value)
                {
                    Static.StaticResources.strmpoublié = value;
                    OnPropertyChanged();
                }
            }
        }

        public string StrSeSouvenir
        {
            get => Static.StaticResources.strSesouvenir;
            set
            {
                if (Static.StaticResources.strSesouvenir != value)
                {
                    Static.StaticResources.strSesouvenir = value;
                    OnPropertyChanged();
                }
            }
        }

        public string StrConnexion
        {
            get => Static.StaticResources.strConnexion;
            set
            {
                if (Static.StaticResources.strConnexion != value)
                {
                    Static.StaticResources.strConnexion = value;
                    OnPropertyChanged();
                }
            }
        }

        public string StrPasDeCompte
        {
            get => Static.StaticResources.strpasdecompte;
            set
            {
                if (Static.StaticResources.strpasdecompte != value)
                {
                    Static.StaticResources.strpasdecompte = value;
                    OnPropertyChanged();
                }
            }
        }

        public string StrCreerUnCompte
        {
            get => Static.StaticResources.strCréeruncompte;
            set
            {
                if (Static.StaticResources.strCréeruncompte != value)
                {
                    Static.StaticResources.strCréeruncompte = value;
                    OnPropertyChanged();
                }
            }
        }

        public string StrCreationCompte
        {
            get => Static.StaticResources.strcreation;
            set
            {
                if (Static.StaticResources.strcreation != value)
                {
                    Static.StaticResources.strcreation = value;
                    OnPropertyChanged();
                }
            }
        }

        public string StrPrenom
        {
            get => Static.StaticResources.strprenom;
            set
            {
                if (Static.StaticResources.strprenom != value)
                {
                    Static.StaticResources.strprenom = value;
                    OnPropertyChanged();
                }
            }
        }

        public string StrGenre
        {
            get => Static.StaticResources.strgenre;
            set
            {
                if (Static.StaticResources.strgenre != value)
                {
                    Static.StaticResources.strgenre = value;
                    OnPropertyChanged();
                }
            }
        }

        public string StrEmail
        {
            get => Static.StaticResources.stremail;
            set
            {
                if (Static.StaticResources.stremail != value)
                {
                    Static.StaticResources.stremail = value;
                    OnPropertyChanged();
                }
            }
        }

        public string StrPays
        {
            get => Static.StaticResources.strPays;
            set
            {
                if (Static.StaticResources.strPays != value)
                {
                    Static.StaticResources.strPays = value;
                    OnPropertyChanged();
                }
            }
        }

        public string StrVille
        {
            get => Static.StaticResources.strVille;
            set
            {
                if (Static.StaticResources.strVille != value)
                {
                    Static.StaticResources.strVille = value;
                    OnPropertyChanged();
                }
            }
        }

        public string StrCodePostal
        {
            get => Static.StaticResources.strCodepostal;
            set
            {
                if (Static.StaticResources.strCodepostal != value)
                {
                    Static.StaticResources.strCodepostal = value;
                    OnPropertyChanged();
                }
            }
        }

        public string StrAdresse
        {
            get => Static.StaticResources.strAdresse;
            set
            {
                if (Static.StaticResources.strAdresse != value)
                {
                    Static.StaticResources.strAdresse = value;
                    OnPropertyChanged();
                }
            }
        }

        public string StrCompte
        {
            get => Static.StaticResources.strcompte;
            set
            {
                if (Static.StaticResources.strcompte != value)
                {
                    Static.StaticResources.strcompte = value;
                    OnPropertyChanged();
                }
            }
        }

        public string StrCode
        {
            get => Static.StaticResources.strCode;
            set
            {
                if (Static.StaticResources.strCode != value)
                {
                    Static.StaticResources.strCode = value;
                    OnPropertyChanged();
                }
            }
        }
    }
}
