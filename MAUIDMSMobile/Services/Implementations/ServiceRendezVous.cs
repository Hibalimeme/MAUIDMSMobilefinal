using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using MAUIDMSMobile.Mvvm.Models;
using MAUIDMSMobile.Services.Interfaces;
using MAUIDMSMobile.Static;
using MAUIDMSMobile.Mvvm.Models.Odata4;
using AutoMapper;
using MAUIDMSMobile.Mapper;
using System.Diagnostics;
using MAUIDMSMobile.Resx;
using System.Globalization;

namespace MAUIDMSMobile.Services.Implementations
{
    public class ServiceRendezVous : IServiceRendezVous
    {
        private readonly IAuthService _authService;
        private readonly Methods _methods;
     //private readonly string apiUrl = "https://api.businesscentral.dynamics.com/v2.0/6db6dfc0-0dec-4ea8-8818-5d7d08b50ca4/Sales/ODataV4/Company('CRONUS%20FR')/RendezVousAvenir";
        private readonly IMapper _mapper;
        private readonly IMapper _hmapper;
        private readonly IMapper _cmapper;
        private readonly IMapper _pmapper;
        private readonly IMapper _crmapper;
        private readonly IMapper _vmapper;
        private readonly IMapper _rmapper;
        private readonly IMapper _ramapper;
        
        private readonly HttpClient _httpClient;
        public ServiceRendezVous(IMapper mapper, HttpClient httpClient)
        {
            _authService = ServiceHelper.GetService<IAuthService>();
            _httpClient = httpClient;

            _methods = new Methods(_httpClient);

            _mapper = AutoMapperConfig.MapRendezVousOdataToCreneau();
            _hmapper = AutoMapperConfig.MapHistoriqueOdataToHistoriqueRendezVous();
            _cmapper = AutoMapperConfig.MapServiceCenterOdataToCentreServiceModel();
            _pmapper = AutoMapperConfig.MapPackOdataToPackDeMaintenance();
            _crmapper = AutoMapperConfig.MapCreneauxOdataToCreneau();
            _vmapper = AutoMapperConfig.CreateMapperForValidatedVehicle();
            _rmapper = AutoMapperConfig.MapRendezVousToAppointmentPostModel();
            _ramapper = AutoMapperConfig.MapRendezVousAvenirOdataToRendezVousAvenir();



        }


        public async Task<List<CentreServiceModel>> GetCentresServiceAsync()
        {
            try
            {
                string token = await _authService.GetAccessTokenAsync();

                string url = "Company('CRONUS%20FR')/ResponsibilityCenter";
                string responseContent = await _methods.SendGetRequestAsync(url, token);

                if (responseContent.StartsWith("Error"))
                {
                    return new List<CentreServiceModel> { new CentreServiceModel { NomCentre = responseContent } };
                }

                var parsedResponse = JsonConvert.DeserializeObject<ApiResponseCenter>(responseContent);

                //var parsed = JsonConvert.DeserializeObject<ApiResponseCenter<CentreServiceModel>>(responseContent);

                return _cmapper.Map<List<CentreServiceModel>>(parsedResponse.value);

            }
            catch (Exception ex)
            {
                return new List<CentreServiceModel>
        {
            new CentreServiceModel { NomCentre = "Error: " + ex.Message }
        };
            }
        }


        public async Task<List<PackDeMaintenance>> GetPacksDeMaintenanceAsync()
        {
            try
            {
                string token = await _authService.GetAccessTokenAsync();

                string url = "Company('CRONUS%20FR')/MaintenancePack";
                string responseContent = await _methods.SendGetRequestAsync(url, token);

                if (responseContent.StartsWith("Error"))
                {
                    return new List<PackDeMaintenance> { new PackDeMaintenance { Marque = responseContent } };
                }

                var parsedResponse = JsonConvert.DeserializeObject<ApiResponsePack>(responseContent);

                return _pmapper.Map<List<PackDeMaintenance>>(parsedResponse.value);  // Mapping PackOdata vers PackDeMaintenance
            }
            catch (Exception ex)
            {
                return new List<PackDeMaintenance>
        {
            new PackDeMaintenance { Marque = "Error: " + ex.Message }
        };
            }
        }

