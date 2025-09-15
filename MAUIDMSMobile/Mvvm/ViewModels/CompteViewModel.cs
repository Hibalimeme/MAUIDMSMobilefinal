using System.Diagnostics;
using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;
using MAUIDMSMobile.Mvvm.Models;
using MAUIDMSMobile.Services;
using static System.Net.Mime.MediaTypeNames;
using YourApp.Services;
using MAUIDMSMobile.Static;
using CommunityToolkit.Mvvm.ComponentModel;
using MAUIDMSMobile.Mvvm.Views;
using System.ComponentModel.DataAnnotations;
using System.Text.Json;
using MAUIDMSMobile.Resx;
using System.Globalization;





namespace MAUIDMSMobile.Mvvm.ViewModels
{
    public partial class CompteViewModel : ObservableObject
    {
                [ObservableProperty]
        public bool _isBusy;
        private readonly IUserService _userService;
        private readonly IAuthService _authService;
        //preferencesloginkey
        private string preferencesloginkey = "DMSMOBILE_login";
        private string preferencescheckkey = "DMSMOBILE_check";
        private string preferencespasswordkey = "DMSMOBILE_password";



       
        public Utilisateur User { get; set; } = new();
        public UtilisateurLogin Userlogin { get; set; } = new();
        public Utilisateurcreationdecompte Usercreationdecompte { get; set; } = new();
        public Utilisateurupdate Userupdate { get; set; } = new();
        public Utilisateurmotdepasseoublié Usermotdepasseoublié { get; set; } = new();
        

        public INavigation navigation;
        public CompteViewModel(INavigation _navigation)
        {
            navigation = _navigation;
            _userService = ServiceHelper.GetService<IUserService>();
            _authService = ServiceHelper.GetService<IAuthService>();

        }

        public bool LoadRememberMeState()
        {
            try
            {
                if (Preferences.ContainsKey(preferencesloginkey))
                {
                    if (!string.IsNullOrEmpty(Preferences.Get(preferencesloginkey, string.Empty)))
                        Userlogin.email = Preferences.Get(preferencesloginkey, string.Empty);
                }
                if (Preferences.ContainsKey(preferencespasswordkey))
                {
                    if (!string.IsNullOrEmpty(Preferences.Get(preferencespasswordkey, string.Empty)))
                        Userlogin.password = Preferences.Get(preferencespasswordkey, string.Empty);
                }
                if (Preferences.ContainsKey(preferencescheckkey))
                {
                    Userlogin.Checkkeepsessionopen = Preferences.Get(preferencescheckkey, false);
                }



                if (Userlogin.Checkkeepsessionopen &&
                    !string.IsNullOrEmpty(Userlogin.email) &&
                    !string.IsNullOrEmpty(Userlogin.password))
                {
                    User.NumWhatsApp = Userlogin.email;
                    User.Password = Userlogin.password;
                    StaticVariables.CurrentUser = new UtilisateurLogin();
                    StaticVariables.CurrentUser = Userlogin;
                    return true; // Les informations sont bien récupérées
                }

                return false; // Il manque des informations
            }
            catch (Exception V)
            {

                throw;
            }
        }

