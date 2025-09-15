using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MAUIDMSMobile.Services.Interfaces;
using MAUIDMSMobile.Mvvm.Models;
using MAUIDMSMobile.Static;
using MAUIDMSMobile.Resx;
using Mopups.Services;
using MAUIDMSMobile.Mvvm.Views;
using System.Text.Json;
using System.Diagnostics;


namespace MAUIDMSMobile.Mvvm.ViewModels
{
    public partial class DevisViewModel : BaseViewModel
    {

        [ObservableProperty]
        private ImageSource? _devisImage = ImageSource.FromFile("camera.png");

        [ObservableProperty]
        private ImageSource? _devisImageFullScreen;

        [ObservableProperty]
        private bool _isImageFullScreen;

        private readonly IserviceDevis _devisService;

        [ObservableProperty]
        public ObservableCollection<DevisModel> _devisList;
        [ObservableProperty]
        public DevisModel _devis;

        [ObservableProperty]
        private bool _isButtonsEnabled ;
        [ObservableProperty] private DevisModelPost _devispost ;
    

        [ObservableProperty] private DevisModel _selectedDevis;

        [ObservableProperty]
        public string _responseMessage;

        public DevisViewModel()
        {
            try
            {
                _devisService = ServiceHelper.GetService<IserviceDevis>();
                DevisList = new ObservableCollection<DevisModel>();
                IsButtonsEnabled = true;
                Devispost = new();
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        //partial void OnDevisChanged(DevisModel oldValue, DevisModel newValue)
        //{
        //    UpdateIsButtonsEnabled();
        //}

        public void UpdateIsButtonsEnabled()
        {
            IsButtonsEnabled = SelectedDevis != null && SelectedDevis.Statut == "Pending";
        }

       [RelayCommand]
private async Task Accepter()
{
            try
            {

                //if (!IsButtonsEnabled) return;
             
                Devispost.ReponseClient = 1;
                ResponseMessage = await _devisService.CreateDevisResponseAsync(Devispost);
                await App.Current.MainPage.DisplayAlert(AppResources.Succes, AppResources.devisaccpte, AppResources.messageok);
                await ClosePopupAsync();
                //SelectedDevis.Statut = "Accepté";
                //UpdateIsButtonsEnabled();
            }
            catch (Exception e)
            {

                await App.Current.MainPage.DisplayAlert(AppResources.ErreurAllert, AppResources.ErreurAllert + e.Message, AppResources.messageok);
            }
}
        private async Task ClosePopupAsync() => await MopupService.Instance.PopAsync();

        [RelayCommand]
private async Task Refuser()
{
            try
            {
                //if (!IsButtonsEnabled) return;
               
                Devispost.ReponseClient = 0;
                ResponseMessage = await _devisService.CreateDevisResponseAsync(Devispost);
                await App.Current.MainPage.DisplayAlert(AppResources.Succes, AppResources.devisrefus, AppResources.messageok);
                await ClosePopupAsync();
                //SelectedDevis.Statut = "Refusé";
                //UpdateIsButtonsEnabled();
            }
            catch (Exception ex)
            {

                await App.Current.MainPage.DisplayAlert(AppResources.ErreurAllert, AppResources.ErreurAllert + ex.Message, AppResources.messageok);
            }
}

        partial void OnSelectedDevisChanged(DevisModel? newValue)
        {
            if (newValue == null)
            {
                Debug.WriteLine("SelectedDevis is null");
            }
            else
            {
                Debug.WriteLine($"SelectedDevis has ImageBase64: {newValue.ImageBase64?.Length ?? 0} characters");
            }

            ConvertBase64ToImage(newValue?.ImageBase64);
        }
        private void ConvertBase64ToImage(string? base64)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(base64))
                {
                    DevisImage = ImageSource.FromFile("camera.png");
                    DevisImageFullScreen = DevisImage;
                    return;
                }

                // Supprimer le préfixe s’il existe
                if (base64.StartsWith("data:image"))
                {
                    var parts = base64.Split(',');
                    base64 = parts.Length > 1 ? parts[1] : base64;
                }

                byte[] imageBytes = Convert.FromBase64String(base64);

                // Vérification visuelle
                Debug.WriteLine($"[✓] Base64 convertie ({imageBytes.Length} octets)");

                DevisImage = ImageSource.FromStream(() => new MemoryStream(imageBytes));
                DevisImageFullScreen = DevisImage;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"[X] Erreur de conversion Base64 → Image: {ex.Message}");
                DevisImage = ImageSource.FromFile("camera.png");
                DevisImageFullScreen = DevisImage;
            }
        }


        [RelayCommand]
        private void ShowFullScreenImage()
        {
            IsImageFullScreen = true;
        }

        // Commande pour fermer la vue plein écran
        [RelayCommand]
        private void CloseFullScreenImage()
        {
            IsImageFullScreen = false;
        }
        public async Task ChargerDevisAsync()
            {
                if (IsBusy)
                    return;

                IsBusy = true;
                try
                {
                    var devisResult = await _devisService.GetDevisAsync();

                    if (devisResult != null && devisResult.Count > 0)
                    {
                        DevisList = new ObservableCollection<DevisModel>(devisResult);
                    }
                   
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
        [RelayCommand]
        public async Task tappedDevisCommand()
        {
            if (!IsBusy)
            {
                IsBusy = true;
                try
                {
                    await MopupService.Instance.PushAsync(new devisPOPUP(this));
                }
                catch (Exception e)
                {
                    // Tu peux logguer l'erreur ici
                    throw;
                }
                finally
                {
                    IsBusy = false;
                }
            }
        }

        [RelayCommand]
        public async Task VerifierCreneauxTapped(DevisModel selectedItem)
        {
            if (IsBusy || selectedItem == null)
                return;

            IsBusy = true;
            try
            {

                SelectedDevis = selectedItem;
                UpdateIsButtonsEnabled();
                var s = IsButtonsEnabled;
                Devispost = new();
                Devispost.LoginMobile = StaticVariables.CurrentUser.email;
                Devispost.NumDevis = SelectedDevis.NumeroDevis;

                await MopupService.Instance.PushAsync(new devisPOPUP(this));
                // Pass the whole VM instance to the popup (which has SelectedDevis set)

            }
            finally
            {
                IsBusy = false;
            }
        }
        //private async Task CreateDevis()
        //{
        //    try
        //    {
        //        // Assigner le login mobile de l'utilisateur actuel
        //        Devispost.LoginMobile = StaticVariables.CurrentUser.email;

        //        // Appeler le service de création de devis
        //        string response = await _devisService.CreateDevisResponseAsync(Devispost);

        //        // Analyser la réponse JSON
        //        using var doc = JsonDocument.Parse(response);
        //        string innerJson = doc.RootElement.GetProperty("value").GetString();

        //        using var innerDoc = JsonDocument.Parse(innerJson);
        //        int resultCreate = innerDoc.RootElement.GetProperty("result").GetInt32();

        //        // Vérifier si la création a été un succès
        //        if (resultCreate == 1)
        //        {
        //            await MainThread.InvokeOnMainThreadAsync(() =>
        //            {
        //                App.Current.MainPage.DisplayAlert("Succès", "Le devis a été créé avec succès.", AppResources.OK);
        //            });
        //        }
        //        else
        //        {
        //            await MainThread.InvokeOnMainThreadAsync(() =>
        //            {
        //                App.Current.MainPage.DisplayAlert(AppResources.ErreurAllert, "Échec de la création du devis.", AppResources.OK);
        //            });
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        // Gérer les erreurs
        //        await MainThread.InvokeOnMainThreadAsync(() =>
        //        {
        //            App.Current.MainPage.DisplayAlert(AppResources.ErreurAllert, ex.Message, AppResources.OK);
        //        });
        //    }
        //}

    }
}

