using System.Collections.ObjectModel;
using System.Text.Json;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MAUIDMSMobile.Services.Interfaces;
using MAUIDMSMobile.Mvvm.Models;
using MAUIDMSMobile.Static;
using MAUIDMSMobile.Resx;
using SkiaSharp;
using System.Globalization;

namespace MAUIDMSMobile.Mvvm.ViewModels
{
    public partial class VehiculeViewModel : ObservableObject
    {
        private readonly IVehicleService _VehicleService;
        private readonly INavigation _navigation;

        private List<marquemodel> _allBrands;

        public ObservableCollection<string> Models { get; } = new();
        public ObservableCollection<string> Marques { get; } = new();
        public ObservableCollection<VehiculeAttent> VehiculesEnAttente { get; } = new();

        private ObservableCollection<VehiculeValide> _vehiculesValides = new();
        public ObservableCollection<VehiculeValide> VehiculesValides
        {
            get => _vehiculesValides;
            set => SetProperty(ref _vehiculesValides, value);
        }

        public VehiculeModel Vehicule { get; set; } = new VehiculeModel
        {
            DateMiseEnCirculation = null
        };

        private const string preferencesloginkey = "DMSMOBILE_login";

        [ObservableProperty]
        private bool isBusy;

        [ObservableProperty]
        private string selectedNo;

        [ObservableProperty]
        private string selectedBrand;

        [ObservableProperty]
        private ImageSource? photoCarteGrisePreview = "camera.png";

        private ImageSource? _photo2Preview = "camera.png";
        public ImageSource? Photo2Preview
        {
            get => _photo2Preview;
            set => SetProperty(ref _photo2Preview, value);
        }

        private ImageSource? _photo3Preview = "camera.png";
        public ImageSource? Photo3Preview
        {
            get => _photo3Preview;
            set => SetProperty(ref _photo3Preview, value);
        }

        public VehiculeViewModel(INavigation navigation)
        {
            _navigation = navigation;
            _VehicleService = ServiceHelper.GetService<IVehicleService>();
            LoadBrandsAsync();
        }

        partial void OnSelectedNoChanged(string value)
        {
            FilterBrandsByNo(value);
        }

        private async void LoadBrandsAsync()
        {
            _allBrands = await _VehicleService.GetVehicleBrandsAsync();
            var uniqueNos = _allBrands.Select(b => b.model).Distinct();

            Models.Clear();
            foreach (var no in uniqueNos)
                Models.Add(no);
        }

        private void FilterBrandsByNo(string no)
        {
            try
            {
                var brands = _allBrands
                    .Where(b => b.model == no)
                    .Select(b => b.marque)
                    .Distinct()
                    .ToList();

                Marques.Clear();
                foreach (var brand in brands)
                    Marques.Add(brand);
            }
            catch
            {
                // Log or handle if needed
            }
        }

        [RelayCommand]
        private async Task CreateVehicule(Page page)
        {
            if (IsBusy) return;

            IsBusy = true;
            try
            {
                Vehicule.Marque = SelectedNo;
                Vehicule.Modele = SelectedBrand;
                Vehicule.Photo2 = " ";
                Vehicule.Photo3 = " ";
                Vehicule.UserLogin = StaticVariables.CurrentUser.email;
                Thread.CurrentThread.CurrentCulture = StaticMultiLang.LocalizationResourceManager.CurrentCulture;
                Thread.CurrentThread.CurrentUICulture = StaticMultiLang.LocalizationResourceManager.CurrentCulture;

                CultureInfo.DefaultThreadCurrentCulture = StaticMultiLang.LocalizationResourceManager.CurrentCulture;
                CultureInfo.DefaultThreadCurrentUICulture = StaticMultiLang.LocalizationResourceManager.CurrentCulture;
                bool formvalid = ValidationHelper.IsFormValid(Vehicule, page);
                // Logique après la validation
                if (formvalid)
                {
                   

                    string response = await _VehicleService.CreateVehicleAsync(Vehicule);
                    if (!string.IsNullOrWhiteSpace(response) && response.TrimStart().StartsWith("{"))
                    {
                        using var doc = JsonDocument.Parse(response);
                        string innerJson = doc.RootElement.GetProperty("value").GetString();
                        using var innerDoc = JsonDocument.Parse(innerJson);
                        int result = innerDoc.RootElement.GetProperty("result").GetInt32();

                        if (result == 1)
                        {
                            await MainThread.InvokeOnMainThreadAsync(() =>
                                App.Current.MainPage.DisplayAlert(AppResources.Succes, AppResources.VehiculeCreeSucces, AppResources.messageok));
                        }
                        else
                        {
                            string createResult = result switch
                            {
                                -1 => AppResources.EreurMarqueInconnu,
                                -2 => AppResources.EreurModelInconnu,
                                -3 => AppResources.EreurNumChasiinvalide,
                                -4 => AppResources.EreurNumChassiexiste,
                                -5 => AppResources.EreurDateInvalide,
                                -6 => AppResources.EreurImatriculationinvalide,
                                -7 => AppResources.EreurUserInvalide,
                                -8 => AppResources.EreurPhoto,
                                _ => AppResources.EreurInconnu,
                            };

                            await App.Current.MainPage.DisplayAlert(AppResources.ErreurAllert, createResult, AppResources.messageok);
                        }
                    }
                }
                   
            }
            catch (Exception ex)
            {
                throw;
                //await App.Current.MainPage.DisplayAlert(AppResources.ErreurAllert, AppResources.Ereurservenue + ex.Message, AppResources.OK);
            }
            finally
            {
                IsBusy = false;
            }
        }

