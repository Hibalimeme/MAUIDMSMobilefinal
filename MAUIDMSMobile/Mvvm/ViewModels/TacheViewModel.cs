//using System;
//using System.Collections.Generic;
//using System.Diagnostics;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using CommunityToolkit.Mvvm.ComponentModel;
//using CommunityToolkit.Mvvm.Input;
//using MAUIDMSMobile.Mvvm.Models;
//using MAUIDMSMobile.Services.Interfaces;
//using MAUIDMSMobile.Static;

//namespace MAUIDMSMobile.Mvvm.ViewModels
//{
//    public partial class TacheViewModel : ObservableObject
//    {
//        private readonly ITacheService _tacheService;

//        // Observable property for the vehicle list
//        [ObservableProperty]
//        private List<VehiculeModel> vehicules;

//        // Observable property for result message
//        [ObservableProperty]
//        private string resultMessage;

//        // Observable property for result of loading vehicles
//        [ObservableProperty]
//        private string loadVehiculesResultMessage;

//        // Observable property for result of registering a vehicle
//        [ObservableProperty]
//        private string registerVehiculeResultMessage;
//        [ObservableProperty]
//        private bool isMessageVisible = false;

//        // Public property for a single vehicle
//        public VehiculeModel Vehicule { get; private set; } = new VehiculeModel();


//        // Constructor
//        public TacheViewModel()
//        {
//            _tacheService = ServiceHelper.GetService<ITacheService>();
//        }

//        // Command to load vehicles
//        [RelayCommand]
//        public async Task LoadVehiculesAsync()
//        {
//            LoadVehiculesResultMessage = "Chargement en cours...";
//            IsMessageVisible = true; // 🔥 Affichage du message

//            try
//            {
//                Vehicules = await _tacheService.GetUserVehiculesAsync();

//                if (Vehicules == null || Vehicules.Count == 0)
//                {
//                    LoadVehiculesResultMessage = "Aucun véhicule trouvé.";
//                }
//                else
//                {
//                    StringBuilder stringBuilder = new StringBuilder();
//                    foreach (var vehicule in Vehicules)
//                    {
//                        stringBuilder.AppendLine($"Marque: {vehicule.Marque}");
//                        stringBuilder.AppendLine($"Modèle: {vehicule.Modele}");
//                        stringBuilder.AppendLine($"Date de mise en circulation: {vehicule.DateMiseEnCirculation:dd/MM/yyyy}");
//                        stringBuilder.AppendLine($"Numéro de châssis: {vehicule.NumeroChassis}");
//                        stringBuilder.AppendLine($"Immatriculation: {vehicule.Immatriculation}");
//                        stringBuilder.AppendLine("---------------------------");
//                    }
//                    LoadVehiculesResultMessage = $"{Vehicules.Count} véhicule(s) récupéré(s):\n" + stringBuilder.ToString();
//                }

//                Debug.WriteLine("Réponse API GET: " + LoadVehiculesResultMessage);
//            }
//            catch (Exception ex)
//            {
//                Debug.WriteLine("Erreur lors du chargement des véhicules: " + ex.Message);
//                LoadVehiculesResultMessage = "Erreur: " + ex.Message;
//            }
//        }


//        // Command to register a vehicle
//        [RelayCommand]
//        private async Task RegisterMobileUserVehicule()
//        {
//            try
//            {
//                // Call the service to register the vehicle
//                string response = await _tacheService.RegisterMobileUserVehiculeAsync(Vehicule);

//                // Process the response and handle different result codes
//                int result = int.Parse(response);
//                registerVehiculeResultMessage = result switch
//                {
//                    1 => "Véhicule enregistré avec succès !",
//                    -1 => "Erreur: Marque inconnue.",
//                    -2 => "Erreur: Modèle inconnu.",
//                    -3 => "Erreur: Numéro de châssis invalide.",
//                    -4 => "Erreur: Numéro de châssis déjà enregistré.",
//                    -5 => "Erreur: Date de mise en circulation invalide.",
//                    -6 => "Erreur: Immatriculation invalide.",
//                    -7 => "Erreur: Utilisateur invalide.",
//                    _ => "Erreur inconnue."
//                };

//                // Debug log for API response
//                Debug.WriteLine("Réponse API POST: " + response);
//            }
//            catch (Exception ex)
//            {
//                Debug.WriteLine("Erreur lors de l'enregistrement du véhicule: " + ex.Message);
//                registerVehiculeResultMessage = "Erreur: " + ex.Message;
//            }
//            OnPropertyChanged(nameof(registerVehiculeResultMessage));
//        }

//    }
//}
