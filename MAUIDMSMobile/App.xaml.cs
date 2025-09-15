using LocalizationResourceManager.Maui;
using MAUIDMSMobile.Mvvm.Views;
using MAUIDMSMobile.Static;
using Microsoft.Maui.Storage; // Pour Preferences

namespace MAUIDMSMobile
{
    public partial class App : Application
    {
        private string preferencesLangueKey = "DMSMOBILE_langue";

        public App(ILocalizationResourceManager localizationResourceManager)
        {
            InitializeComponent();

            // 1) Lire la langue stockée dans Preferences (sinon par défaut "en")
            var savedLang = Preferences.Get(preferencesLangueKey, "fr");

            // 2) Appliquer la langue sauvegardée
            StaticMultiLang.LocalizationResourceManager = localizationResourceManager;
            StaticMultiLang.SetAppLanguage(savedLang, localizationResourceManager);
            
            // 3) Lancer la page principale
            MainPage = new NavigationPage(new LoginPage());
        }
    }
}