        [RelayCommand]
        private async Task PhotoCarteGriseAsync() => await PickAndAssignImageAsync("PhotoCarteGrise");

        [RelayCommand]
        private async Task Photo2Async() => await PickAndAssignImageAsync("Photo2");

        [RelayCommand]
        private async Task Photo3Async() => await PickAndAssignImageAsync("Photo3");

        private async Task PickAndAssignImageAsync(string target)
        {
            try
            {
                var result = await FilePicker.PickAsync(new PickOptions
                {
                    PickerTitle = "Choisissez une image",
                    FileTypes = FilePickerFileType.Images
                });

                if (result != null)
                {
                    using var stream = await result.OpenReadAsync();
                    using var memoryStream = new MemoryStream();
                    await stream.CopyToAsync(memoryStream);
                    var resizedBytes = ResizeImage(memoryStream.ToArray(), 800);
                    var base64 = Convert.ToBase64String(resizedBytes);

                    switch (target)
                    {
                        case "PhotoCarteGrise":
                            Vehicule.PhotoCarteGrise = base64;
                            PhotoCarteGrisePreview = ImageSource.FromStream(() => new MemoryStream(resizedBytes));
                            break;
                        case "Photo2":
                            Vehicule.Photo2 = base64;
                            Photo2Preview = ImageSource.FromStream(() => new MemoryStream(resizedBytes));
                            break;
                        case "Photo3":
                            Vehicule.Photo3 = base64;
                            Photo3Preview = ImageSource.FromStream(() => new MemoryStream(resizedBytes));
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert(AppResources.ErreurAllert, AppResources.Ereurservenue + ex.Message, AppResources.messageok);
            }
        }

        private byte[] ResizeImage(byte[] imageData, int maxWidth)
        {
            using var original = SKBitmap.Decode(imageData);

            if (original.Width <= maxWidth)
                return imageData;

            float ratio = (float)maxWidth / original.Width;
            int newWidth = (int)(original.Width * ratio);
            int newHeight = (int)(original.Height * ratio);

            using var resized = original.Resize(new SKImageInfo(newWidth, newHeight), SKFilterQuality.Medium);
            using var image = SKImage.FromBitmap(resized);
            using var data = image.Encode(SKEncodedImageFormat.Jpeg, 80);

            return data.ToArray();
        }

        public async Task LoadPendingValidationVehiclesAsync()
        {

            if (IsBusy) return;

            IsBusy = true;
            try
            {
                Thread.CurrentThread.CurrentCulture = StaticMultiLang.LocalizationResourceManager.CurrentCulture;
                Thread.CurrentThread.CurrentUICulture = StaticMultiLang.LocalizationResourceManager.CurrentCulture;

                CultureInfo.DefaultThreadCurrentCulture = StaticMultiLang.LocalizationResourceManager.CurrentCulture;
                CultureInfo.DefaultThreadCurrentUICulture = StaticMultiLang.LocalizationResourceManager.CurrentCulture;
                var vehicles = await _VehicleService.GetPendingValidationVehiclesAsync();
                VehiculesEnAttente.Clear();
                foreach (var vehicle in vehicles)
                    VehiculesEnAttente.Add(vehicle);
            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert(AppResources.ErreurAllert, AppResources.Ereurservenue + ex.Message, AppResources.messageok);
            }
            finally
            {
                IsBusy = false;
            }
        }

        public async Task LoadValidatedVehiclesAsync()
        {
            try
            {
                var vehicles = await _VehicleService.GetValidatedVehiclesAsync();
                VehiculesValides = new ObservableCollection<VehiculeValide>(vehicles);
            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert(AppResources.ErreurAllert, AppResources.Ereurservenue + ex.Message, AppResources.messageok);
            }
        }
    }
}
