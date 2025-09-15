using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MAUIDMSMobile.Mvvm.Models;
using MAUIDMSMobile.Mvvm.Views;
using MAUIDMSMobile.Resx;
using MAUIDMSMobile.Services.Interfaces;
using MAUIDMSMobile.Static;
using Mopups.Services;

namespace MAUIDMSMobile.Mvvm.ViewModels
{
    public partial class RendezVousViewModel : BaseViewModel
    {
        private readonly IServiceRendezVous _rendezVousService;
        private readonly INavigation _navigation;

        public RendezVousViewModel(INavigation navigation)
        {
            _navigation = navigation;
            _rendezVousService = ServiceHelper.GetService<IServiceRendezVous>();

            CentresService = new();
            PackService = new();
            RendezvousList = new();
            RendezvousHistoriqueService = new();
            CreneauxSelectionnes = new();
        }

        [ObservableProperty] private RendezVousPosteModel rendezVous = new();
        [ObservableProperty] private ObservableCollection<VehiculeValide> vehiculesValides;
        [ObservableProperty] private ObservableCollection<CentreServiceModel> centresService;
        [ObservableProperty] private ObservableCollection<RendezVousAvenirModel> rendezvousList;
        [ObservableProperty] private ObservableCollection<HistoriqueRendezVous> rendezvousHistoriqueService;
        [ObservableProperty] private ObservableCollection<Creneau> creneauxService;
        [ObservableProperty] private ObservableCollection<Creneau> creneauxSelectionnes;
        [ObservableProperty] private ObservableCollection<PackDeMaintenance> packService;
        [ObservableProperty] private VehiculeValide vehiculeSelectionne;
        [ObservableProperty] private Creneau creneauSelectionne;
        [ObservableProperty] private CentreServiceModel centreSelectionne;
        [ObservableProperty] private PackDeMaintenance packSelectionne;

        private async Task ClosePopupAsync() => await MopupService.Instance.PopAsync();

        [RelayCommand]
        private async Task ValiderCreneaux()
        {
            Thread.CurrentThread.CurrentCulture = StaticMultiLang.LocalizationResourceManager.CurrentCulture;
            Thread.CurrentThread.CurrentUICulture = StaticMultiLang.LocalizationResourceManager.CurrentCulture;
            CultureInfo.DefaultThreadCurrentCulture = StaticMultiLang.LocalizationResourceManager.CurrentCulture;
            CultureInfo.DefaultThreadCurrentUICulture = StaticMultiLang.LocalizationResourceManager.CurrentCulture;

            if (CreneauxService?.Any() != true || PackSelectionne?.DureeEnMinutes <= 0)
            {
                await App.Current.MainPage.DisplayAlert(AppResources.ErreurAllert, AppResources.crenauxpasvalide, AppResources.messageok);
                return;
            }

            var valides = CreneauxCouvrentLaDuree(CreneauxService.Where(c => c.IsSelected).ToList(), PackSelectionne.DureeEnMinutes);

            if (!valides.Any())
            {
                await App.Current.MainPage.DisplayAlert(AppResources.ErreurAllert, AppResources.packinsufisant, AppResources.messageok);
                return;
            }

            CreneauxSelectionnes.Clear();
            foreach (var v in valides)
                CreneauxSelectionnes.Add(v);

            await ClosePopupAsync();
        }

        private List<Creneau> CreneauxCouvrentLaDuree(List<Creneau> selectionnes, int dureeVoulue)
        {
            // Trier les créneaux par date et heure
            var tries = selectionnes.OrderBy(c => DateTime.Parse($"{c.DatePlanifiee} {c.HeurePlanifiee}")).ToList();

            var result = new List<Creneau>();
            int total = 0;

            for (int i = 0; i < tries.Count; i++)
            {
                if (i > 0)
                {
                    var prev = tries[i - 1];
                    var curr = tries[i];

                    var prevTime = DateTime.Parse($"{prev.DatePlanifiee} {prev.HeurePlanifiee}");
                    var currTime = DateTime.Parse($"{curr.DatePlanifiee} {curr.HeurePlanifiee}");

                    // Vérifie si les créneaux sont successifs
                    if ((currTime - prevTime).TotalMinutes != prev.Duree)
                    {
                        return new List<Creneau>(); // ❌ Non successifs
                    }
                }

                result.Add(tries[i]);
                total += tries[i].Duree;
            }

            // ✅ Retourne les créneaux seulement si durée == durée voulue
            return total == dureeVoulue ? result : new List<Creneau>();
        }

        [RelayCommand] private async Task VehiculeSelected(VehiculeValide v) => await SetSelectedAsync(() => VehiculeSelectionne = v);
        [RelayCommand] private async Task CentreSelected(CentreServiceModel c) => await SetSelectedAsync(() => CentreSelectionne = c);
        [RelayCommand] private async Task PackSelected(PackDeMaintenance p) => await SetSelectedAsync(() => PackSelectionne = p);
        [RelayCommand] private async Task CreneauSelected(Creneau c) => await SetSelectedAsync(() => CreneauSelectionne = c);

        private async Task SetSelectedAsync(Action setSelected)
        {
            if (IsBusy) return;
            IsBusy = true;
            try
            {
                Thread.CurrentThread.CurrentCulture = StaticMultiLang.LocalizationResourceManager.CurrentCulture;
                Thread.CurrentThread.CurrentUICulture = StaticMultiLang.LocalizationResourceManager.CurrentCulture;

                CultureInfo.DefaultThreadCurrentCulture = StaticMultiLang.LocalizationResourceManager.CurrentCulture;
                CultureInfo.DefaultThreadCurrentUICulture = StaticMultiLang.LocalizationResourceManager.CurrentCulture;
                setSelected();
                await ClosePopupAsync();
            }
            finally { IsBusy = false; }
        }