        [RelayCommand]
        private async Task CreateUser(Page page)
        {
            if (!IsBusy)
            {
                IsBusy = true;
                try
                {
                    Thread.CurrentThread.CurrentCulture = StaticMultiLang.LocalizationResourceManager.CurrentCulture;
                    Thread.CurrentThread.CurrentUICulture = StaticMultiLang.LocalizationResourceManager.CurrentCulture;

                    CultureInfo.DefaultThreadCurrentCulture = StaticMultiLang.LocalizationResourceManager.CurrentCulture;
                    CultureInfo.DefaultThreadCurrentUICulture = StaticMultiLang.LocalizationResourceManager.CurrentCulture;


                    bool formvalid = ValidationHelper.IsFormValid(Usercreationdecompte, page);
                    // Logique après la validation
                    if (formvalid)
                    {
                        string response = await _userService.CreateUserAsync(Usercreationdecompte);
                        if (!string.IsNullOrWhiteSpace(response) && response.TrimStart().StartsWith("{"))
                        {
                            // Lire le JSON extérieur
                            using var doc = JsonDocument.Parse(response);
                            string innerJson = doc.RootElement.GetProperty("value").GetString();

                            // Lire le JSON intérieur
                            using var innerDoc = JsonDocument.Parse(innerJson);
                            int resultcreate = innerDoc.RootElement.GetProperty("result").GetInt32();

                            // Si résultat = 1, on redirige vers LoginPage
                            if (resultcreate == 1)
                            {
                                await MainThread.InvokeOnMainThreadAsync(() =>
                                {
                                    App.Current.MainPage.DisplayAlert(AppResources.Succes, AppResources.comptesucces , AppResources.messageok);
                                });
                                await MainThread.InvokeOnMainThreadAsync(() =>
                                {
                                    App.Current.MainPage = new LoginPage();
                                });

                            }

                            string createResult = resultcreate switch
                            {


                                -2 => AppResources.Ereurobilgatoire,
                                -3 => AppResources.EreurDejaexist,
                                -4 => AppResources.EreurGenre,
                                _ => AppResources.EreurInconnu,

                            };
                            await MainThread.InvokeOnMainThreadAsync(() =>
                            {
                                App.Current.MainPage.DisplayAlert(AppResources.ErreurAllert, createResult, AppResources.messageok);
                            });

                        }
                        

                    }
                }


                catch (Exception ex)
                {
                    Console.WriteLine(AppResources.ErreurAllert + ex.Message);
                    string createResult = AppResources.ErreurAllert + ex.Message;
                    await MainThread.InvokeOnMainThreadAsync(() =>
                    {
                        App.Current.MainPage.DisplayAlert("Exception", $"Error: {ex.Message}", AppResources.messageok);
                    });
                }
                finally
                {
                    IsBusy = false;
                }

            }


        }
        
        [RelayCommand]
        private async Task UpdateUser(Page page)
        {
            if (!IsBusy)
            {
                IsBusy = true;
                try
                {
                    Thread.CurrentThread.CurrentCulture = StaticMultiLang.LocalizationResourceManager.CurrentCulture;
                    Thread.CurrentThread.CurrentUICulture = StaticMultiLang.LocalizationResourceManager.CurrentCulture;

                    CultureInfo.DefaultThreadCurrentCulture = StaticMultiLang.LocalizationResourceManager.CurrentCulture;
                    CultureInfo.DefaultThreadCurrentUICulture = StaticMultiLang.LocalizationResourceManager.CurrentCulture;

                    bool formvalid = ValidationHelper.IsFormValid(Userupdate, page);
                    if (formvalid)
                    {

                        string response = await _userService.UpdateUserAsync(Userupdate);
                        if (!string.IsNullOrWhiteSpace(response) && response.TrimStart().StartsWith("{"))
                        {
                            using var doc = JsonDocument.Parse(response);

                            // value est un int, pas un JSON
                            int result = doc.RootElement.GetProperty("value").GetInt32();

                            if (result == 1)
                            {
                                await MainThread.InvokeOnMainThreadAsync(async () =>
                                {
                                    await App.Current.MainPage.DisplayAlert(AppResources.MiseAjourOk, "", AppResources.messageok);
                                });

                                return; // 🔁 Prevent further code from executing
                            }

                            string message = result switch
                            {
                                -1 => AppResources.EreurUserNonTrouvé,
                                -2 => AppResources.EreurCompteBlockeOuAttent,
                                -4 => AppResources.EreurGenre,
                                _ => AppResources.EreurInconnu,
                            };

                            await MainThread.InvokeOnMainThreadAsync(() =>
                            {
                                App.Current.MainPage.DisplayAlert(AppResources.ErreurAllert, message, AppResources.messageok);
                            });
                            Console.WriteLine("API Response: " + response);
                        }


                    }



                }
                catch (Exception ex)
                {
                    Console.WriteLine(AppResources.ErreurAllert + ex.Message);
                    await MainThread.InvokeOnMainThreadAsync(() =>
                    {
                        App.Current.MainPage.DisplayAlert("Exception", $"Error: {ex.Message}", AppResources.messageok);
                    });
                }
                finally
                {
                    IsBusy = false;
                }
            }


             
        }
        