        public async Task<List<Creneau>> GetCreneauxAsync()
        {
            try
            {
                // Obtention du token d'accès
                string token = await _authService.GetAccessTokenAsync();

                // URL pour l'API OData des créneaux (remplace avec la bonne URL pour les créneaux)
                string url = string.Format("Company('{1}')/AvailableScheduledTime?$filter=LoginMobile eq '{2}'",
     "6db6dfc0-0dec-4ea8-8818-5d7d08b50ca4", "CRONUS FR", "");

                Trace.WriteLine(url);
                // Envoi de la requête GET à l'API
                string responseContent = await _methods.SendGetRequestAsync(url, token);

                // Si la réponse commence par "Error", retourne une liste avec un message d'erreur
                if (responseContent.StartsWith("Error"))
                {
                    return new List<Creneau> { new Creneau { CodeCreneau = responseContent } };
                }

                // Désérialisation de la réponse JSON
                var parsedResponse = JsonConvert.DeserializeObject<ApiResponseCreneau>(responseContent);

                // Mapping de CreneauxOdata vers Creneau
                var creneaux = _crmapper.Map<List<Creneau>>(parsedResponse.value);

                // Tri par DatePlanifiee puis HeurePlanifiee
                var sortedCreneaux = creneaux
                    .OrderBy(c => DateTime.ParseExact(c.DatePlanifiee, "yyyy-MM-dd", CultureInfo.InvariantCulture))
                    .ThenBy(c => TimeSpan.Parse(c.HeurePlanifiee))
                    .ToList();
                for (int i = 0; i < sortedCreneaux.Count; i++)
                {
                    sortedCreneaux[i].Index = i + 1;
                }

                return sortedCreneaux;
            }
            catch (Exception ex)
            {
                // Si une exception est levée, retourner une liste avec le message d'erreur
                return new List<Creneau>
        {
            new Creneau { CodeCreneau = "Error: " + ex.Message }
        };
            }
        }
        public async Task<List<RendezVousAvenirModel>> GetRendezVousAsync()
        {
            try
            {
                // Obtention du token d'accès
                string token = await _authService.GetAccessTokenAsync();

                // URL pour l'API OData des créneaux
                string url = string.Format($"Company('CRONUS%20FR')/ScheduledTimeAPI?$filter=LoginMobile%20eq%20%27{StaticVariables.CurrentUser.email}%27");
                Trace.WriteLine(url);
                // Envoi de la requête GET à l'API
                string responseContent = await _methods.SendGetRequestAsync(url, token);

                // Si la réponse commence par "Error"
                if (responseContent.StartsWith("Error"))
                {
                    return new List<RendezVousAvenirModel> { new RendezVousAvenirModel { NumeroCreneau = responseContent } };
                }

                // Désérialisation correcte
                var parsedResponse = JsonConvert.DeserializeObject<ApiResponseRenderVousAvenir>(responseContent);

                // Mapping simplifié
                return _ramapper.Map<List<RendezVousAvenirModel>>(parsedResponse.value);
            }
            catch (Exception ex)
            {
                return new List<RendezVousAvenirModel>
        {
            new RendezVousAvenirModel { NumeroCreneau = "Error: " + ex.Message }
        };
            }
        }

        public async Task<List<VehiculeValide>> GetValidatedVehiclesAsync()
        {
            try
            {

                string token = await _authService.GetAccessTokenAsync();

                string url = string.Format($"Company('CRONUS%20FR')/ValidatedVN?$filter=UserLogin%20eq%20%27{StaticVariables.CurrentUser.email}%27");// remplace par ton endpoint réel pour les validés
                Trace.WriteLine(url);
                string responseContent = await _methods.SendGetRequestAsync(url, token);

                if (responseContent.StartsWith("Error"))
                {
                    return new List<VehiculeValide> { new VehiculeValide { Marque = responseContent } };
                }

                var parsedResponse = JsonConvert.DeserializeObject<ApiResponseValidatedVN>(responseContent);

                return _vmapper.Map<List<VehiculeValide>>(parsedResponse.value);
            }
            catch (Exception ex)
            {
                return new List<VehiculeValide> { new VehiculeValide { Marque = "Error: " + ex.Message } };
            }
        }


        public async Task<List<HistoriqueRendezVous>> GetHistoriqueAsync()
        {
            try
            {
                // Obtention du token d'accès
                string token = await _authService.GetAccessTokenAsync();

                // URL pour l'API OData des créneaux (remplace avec la bonne URL pour les créneaux)
                string url = string.Format($"Company('CRONUS%20FR')/CompletedScheduledTime?$filter=UserLogin%20eq%20%27{StaticVariables.CurrentUser.email}%27");

                // Envoi de la requête GET à l'API
                string responseContent = await _methods.SendGetRequestAsync(url, token);

                // Si la réponse commence par "Error", retourne une liste avec un message d'erreur
                if (responseContent.StartsWith("Error"))
                {
                    return new List<HistoriqueRendezVous> { new HistoriqueRendezVous { NumeroCreneau = responseContent } };
                }

                // Désérialisation de la réponse JSON
                var parsedResponse = JsonConvert.DeserializeObject<ApiResponseHistorique>(responseContent);

                // Mapping de CreneauxOdata vers Creneau
                return _hmapper.Map<List<HistoriqueRendezVous>>(parsedResponse.value);
            }
            catch (Exception ex)
            {
                // Si une exception est levée, retourner une liste avec le message d'erreur
                return new List<HistoriqueRendezVous>
        {
            new HistoriqueRendezVous { NumeroCreneau = "Error: " + ex.Message }
        };
            }
        }





        public async Task<string> CreateAppointmentAsync(RendezVousPosteModel rendezVous)
        {
            try
            {
                // Récupérer le token d'accès
                string token = await _authService.GetAccessTokenAsync();

                // URL de l'API pour créer un rendez-vous
                string url = "DMSMobileCU_ReserveSlots?company=CRONUS%20FR";

                // Mapper le modèle local vers le modèle OData attendu par l'API
                var odataAppointment = _rmapper.Map<AppointmentPostModel>(rendezVous);

                // Sérialiser en JSON
                string jsonPayload = JsonConvert.SerializeObject(odataAppointment);

                // Envoyer la requête POST
                string responseContent = await _methods.SendPostRequestAsync(url, jsonPayload, token);

                // Retourner la réponse brute
                return responseContent;
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Erreur", ex.Message, "Fermer");
                // Gestion des erreurs
                return "Erreur : " + ex.Message;
            }
        }

    }

}