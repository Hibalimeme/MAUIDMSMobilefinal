using LocalizationResourceManager.Maui;
using MAUIDMSMobile.Static;
using Mopups.Pages;
using Mopups.Services;
using System.Globalization;

namespace MAUIDMSMobile.Mvvm.Views;

public partial class Langue : PopupPage
{
    private string preferencesLangueKey = "DMSMOBILE_langue";
    private readonly ILocalizationResourceManager _localizationResourceManager;
    public bool _isBusy;
    public Langue()
    {
        try
        {
            InitializeComponent();

            // Get the ILocalizationResourceManager instance from DI
            _localizationResourceManager = Application.Current.Handler.MauiContext.Services.GetService<ILocalizationResourceManager>();
        }
        catch (Exception ex)
        {

            throw;
        }
    }

    private async void OnFranceButtonClicked(object sender, EventArgs e)
    {
        if (!IsBusy)
        {
            IsBusy = true;
            try
            {
                Preferences.Set(preferencesLangueKey, "fr");
                CultureInfo newCulture = new CultureInfo("fr");
                Thread.CurrentThread.CurrentCulture = newCulture;
                Thread.CurrentThread.CurrentUICulture = newCulture;

                CultureInfo.DefaultThreadCurrentCulture = newCulture;
                CultureInfo.DefaultThreadCurrentUICulture = newCulture;
                // Appliquer la langue ? l'application
                StaticMultiLang.LocalizationResourceManager = _localizationResourceManager;
                StaticMultiLang.SetAppLanguage("fr", _localizationResourceManager);

                // Fermer le popup
                await MopupService.Instance.PopAsync();

                // ✅ Then reset the main page safely
                Application.Current.MainPage = new NavigationPage(new LoginPage());
            }
            catch (Exception ex)
            {

                throw;
            }
            finally
            {
                IsBusy = false;
            }
        }

        // Sauvegarder le choix dans les pr?f?rences

    }


    private async void OnangletaireButtonClicked(object sender, EventArgs e)
    {
        if (!IsBusy)
        {
            IsBusy = true;
            {
                try
                {
                    Preferences.Set(preferencesLangueKey, "en");
                    CultureInfo newCulture = new CultureInfo("en");
                    Thread.CurrentThread.CurrentCulture = newCulture;
                    Thread.CurrentThread.CurrentUICulture = newCulture;

                    CultureInfo.DefaultThreadCurrentCulture = newCulture;
                    CultureInfo.DefaultThreadCurrentUICulture = newCulture;
                    StaticMultiLang.LocalizationResourceManager = _localizationResourceManager;
                    StaticMultiLang.SetAppLanguage("en", _localizationResourceManager);
                    await MopupService.Instance.PopAsync();

                    // ✅ Then reset the main page safely
                    Application.Current.MainPage = new NavigationPage(new LoginPage());
                }
                catch (Exception ex)
                {

                    throw;
                }
                finally
                {
                    IsBusy = false;
                }
            }
        }
    }

    private async void OnlibyeButtonClicked(object sender, EventArgs e)
    {
        if (!IsBusy)
        {
            IsBusy = true;
            {
                try
                {
                    Preferences.Set(preferencesLangueKey, "ar");
                    CultureInfo newCulture = new CultureInfo("ar");
                    Thread.CurrentThread.CurrentCulture = newCulture;
                    Thread.CurrentThread.CurrentUICulture = newCulture;

                    CultureInfo.DefaultThreadCurrentCulture = newCulture;
                    CultureInfo.DefaultThreadCurrentUICulture = newCulture;
                    StaticMultiLang.LocalizationResourceManager = _localizationResourceManager;
                    StaticMultiLang.SetAppLanguage("ar", _localizationResourceManager);
                    Application.Current.MainPage.FlowDirection = FlowDirection.RightToLeft;

                    await MopupService.Instance.PopAsync();

                    // ✅ Then reset the main page safely
                    Application.Current.MainPage = new NavigationPage(new LoginPage());
                }
                catch (Exception ex)
                {

                    throw;
                }
                finally
                {
                    IsBusy = false;
                }
            }
        }
    }
}