        [RelayCommand]
        private async Task AuthenticateMobileUser()
        {
            if (!IsBusy)
            {
                IsBusy = true;
                try
                {


                    // var currentpage = App.Current.MainPage;
                    Page currentpage = null;

                    if (navigation.NavigationStack.Count > 0)
                    {
                        currentpage = navigation.NavigationStack.Last();
                    }
                    else
                    {
                        // fallback si NavigationStack est vide
                        currentpage = App.Current.MainPage;
                    }
                    Thread.CurrentThread.CurrentCulture = StaticMultiLang.LocalizationResourceManager.CurrentCulture;
Thread.CurrentThread.CurrentUICulture = StaticMultiLang.LocalizationResourceManager.CurrentCulture;

CultureInfo.DefaultThreadCurrentCulture = StaticMultiLang.LocalizationResourceManager.CurrentCulture;
CultureInfo.DefaultThreadCurrentUICulture = StaticMultiLang.LocalizationResourceManager.CurrentCulture;
                    bool formvalid = ValidationHelper.IsFormValid(Userlogin, currentpage);
                    if (formvalid)
                    {
                        bool isRemembered = LoadRememberMeState(); // Charge les prefs et vérifie

                        string response = await _userService.AuthenticateMobileUserAsync(Userlogin);
                        if (!string.IsNullOrWhiteSpace(response) && response.TrimStart().StartsWith("{"))
                        {
                            int resultAuthentication = 0;

                            using (JsonDocument doc = JsonDocument.Parse(response))
                            {
                                string innerJson = doc.RootElement.GetProperty("value").GetString();

                                using (JsonDocument innerDoc = JsonDocument.Parse(innerJson))
                                {
                                    resultAuthentication = innerDoc.RootElement.GetProperty("result").GetInt32();
                                }
                            }


                            if (resultAuthentication == 1)
                            {

                                StaticVariables.CurrentUser = Userlogin;
                                Console.WriteLine($"[DEBUG] CurrentUser email: {StaticVariables.CurrentUser.email}");
                                /*var email = StaticVariables.CurrentUser.email; */ // ça vaudra "client@mail.com"

                                if (Userlogin.Checkkeepsessionopen)
                                {
                                    Preferences.Set(preferencescheckkey, true);
                                    Preferences.Set(preferencesloginkey, Userlogin.email.Trim());
                                    Preferences.Set(preferencespasswordkey, Userlogin.password);
                                }
                                else
                                {
                                    Preferences.Remove(preferencescheckkey);
                                    Preferences.Remove(preferencesloginkey);
                                    Preferences.Remove(preferencespasswordkey);
                                }

                                await MainThread.InvokeOnMainThreadAsync(() =>
                                {
                                    App.Current.MainPage = new NavigationPage(new Homepage());

                                });

                                return;
                            }
                            else
                            {
                                string loginResult = resultAuthentication switch
                                {
                                    -1 => AppResources.EreurUserNonTrouvé,
                                    -2 => AppResources.EreurMpIncorect,

                                    -3 => AppResources.EreurCompteBlock,
                                    -4 => AppResources.EreurAttendValidation,

                                    _ => AppResources.EreurInconnu,

                                };

                                await MainThread.InvokeOnMainThreadAsync(() =>
                                {
                                    App.Current.MainPage.DisplayAlert(AppResources.EchecConexion, loginResult, AppResources.messageok);
                                });
                            }
                        }


                    }



                }
                catch (Exception ex)
                {
                    Console.WriteLine("Login Error: " + ex.Message);
                    await MainThread.InvokeOnMainThreadAsync(() =>
                    {
                        App.Current.MainPage.DisplayAlert("Exception", $"Error: {ex.Message}", AppResources.messageok);
                    });
                }
                finally
                {
                    IsBusy = false;
                }
            }


               
        }
        