        [RelayCommand]
        private Task Check(Creneau creneau)
        {
            creneau.IsSelected = !creneau.IsSelected;
            UpdateCreneauSelectionState();
            OnPropertyChanged(nameof(CreneauxService));
            return Task.CompletedTask;
        }

        private void UpdateCreneauSelectionState()
        {
            if (CreneauxService == null || PackSelectionne == null) return;

            int selectedDuration = CreneauxService.Where(c => c.IsSelected).Sum(c => c.Duree);
            bool limitReached = selectedDuration >= PackSelectionne.DureeEnMinutes;

            foreach (var c in CreneauxService)
            {
                if (!c.IsSelected)
                    c.IsEnabled = !limitReached;
            }
        }

        public async Task ChargerCentresServiceAsync() => CentresService = new(await _rendezVousService.GetCentresServiceAsync());
        public async Task LoadValidatedVehiclesAsync() => VehiculesValides = new(await _rendezVousService.GetValidatedVehiclesAsync());
        public async Task ChargerRendezVousAsync() => RendezvousList = new(await _rendezVousService.GetRendezVousAsync());
        public async Task ChargerHistoriqeAsync() => RendezvousHistoriqueService = new(await _rendezVousService.GetHistoriqueAsync());
        public async Task ChargerCreneauxAsync() => CreneauxService = new(await _rendezVousService.GetCreneauxAsync());
        public async Task ChargerPackServiceAsync() => PackService = new(await _rendezVousService.GetPacksDeMaintenanceAsync());

        [RelayCommand] private async Task OpenPopupClicked() => await MopupService.Instance.PushAsync(new Ajouterrendezvous(this));

        [RelayCommand]
        private async Task CreateRendezvous()
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
                    RendezVous.LoginMobile = StaticVariables.CurrentUser.email;

                    var slotData = CreneauxSelectionnes.Select(c => new
                    {
                        Date = c.DatePlanifiee,
                        Time = c.HeurePlanifiee
                    });

                    RendezVous.DonneesSlotJson = JsonSerializer.Serialize(slotData);
                    RendezVous.IdVehiculeValide = VehiculeSelectionne.NumChassis;
                    RendezVous.CodeCentreService = CentreSelectionne.Code;
                    RendezVous.CodePackageMaintenance = PackSelectionne.CodePackMaintenance;

                    string response = await _rendezVousService.CreateAppointmentAsync(RendezVous);

                    if (!string.IsNullOrWhiteSpace(response) && response.TrimStart().StartsWith("{"))
                    {
                        using var doc = JsonDocument.Parse(response);

                        if (doc.RootElement.TryGetProperty("value", out JsonElement valueElement))
                        {
                            string? resultString = valueElement.GetString();

                            if (int.TryParse(resultString, out int result))
                            {
                                if (result == 1)
                                {
                                    await MainThread.InvokeOnMainThreadAsync(() =>
                                        App.Current.MainPage.DisplayAlert(AppResources.Succes, AppResources.bookedok, AppResources.messageok));
                                }
                                else
                                {
                                    string msg = result switch
                                    {
                                        -1 => AppResources.rdzechoue,
                                        _ => AppResources.EreurInconnu
                                    };

                                    await App.Current.MainPage.DisplayAlert(AppResources.ErreurAllert, msg, AppResources.messageok);
                                }
                            }
                            else
                            {
                                await App.Current.MainPage.DisplayAlert(AppResources.ErreurAllert, AppResources.ErreurAllert, AppResources.messageok);
                            }
                        }
                        else
                        {
                            await App.Current.MainPage.DisplayAlert(AppResources.ErreurAllert, AppResources.ErreurAllert, AppResources.messageok);
                        }
                    }
                }
                catch (Exception ex)
                {
                    await App.Current.MainPage.DisplayAlert(AppResources.ErreurAllert, AppResources.doitremplir, AppResources.messageok);
                }
            }
               
        }

        [RelayCommand] public async Task CentreServiceTapped() => await LoadPopupAsync(ChargerCentresServiceAsync, new CentreService(this));
        [RelayCommand] public async Task PackEntretienTapped() => await LoadPopupAsync(ChargerPackServiceAsync, new PackEntretien(this));
        [RelayCommand] public async Task VehiculeTapped() => await LoadPopupAsync(LoadValidatedVehiclesAsync, new ChoixVehicule(this));

        [RelayCommand]
        public async Task VerifierCreneauxTapped()
        {
            if (IsBusy) return;
            IsBusy = true;
            try
            {
                await ChargerCreneauxAsync();
                if (MopupService.Instance.PopupStack.Any())
                    await MopupService.Instance.PopAllAsync();
                await MopupService.Instance.PushAsync(new CreneauxDisponible(this));
            }
            finally { IsBusy = false; }
        }

        private async Task LoadPopupAsync(Func<Task> loadData, Mopups.Pages.PopupPage popup)
        {
            if (IsBusy) return;
            IsBusy = true;
            try
            {
                Thread.CurrentThread.CurrentCulture = StaticMultiLang.LocalizationResourceManager.CurrentCulture;
                Thread.CurrentThread.CurrentUICulture = StaticMultiLang.LocalizationResourceManager.CurrentCulture;

                CultureInfo.DefaultThreadCurrentCulture = StaticMultiLang.LocalizationResourceManager.CurrentCulture;
                CultureInfo.DefaultThreadCurrentUICulture = StaticMultiLang.LocalizationResourceManager.CurrentCulture;
                await loadData();
                await MopupService.Instance.PushAsync(popup);
            }
            finally { IsBusy = false; }
        }
    }
}
