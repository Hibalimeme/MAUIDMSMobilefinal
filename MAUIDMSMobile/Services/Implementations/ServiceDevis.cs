using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using MAUIDMSMobile.Mvvm.Models;
using MAUIDMSMobile.Services.Interfaces;
using MAUIDMSMobile.Static;
using MAUIDMSMobile.Mvvm.Models.Odata4;
using AutoMapper;
using MAUIDMSMobile.Mapper;
using MAUIDMSMobile.Resx;
using System.Net.Http;

namespace MAUIDMSMobile.Services.Implementations
{
    public class ServiceDevis : IserviceDevis
    {
        private readonly IAuthService _authService;
        private readonly Methods _methods;
        private readonly IMapper _mapper;
        private readonly IMapper _dmapper;
        private readonly HttpClient _httpClient;
        public ServiceDevis(IMapper mapper, HttpClient httpClient)
        {
            _authService = ServiceHelper.GetService<IAuthService>();
            _httpClient = httpClient;

            _methods = new Methods(_httpClient);
            _mapper = AutoMapperConfig.MapDevisOdataToDevis();
            _dmapper = AutoMapperConfig.MapDevisToDevisPostOdata();
        }

        public async Task<List<DevisModel>> GetDevisAsync()
        {
            try
            {
                string token = await _authService.GetAccessTokenAsync();

                string url = string.Format($"Company('CRONUS%20FR')/Repair?$filter=LoginMobile%20eq%20%27{StaticVariables.CurrentUser.email}%27") ;

                string responseContent = await _methods.SendGetRequestAsync(url, token);

                if (responseContent.StartsWith("Error"))
                {
                    return new List<DevisModel> { new DevisModel { NumeroDevis = responseContent } };
                }

                var parsedResponse = JsonConvert.DeserializeObject<ApiResponseDevis>(responseContent);

                return _mapper.Map<List<DevisModel>>(parsedResponse.value);

            }
            catch (Exception ex)
            {
                return new List<DevisModel>
                {
                    new DevisModel { NumeroDevis = "Error: " + ex.Message }
                };
            }
        }

            public async Task<string> CreateDevisResponseAsync(DevisModelPost devis)
        {
            try
            {
                // Récupérer le token d'accès
                string token = await _authService.GetAccessTokenAsync();

                // URL de l'API pour envoyer la réponse devis
                string url = "DMSMobileCU_RespondToRepair?company=CRONUS%20FR";

                // Mapper le modèle local vers le modèle OData attendu par l'API
                var odataDevis = _dmapper.Map<DevisModelPostOdata>(devis);

                // Sérialiser en JSON
                string jsonPayload = JsonConvert.SerializeObject(odataDevis);

                // Envoyer la requête POST
                string responseContent = await _methods.SendPostRequestAsync(url, jsonPayload, token);

                // Retourner la réponse brute
                return responseContent;
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert(AppResources.ErreurAllert, ex.Message, "Fermer");
                // Gestion des erreurs
                return "Erreur : " + ex.Message;
            }
        }

    }
}