        [RelayCommand]
        private async Task ResetPassword()
        {
            if (!IsBusy)
            {
                IsBusy = true;
                try
                {
                    Page currentPage = null;
                    if (navigation.NavigationStack.Count > 0)
                    {
                        currentPage = navigation.NavigationStack.Last();
                    }
                    else
                    {
                        currentPage = App.Current.MainPage;
                    }
                    Thread.CurrentThread.CurrentCulture = StaticMultiLang.LocalizationResourceManager.CurrentCulture;
                    Thread.CurrentThread.CurrentUICulture = StaticMultiLang.LocalizationResourceManager.CurrentCulture;

                    CultureInfo.DefaultThreadCurrentCulture = StaticMultiLang.LocalizationResourceManager.CurrentCulture;
                    CultureInfo.DefaultThreadCurrentUICulture = StaticMultiLang.LocalizationResourceManager.CurrentCulture;

                    bool formvalid = ValidationHelper.IsFormValid(Usermotdepasseoublié, currentPage);
                  
                    if (formvalid)
                    {
                        string response = await _userService.ResetMobileUserPasswordAsync(Usermotdepasseoublié);
                        if (!string.IsNullOrWhiteSpace(response) && response.TrimStart().StartsWith("{"))
                        {
                            using var doc = JsonDocument.Parse(response);
                            string innerJson = doc.RootElement.GetProperty("value").GetString();

                            // Lire le JSON intérieur
                            using var innerDoc = JsonDocument.Parse(innerJson);
                            int resultReset = innerDoc.RootElement.GetProperty("result").GetInt32();
                            if (resultReset == 1)
                            {
                                await MainThread.InvokeOnMainThreadAsync(async () =>
                                {
                                    await App.Current.MainPage.DisplayAlert(AppResources.RenitialisationOk, "", AppResources.messageok);
                                    //App.Current.MainPage = new NavigationPage(new LoginPage());
                                });
                                return;
                            }
                            string resetResult = resultReset switch
                            {
                                -1 => AppResources.EreurUserNonTrouvé,//todo pop up 
                                -2 => AppResources.EreurCompteBlockeOuAttent,
                                -3 => AppResources.EreurEnvoieEmail,
                                _ => AppResources.EreurInconnu,

                            };
                            await MainThread.InvokeOnMainThreadAsync(() =>
                            {
                                App.Current.MainPage.DisplayAlert(AppResources.ErreurAllert, resetResult, AppResources.messageok);
                            });

                            Console.WriteLine("API Response: " + response);

                        }


                    }



                }
                catch (Exception ex)
                {
                    Console.WriteLine(AppResources.ErreurAllert + ex.Message);
                    string resetResult = AppResources.ErreurAllert + ex.Message;
                    await MainThread.InvokeOnMainThreadAsync(() =>
                    {
                        App.Current.MainPage.DisplayAlert("Exception", $"Error: {ex.Message}", AppResources.messageok);
                    });
                }
                finally
                {
                    IsBusy = false;
                }
            }


               
        }
        [RelayCommand]
        private async Task Connecter()
        {
            if (IsBusy)
                return;

            IsBusy = true;
            try
            {
                // Ici vous pouvez récupérer un token si nécessaire...

                // Navigation vers la page de login
                App.Current.MainPage = new NavigationPage(new LoginPage());
            }
            finally
            {
                IsBusy = false;
            }
        }
            [RelayCommand]
        private async Task GetTokenAsync()
        {
            try
            {
                string token = await _authService.GetAccessTokenAsync();
                Console.WriteLine("Access Token: " + token);
            }
            catch (Exception e)
            {
                Console.WriteLine("Token Error: " + e.Message);
            }
        }
    }
}